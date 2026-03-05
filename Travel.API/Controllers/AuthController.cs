using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.API.Helpers;
using Travel.API.Services;
using TravelPortal.Models.DTOs;
using TravelPortal.Services;

namespace Travel.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly Repository _repo;
        public AuthController(JwtService jwtService, Repository repository)
        {
            _jwtService = jwtService;
            _repo = repository;
        }

        [HttpPost("GetToken")]
        public IActionResult GetToken(LoginDto model)
        {
            var response = _repo.Login(model);
            if (response.Status == 1)
            {
                string usrno = Convert.ToString(response.data);
                var token = _jwtService.GenerateToken(usrno, model.UserId, model.UserType);
                response.data = new
                {
                    Usrno = usrno,
                    Token = token
                };
            }
            return Ok(response);
        }
        [HttpPost("SendOTP")]
        public IActionResult LoginViaOtp([FromBody] LoginViaOtpModel user)
        {
            var res = _repo.LoginViaOTP(user);
            return Ok(res);
        }
        [Authorize]
        [HttpPost("GetProfile")]
        public IActionResult GetProfile([FromBody] GetByUsrno user)
        {
            var response = _repo.GetProfile(user);
            return Ok(response);
        }
    }
}
