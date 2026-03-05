using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TravelPortal.Data.Entities;

namespace TravelPortal.Data.Context;

public partial class TravelDbContext : DbContext
{
    public TravelDbContext()
    {
    }

    public TravelDbContext(DbContextOptions<TravelDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AgentNote> AgentNotes { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<SupportChat> SupportChats { get; set; }

    public virtual DbSet<SupportchatAttachFile> SupportchatAttachFiles { get; set; }

    public virtual DbSet<TblAccountCredit> TblAccountCredits { get; set; }

    public virtual DbSet<TblAccountCreditDetail> TblAccountCreditDetails { get; set; }

    public virtual DbSet<TblAccountMain> TblAccountMains { get; set; }

    public virtual DbSet<TblAccountMainDetail> TblAccountMainDetails { get; set; }

    public virtual DbSet<TblFlightConnectionVium> TblFlightConnectionVia { get; set; }

    public virtual DbSet<TblFlightFare> TblFlightFares { get; set; }

    public virtual DbSet<TblFlightInventory> TblFlightInventories { get; set; }

    public virtual DbSet<TblFlightSearchLog> TblFlightSearchLogs { get; set; }

    public virtual DbSet<TblHistoryFundRequest> TblHistoryFundRequests { get; set; }

    public virtual DbSet<TblManageAgentKyc> TblManageAgentKycs { get; set; }

    public virtual DbSet<TblManageApi> TblManageApis { get; set; }

    public virtual DbSet<TblManageAssignApi> TblManageAssignApis { get; set; }

    public virtual DbSet<TblManageCityAirport> TblManageCityAirports { get; set; }

    public virtual DbSet<TblManageCompanyAccount> TblManageCompanyAccounts { get; set; }

    public virtual DbSet<TblManageCoupon> TblManageCoupons { get; set; }

    public virtual DbSet<TblManageCreditLimit> TblManageCreditLimits { get; set; }

    public virtual DbSet<TblManageCustomer> TblManageCustomers { get; set; }

    public virtual DbSet<TblManageMarkup> TblManageMarkups { get; set; }

    public virtual DbSet<TblManageMenuRight> TblManageMenuRights { get; set; }

    public virtual DbSet<TblManageUserInvited> TblManageUserInviteds { get; set; }

    public virtual DbSet<TblMasterAgencyType> TblMasterAgencyTypes { get; set; }

    public virtual DbSet<TblMasterBank> TblMasterBanks { get; set; }

    public virtual DbSet<TblMasterBusinessType> TblMasterBusinessTypes { get; set; }

    public virtual DbSet<TblMasterCity> TblMasterCities { get; set; }

    public virtual DbSet<TblMasterCreditType> TblMasterCreditTypes { get; set; }

    public virtual DbSet<TblMasterKycdocumentType> TblMasterKycdocumentTypes { get; set; }

    public virtual DbSet<TblMasterMenu> TblMasterMenus { get; set; }

    public virtual DbSet<TblMasterState> TblMasterStates { get; set; }

    public virtual DbSet<TblMasterSupportingDocumentType> TblMasterSupportingDocumentTypes { get; set; }

    public virtual DbSet<TblMasterUser> TblMasterUsers { get; set; }

    public virtual DbSet<TblMasterUserType> TblMasterUserTypes { get; set; }

    public virtual DbSet<TblOtpverification> TblOtpverifications { get; set; }

    public virtual DbSet<TblUserLevelDownline> TblUserLevelDownlines { get; set; }

    public virtual DbSet<ViewUser> ViewUsers { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=db_silvi;Persist Security Info=True;User ID=sa;Password=123456;Encrypt=True;TrustServerCertificate=True;Pooling=True;Min Pool Size=0;Max Pool Size=250;Connect Timeout=100000");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetRoles");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUsers");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK_dbo.AspNetUserRoles");
                        j.ToTable("AspNetUserRoles");
                        j.IndexerProperty<string>("UserId").HasMaxLength(128);
                        j.IndexerProperty<string>("RoleId").HasMaxLength(128);
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUserClaims");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId }).HasName("PK_dbo.AspNetUserLogins");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Iso2).IsFixedLength();
            entity.Property(e => e.Iso3).IsFixedLength();
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyCode).HasName("PK__Currenci__408426BED7B6E367");

