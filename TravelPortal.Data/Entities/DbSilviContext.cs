using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TravelPortal.Data.Entities;

public partial class DbSilviContext : DbContext
{
    public DbSilviContext()
    {
    }

    public DbSilviContext(DbContextOptions<DbSilviContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AgentNote> AgentNotes { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CreditWallet> CreditWallets { get; set; }

    public virtual DbSet<CreditWalletStatement> CreditWalletStatements { get; set; }

    public virtual DbSet<SupportChat> SupportChats { get; set; }

    public virtual DbSet<SupportchatAttachFile> SupportchatAttachFiles { get; set; }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=db_silvi;Persist Security Info=True;User ID=sa;Password=123456;Encrypt=True;TrustServerCertificate=True;Pooling=True;Min Pool Size=0;Max Pool Size=250;Connect Timeout=100000");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgentNote>(entity =>
        {
            entity.HasKey(e => e.NotesId);

            entity.Property(e => e.NotesId).HasColumnName("NotesID");
            entity.Property(e => e.AddDate).HasColumnType("datetime");
            entity.Property(e => e.NotesTitle).HasMaxLength(100);
            entity.Property(e => e.Priority)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Tag)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetRoles");

            entity.Property(e => e.Id).HasMaxLength(128);
            entity.Property(e => e.ConcurrencyStamp)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoleNameNormalized)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUsers");

            entity.Property(e => e.Id).HasMaxLength(128);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(256);

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

            entity.Property(e => e.UserId).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId }).HasName("PK_dbo.AspNetUserLogins");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Countrie__3214EC076BF6B268");

            entity.Property(e => e.Currency).HasMaxLength(10);
            entity.Property(e => e.CurrencySymbol).HasMaxLength(10);
            entity.Property(e => e.Iso2)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Iso3)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.PhoneCode).HasMaxLength(10);
        });

        modelBuilder.Entity<CreditWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblAccount_Credit");

            entity.ToTable("CreditWallet");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Balance)
                .HasComputedColumnSql("([Credit]-[Debit])", false)
                .HasColumnType("decimal(19, 2)");
            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDelete).HasDefaultValue(false);
        });

        modelBuilder.Entity<CreditWalletStatement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblAccount_CreditDetails");

            entity.ToTable("CreditWalletStatement");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Factor)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SupportChat>(entity =>
        {
            entity.Property(e => e.SupportChatId).HasColumnName("SupportChatID");
            entity.Property(e => e.AddDate).HasColumnType("datetime");
            entity.Property(e => e.Attachfile)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SupportchatAttachFile>(entity =>
        {
            entity.HasKey(e => e.AttachFileId);

            entity.Property(e => e.AttachFileId).HasColumnName("AttachFileID");
            entity.Property(e => e.AddDate).HasColumnType("datetime");
            entity.Property(e => e.FileName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Path)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SupportChatId).HasColumnName("SupportChatID");
        });

        modelBuilder.Entity<TblAccountMain>(entity =>
        {
            entity.ToTable("tblAccount_Main");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Balance)
                .HasComputedColumnSql("([Credit]-[Debit])", false)
                .HasColumnType("decimal(19, 2)");
            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDelete).HasDefaultValue(false);

            entity.HasOne(d => d.UsrNoNavigation).WithMany(p => p.TblAccountMains)
                .HasPrincipalKey(p => p.Usrno)
                .HasForeignKey(d => d.UsrNo)
                .HasConstraintName("FK_tblAccount_Main_tblMaster_User");
        });

        modelBuilder.Entity<TblAccountMainDetail>(entity =>
        {
            entity.ToTable("tblAccount_MainDetails");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Factor)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.UsrNoNavigation).WithMany(p => p.TblAccountMainDetails)
                .HasPrincipalKey(p => p.Usrno)
                .HasForeignKey(d => d.UsrNo)
                .HasConstraintName("FK_tblAccount_MainDetails_tblMaster_User");
        });

        modelBuilder.Entity<TblFlightConnectionVium>(entity =>
        {
            entity.HasKey(e => e.FlightConnectionViaId);

            entity.ToTable("tblFlightConnectionVia");

            entity.Property(e => e.FlightConnectionViaId).HasColumnName("FlightConnectionViaID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ArrivalTerminal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DepatureTerminal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DestinationCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Flight)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FlightInventoryId).HasColumnName("FlightInventoryID");
            entity.Property(e => e.FlightNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDelete).HasDefaultValue(false);
            entity.Property(e => e.Operator)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OriginCode)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.FlightInventory).WithMany(p => p.TblFlightConnectionVia)
                .HasForeignKey(d => d.FlightInventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblFlightConnectionVia_tblFlightInventory");
        });

        modelBuilder.Entity<TblFlightFare>(entity =>
        {
            entity.HasKey(e => e.FlightFareId);

            entity.ToTable("tblFlightFares");

            entity.Property(e => e.FlightFareId).HasColumnName("FlightFareID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.AdultFareBreakup).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChildFareBreakup).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ClassType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FlightInventoryId).HasColumnName("FlightInventoryID");
            entity.Property(e => e.InfantFareBreakup).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Pnr)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PNR");
            entity.Property(e => e.Rbd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RBD");
            entity.Property(e => e.Unit)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.FlightInventory).WithMany(p => p.TblFlightFares)
                .HasForeignKey(d => d.FlightInventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblFlightFares_tblFlightInventory");
        });

        modelBuilder.Entity<TblFlightInventory>(entity =>
        {
            entity.HasKey(e => e.FlightInventoryId);

            entity.ToTable("tblFlightInventory");

            entity.Property(e => e.FlightInventoryId).HasColumnName("FlightInventoryID");
            entity.Property(e => e.AddDate).HasColumnType("datetime");
            entity.Property(e => e.AirlineCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AirlineName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ArrivalTerminal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AvailableDays)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ConnectionType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DepartureTerminal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DestinationCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FareRules).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.IsDelete).HasDefaultValue(false);
            entity.Property(e => e.IsRejected).HasDefaultValue(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.OriginCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Remark).HasMaxLength(500);
        });

        modelBuilder.Entity<TblFlightSearchLog>(entity =>
        {
            entity.HasKey(e => e.SearchLogId);

            entity.ToTable("tblFlight_SearchLog");

            entity.Property(e => e.SearchLogId).HasColumnName("SearchLogID");
            entity.Property(e => e.AddDate).HasColumnType("datetime");
            entity.Property(e => e.DepartureDate).HasColumnType("datetime");
            entity.Property(e => e.Destination)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.IsBook).HasDefaultValue(false);
            entity.Property(e => e.Origin)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");
            entity.Property(e => e.TravelClass)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TripType)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.UsrnoNavigation).WithMany(p => p.TblFlightSearchLogs)
                .HasPrincipalKey(p => p.Usrno)
                .HasForeignKey(d => d.Usrno)
                .HasConstraintName("FK_tblFlight_SearchLog_tblMaster_User");
        });

        modelBuilder.Entity<TblHistoryFundRequest>(entity =>
        {
            entity.ToTable("tblHistory_FundRequest");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.IsRejected).HasDefaultValue(false);
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionProof)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Utrno)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("UTRNo");

            entity.HasOne(d => d.UsrnoNavigation).WithMany(p => p.TblHistoryFundRequests)
                .HasPrincipalKey(p => p.Usrno)
                .HasForeignKey(d => d.Usrno)
                .HasConstraintName("FK_tblHistory_FundRequest_tblMaster_User");
        });

        modelBuilder.Entity<TblManageAgentKyc>(entity =>
        {
            entity.ToTable("tblManage_AgentKYC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.AgencyAddress).HasMaxLength(500);
            entity.Property(e => e.AgencyName).HasMaxLength(200);
            entity.Property(e => e.AgencyTypeId).HasColumnName("AgencyTypeID");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.DocumentFile)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.GstNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PanNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.PancardImg)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Pincode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.SupportingDocumentFile)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.UsrnoNavigation).WithMany(p => p.TblManageAgentKycs)
                .HasPrincipalKey(p => p.Usrno)
                .HasForeignKey(d => d.Usrno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblManage_AgentKYC_tblMaster_User");
        });

        modelBuilder.Entity<TblManageApi>(entity =>
        {
            entity.HasKey(e => e.Apiid);

            entity.ToTable("tblManage_API");

            entity.Property(e => e.Apiid).HasColumnName("APIID");
            entity.Property(e => e.AddDate).HasColumnType("datetime");
            entity.Property(e => e.Apitype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APIType");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsLive).HasDefaultValue(false);
            entity.Property(e => e.LiveClientId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LiveClientSecret)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LiveEndPointUrl)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Supplier)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TestClientId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TestClientSecret)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TestEndPointUrl)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblManageAssignApi>(entity =>
        {
            entity.HasKey(e => e.AssignId);

            entity.ToTable("tblManage_AssignAPI");

            entity.Property(e => e.Apiid).HasColumnName("APIID");

            entity.HasOne(d => d.Api).WithMany(p => p.TblManageAssignApis)
                .HasForeignKey(d => d.Apiid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblManage_AssignAPI_tblManage_API");
        });

        modelBuilder.Entity<TblManageCityAirport>(entity =>
        {
            entity.ToTable("tblManage_CityAirport");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Icaocode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ICAOCode");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblManageCompanyAccount>(entity =>
        {
            entity.ToTable("tblManage_CompanyAccount");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.AccountType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Branch)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.BranchLocation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HolderName).HasMaxLength(100);
            entity.Property(e => e.Ifsccode)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("IFSCCode");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblManageCoupon>(entity =>
        {
            entity.ToTable("tblManage_Coupons");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CouponCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DiscountType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblManageCreditLimit>(entity =>
        {
            entity.ToTable("tblManage_CreditLimit");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.AdminRemark).HasMaxLength(500);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AttachFile)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.AvailableAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreditTypeId).HasColumnName("CreditTypeID");
            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.IsRejected).HasDefaultValue(false);
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.RequestedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsedAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblManageCustomer>(entity =>
        {
            entity.ToTable("tblManage_Customer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Otp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("OTP");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<TblManageMarkup>(entity =>
        {
            entity.ToTable("tblManage_Markup");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.AirlineId).HasColumnName("AirlineID");
            entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MarkupTypeId).HasColumnName("MarkupTypeID");
            entity.Property(e => e.MethodId).HasColumnName("MethodID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblManageMenuRight>(entity =>
        {
            entity.ToTable("tblManage_MenuRights");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsAdd).HasDefaultValue(false);
            entity.Property(e => e.IsDelete).HasDefaultValue(false);
            entity.Property(e => e.IsEdit).HasDefaultValue(false);
            entity.Property(e => e.MenuId).HasColumnName("MenuID");
        });

        modelBuilder.Entity<TblManageUserInvited>(entity =>
        {
            entity.ToTable("tblManage_UserInvited");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TblMasterAgencyType>(entity =>
        {
            entity.ToTable("tblMaster_AgencyType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AgencyType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMasterBank>(entity =>
        {
            entity.ToTable("tblMaster_Bank");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BankName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMasterBusinessType>(entity =>
        {
            entity.ToTable("tblMaster_BusinessType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BusinessType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMasterCity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblMaste__3214EC2793974ED2");

            entity.ToTable("tblMaster_City");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StateId).HasColumnName("StateID");

            entity.HasOne(d => d.State).WithMany(p => p.TblMasterCities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__tblMaster__State__2F10007B");
        });

        modelBuilder.Entity<TblMasterCreditType>(entity =>
        {
            entity.HasKey(e => e.CreditTypeId);

            entity.ToTable("tblMaster_CreditType");

            entity.Property(e => e.CreditType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblMasterKycdocumentType>(entity =>
        {
            entity.ToTable("tblMaster_KYCDocumentType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.KycdocumentType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("KYCDocumentType");
        });

        modelBuilder.Entity<TblMasterMenu>(entity =>
        {
            entity.ToTable("tblMaster_Menu");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActionName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ControllerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MenuName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PageName).HasMaxLength(100);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.SubParentId).HasColumnName("SubParentID");
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<TblMasterState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblMaste__3214EC27BB38A01D");

            entity.ToTable("tblMaster_State");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblMasterSupportingDocumentType>(entity =>
        {
            entity.ToTable("tblMaster_SupportingDocumentType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SupportingDocumentType)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblMasterUser>(entity =>
        {
            entity.ToTable("tblMaster_User");

            entity.HasIndex(e => e.Usrno, "UQ_tblMaster_User_Usrno").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.AgreementRemark).HasMaxLength(500);
            entity.Property(e => e.AspNetId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AspNetID");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.Doa)
                .HasColumnType("datetime")
                .HasColumnName("DOA");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IntroStr).IsUnicode(false);
            entity.Property(e => e.IsContract).HasDefaultValue(false);
            entity.Property(e => e.IsKycCompleted).HasDefaultValue(false);
            entity.Property(e => e.IsPaymentByAccount).HasDefaultValue(false);
            entity.Property(e => e.IsPaymentByCc)
                .HasDefaultValue(false)
                .HasColumnName("IsPaymentByCC");
            entity.Property(e => e.IsSales).HasDefaultValue(false);
            entity.Property(e => e.IsToken).HasDefaultValue(false);
            entity.Property(e => e.IsUseLogo).HasDefaultValue(false);
            entity.Property(e => e.LookLimitPeriodType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Pincode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegisterOtp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("RegisterOTP");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierAgreement)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UserID");
            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");
            entity.Property(e => e.Usrno).IsRequired();
        });

        modelBuilder.Entity<TblMasterUserType>(entity =>
        {
            entity.ToTable("tblMaster_UserType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDelete).HasDefaultValue(false);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.UserType).HasMaxLength(100);
        });

        modelBuilder.Entity<TblOtpverification>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblOTPVerification");

            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.IsVerified).HasDefaultValue(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Otp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("OTP");
        });

        modelBuilder.Entity<TblUserLevelDownline>(entity =>
        {
            entity.ToTable("tblUser_LevelDownline");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Doa)
                .HasColumnType("datetime")
                .HasColumnName("DOA");
            entity.Property(e => e.IsActive).HasDefaultValue(false);
        });

        modelBuilder.Entity<ViewUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_Users");

            entity.Property(e => e.AddDate).HasColumnType("datetime");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.AgreementRemark).HasMaxLength(500);
            entity.Property(e => e.AspNetId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AspNetID");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.Doa)
                .HasColumnType("datetime")
                .HasColumnName("DOA");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IntroStr).IsUnicode(false);
            entity.Property(e => e.IsPaymentByCc).HasColumnName("IsPaymentByCC");
            entity.Property(e => e.LookLimitPeriodType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Pincode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegisterOtp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("RegisterOTP");
            entity.Property(e => e.RoleId).HasMaxLength(128);
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.SupplierAgreement)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UserID");
            entity.Property(e => e.UserType).HasMaxLength(100);
            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
