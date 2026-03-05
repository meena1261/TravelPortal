using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using Travel.API.Data;
using Travel.API.Helpers;
using Travel.API.Helpers.Infrastructure.Swagger;
using Travel.API.Services;
using TravelPortal.Services;
using TravelPortal.Services.Factory;
using TravelPortal.Services.Implementations;
using TravelPortal.Services.Interfaces;
using TravelPortal.Supplier.Amadeus;
using TravelPortal.Supplier.Sabre;

namespace Travel.API
{
    public class Program
    {
        // Program.cs
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient();

            builder.Services.AddScoped<AmadeusService>();
            builder.Services.AddScoped<SabreService>();

            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<ITravelPortalAPIDB, Connection>();
            // register
            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<Repository>();

            builder.Services.AddScoped<RazorpayGateway>();
            builder.Services.AddScoped<StripeGateway>();
            builder.Services.AddScoped<PaymentGatewayFactory>();

            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<ICacheService, CacheService>();

            builder.Services.AddScoped<JwtService>();

            builder.Services.AddEndpointsApiExplorer();

            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var jwtkey = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(jwtkey),

                ValidateIssuer = true,
                ValidIssuer = jwtSettings["Issuer"],

                ValidateAudience = true,
                ValidAudience = jwtSettings["Audience"],

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // token expire exact time pe
            };
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;

                options.Events = new JwtBearerEvents
                {
                    // 🔴 TOKEN INVALID / MISSING
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var result = JsonSerializer.Serialize(new JsonResponse
                        {
                            Status = 0,
                            Message = "Invalid or Missing Token"
                        });

                        await context.Response.WriteAsync(result);
                    },

                    // 🔴 ROLE FAILED / POLICY FAILED
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var result = JsonSerializer.Serialize(new JsonResponse
                        {
                            Status = 0,
                            Message = "You do not have permission to access this resource"
                        });

                        await context.Response.WriteAsync(result);
                    },

                    // 🟢 TOKEN EXPIRED (separate handle — very useful)
                    OnAuthenticationFailed = async context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            var result = JsonSerializer.Serialize(new JsonResponse
                            {
                                Status = 0,
                                Message = "Token Expired"
                            });

                            await context.Response.WriteAsync(result);
                        }
                    }
                };
            });

            builder.Services.AddControllers(options =>
            {
                options.Conventions.Add(new ApiResponseConvention());
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.OperationFilter<DefaultResponseOperationFilter>();
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter token like: Bearer {your token}"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

            });

            builder.Services.AddHttpContextAccessor();
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowReactApp",
            //        policy =>
            //        {
            //            policy
            //                .WithOrigins(
            //                    "https://frontend.silviglobal.com"
            //                )
            //                .AllowAnyHeader()
            //                .AllowAnyMethod();
            //        });
            //});
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();
            //app.UseCors("AllowReactApp");   // 🔴 MUST be before Authorization
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