            entity.Property(e => e.CurrencyCode).IsFixedLength();
            entity.Property(e => e.CurrencyId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TblAccountCredit>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Balance).HasComputedColumnSql("([Credit]-[Debit])", false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDelete).HasDefaultValue(false);
        });

        modelBuilder.Entity<TblAccountCreditDetail>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblAccountMain>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Balance).HasComputedColumnSql("([Credit]-[Debit])", false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDelete).HasDefaultValue(false);

            entity.HasOne(d => d.UsrNoNavigation).WithMany(p => p.TblAccountMains)
                .HasPrincipalKey(p => p.Usrno)
                .HasForeignKey(d => d.UsrNo)
                .HasConstraintName("FK_tblAccount_Main_tblMaster_User");
        });

        modelBuilder.Entity<TblAccountMainDetail>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.UsrNoNavigation).WithMany(p => p.TblAccountMainDetails)
                .HasPrincipalKey(p => p.Usrno)
                .HasForeignKey(d => d.UsrNo)
                .HasConstraintName("FK_tblAccount_MainDetails_tblMaster_User");
        });

        modelBuilder.Entity<TblFlightConnectionVium>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDelete).HasDefaultValue(false);

            entity.HasOne(d => d.FlightInventory).WithMany(p => p.TblFlightConnectionVia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblFlightConnectionVia_tblFlightInventory");
        });

        modelBuilder.Entity<TblFlightFare>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.FlightInventory).WithMany(p => p.TblFlightFares)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblFlightFares_tblFlightInventory");
        });

        modelBuilder.Entity<TblFlightInventory>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.IsDelete).HasDefaultValue(false);
            entity.Property(e => e.IsRejected).HasDefaultValue(false);
        });

        modelBuilder.Entity<TblFlightSearchLog>(entity =>
        {
            entity.Property(e => e.IsBook).HasDefaultValue(false);

            entity.HasOne(d => d.UsrnoNavigation).WithMany(p => p.TblFlightSearchLogs)
                .HasPrincipalKey(p => p.Usrno)
                .HasForeignKey(d => d.Usrno)
                .HasConstraintName("FK_tblFlight_SearchLog_tblMaster_User");
        });

        modelBuilder.Entity<TblHistoryFundRequest>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.IsRejected).HasDefaultValue(false);

            entity.HasOne(d => d.UsrnoNavigation).WithMany(p => p.TblHistoryFundRequests)
                .HasPrincipalKey(p => p.Usrno)
                .HasForeignKey(d => d.Usrno)
                .HasConstraintName("FK_tblHistory_FundRequest_tblMaster_User");
        });

        modelBuilder.Entity<TblManageAgentKyc>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.UsrnoNavigation).WithMany(p => p.TblManageAgentKycs)
                .HasPrincipalKey(p => p.Usrno)
                .HasForeignKey(d => d.Usrno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblManage_AgentKYC_tblMaster_User");
        });

        modelBuilder.Entity<TblManageApi>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsLive).HasDefaultValue(false);
        });

        modelBuilder.Entity<TblManageAssignApi>(entity =>
        {
            entity.HasOne(d => d.Api).WithMany(p => p.TblManageAssignApis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblManage_AssignAPI_tblManage_API");
        });

        modelBuilder.Entity<TblManageCityAirport>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblManageCompanyAccount>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblManageCoupon>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblManageCreditLimit>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.IsRejected).HasDefaultValue(false);
        });

        modelBuilder.Entity<TblManageCustomer>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblManageMarkup>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblManageMenuRight>(entity =>
        {
            entity.Property(e => e.IsAdd).HasDefaultValue(false);
            entity.Property(e => e.IsDelete).HasDefaultValue(false);
            entity.Property(e => e.IsEdit).HasDefaultValue(false);
        });

        modelBuilder.Entity<TblManageUserInvited>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblMasterAgencyType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMasterBank>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMasterBusinessType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMasterCity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblMaste__3214EC2793974ED2");

            entity.HasOne(d => d.State).WithMany(p => p.TblMasterCities).HasConstraintName("FK__tblMaster__State__2F10007B");
        });

        modelBuilder.Entity<TblMasterCreditType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMasterKycdocumentType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMasterMenu>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMasterState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblMaste__3214EC27BB38A01D");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMasterSupportingDocumentType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMasterUser>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsContract).HasDefaultValue(false);
            entity.Property(e => e.IsKycCompleted).HasDefaultValue(false);
            entity.Property(e => e.IsPaymentByAccount).HasDefaultValue(false);
            entity.Property(e => e.IsPaymentByCc).HasDefaultValue(false);
            entity.Property(e => e.IsSales).HasDefaultValue(false);
            entity.Property(e => e.IsToken).HasDefaultValue(false);
            entity.Property(e => e.IsUseLogo).HasDefaultValue(false);

        });

        modelBuilder.Entity<TblMasterUserType>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDelete).HasDefaultValue(false);
        });

        modelBuilder.Entity<TblOtpverification>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IsVerified).HasDefaultValue(false);
        });

        modelBuilder.Entity<TblUserLevelDownline>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(false);
        });

        modelBuilder.Entity<ViewUser>(entity =>
        {
            entity.ToView("View_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
