using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MtchMKRAPI.Data.Entities;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Reflection.Metadata;

namespace MtchMKRAPI.Data
{

    public class MtckMKRDbContext : DbContext
    {
        public MtckMKRDbContext(DbContextOptions<MtckMKRDbContext> options) : base(options)
        {

        }

        public DbSet<Game> games { get; set; }
        public DbSet<Match> matches { get; set; }
        public DbSet<Locations> locations { get; set; }

        public DbSet<MatchBooking> matchBookings { get; set; }
              
        public DbSet<Profile> profiles { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<ShowCase> showCases { get; set; }
        public DbSet<PendingMatchDetail> PendingMatchDetails { get; set; }
        public DbSet<PlayerList> playerLists { get; set; }
        public DbSet<MatchDetails> matchDetails { get; set; }
        public DbSet<MatchScoreCard> matchScoreCards { get; set; }
        public DbSet<SearchPlayers> playerListWithDateTimes { get; set; }
        public DbSet<UserProfileImage> userProfileImages { get; set; }
        public DbSet<UserImage> userImage{ get; set; }
        public DbSet<BookedMatchDetails> bookedMatchDetails { get; set; }
        public DbSet<MatchScoreCardDetails> matchScoreCardDetails { get; set; }
        public DbSet<PlaylistForSearch> PlaylistForSearch { get; set; }
        public DbSet<UserRegistration> userRegistration { get; set; }
        public DbSet<UserDeviceInfo> userDeviceInfos { get; set; }
        public DbSet<UsersPaymentInfo> usersPaymentInfos { get; set; }
        public DbSet<MatchRequestDetail> matchRequestDetails { get; set; }
        public DbSet<MatchCreation> matchCreations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Game>().Property(x => x.GameId).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<UserImage>().ToTable("UserImage");
            modelBuilder.Entity<Match>().ToTable("Match");
            modelBuilder.Entity<MatchBooking>().ToTable("MatchBooking");
            modelBuilder.Entity<Profile>().ToTable("Profile");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<UserRegistration>().ToTable("UserRegistration");
            modelBuilder.Entity<UserProfileImage>().ToView("viewUserProfile");
            modelBuilder.Entity<UserProfileImage>().HasNoKey();
            modelBuilder.Entity<ShowCase>().ToView("viewforShowcase");
            modelBuilder.Entity<ShowCase>().HasNoKey();
            modelBuilder.Entity<MatchDetails>().ToView("viewforMatchDetails");
            modelBuilder.Entity<MatchDetails>().HasNoKey();
            modelBuilder.Entity<Locations>().ToTable("Location");
            modelBuilder.Entity<PlayerList>().ToView("viewSearchPlayer");
            modelBuilder.Entity<PlayerList>().HasNoKey();
            modelBuilder.Entity<PendingMatchDetail>().ToView("viewforPendingMatch");
            modelBuilder.Entity<PendingMatchDetail>().HasNoKey();
            modelBuilder.Entity<SearchPlayers>().ToView("viewSearchPlayerByDateTime");
            modelBuilder.Entity<SearchPlayers>().HasNoKey();
            modelBuilder.Entity<MatchScoreCard>().ToTable("MatchScoreCard");
            modelBuilder.Entity<BookedMatchDetails>().ToView("viewforBookedMatches");
            modelBuilder.Entity<BookedMatchDetails>().HasNoKey();
            modelBuilder.Entity<PlaylistForSearch>().HasNoKey();
            modelBuilder.Entity<MatchScoreCardDetails>().HasNoKey();
            modelBuilder.Entity<PlaylistForSearch>().ToFunction("getUsersListByDateTime");
            modelBuilder.Entity<UsersPaymentInfo>().ToTable("UsersPaymentInfo");
            modelBuilder.Entity<UserDeviceInfo>().ToTable("UsersDeviceInfo");
            modelBuilder.Entity<MatchRequestDetail>().ToTable("MatchRequestDetail");
            modelBuilder.Entity<MatchCreation>().ToTable("MatchCreation");
            modelBuilder.Entity<MatchCreation>().Ignore("matchRequestUsers");
        }
        //public virtual ObjectResult<PlaylistForSearch> getUsersListByDateTime(DateTime date1)
        //{
        //    var dateN = date1 != null ?
        //        new ObjectParameter("matchdate", date1) :
        //        new ObjectParameter("matchdate", typeof(DateTime));

        //    return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PlaylistForSearch>("getUsersListByDateTime", dateN);
        //}
           
    }
//    public mtchMKRContext()
//    {
//    }

//    public mtchMKRContext(DbContextOptions<mtchMKRContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Game> Games { get; set; } = null!;
//    public virtual DbSet<Match> Matches { get; set; } = null!;
//    public virtual DbSet<MatchBooking> MatchBookings { get; set; } = null!;
//    public virtual DbSet<Profile> Profiles { get; set; } = null!;
//    public virtual DbSet<User> Users { get; set; } = null!;
//    public virtual DbSet<UserImage> UserImages { get; set; } = null!;

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        if (!optionsBuilder.IsConfigured)
//        {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//            optionsBuilder.UseSqlServer("Server=OTC7;database=mtchMKR;user id=sa;password=Admin@123;");
//        }
//    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Game>(entity =>
    //    {
    //        entity.ToTable("Game");

    //        entity.HasIndex(e => e.Name, "UQ_NAME")
    //            .IsUnique();

    //        entity.Property(e => e.GameId).HasDefaultValueSql("(newid())");

    //        entity.Property(e => e.CreatedDate).HasColumnType("datetime");

    //        entity.Property(e => e.Name)
    //            .HasMaxLength(50)
    //            .IsUnicode(false);
    //    });

    //    modelBuilder.Entity<Match>(entity =>
    //    {
    //        entity.ToTable("Match");

    //        entity.Property(e => e.MatchId)
    //            .HasColumnName("MatchID")
    //            .HasDefaultValueSql("(newid())");

    //        entity.Property(e => e.CreatedDate).HasColumnType("datetime");

    //        entity.Property(e => e.Date).HasColumnType("date");

    //        entity.Property(e => e.Frames)
    //            .HasMaxLength(50)
    //            .IsUnicode(false);

    //        entity.HasOne(d => d.Game)
    //            .WithMany(p => p.Matches)
    //            .HasForeignKey(d => d.GameId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_Match_GameId");
    //    });

    //    modelBuilder.Entity<MatchBooking>(entity =>
    //    {
    //        entity.ToTable("MatchBooking");

    //        entity.Property(e => e.MatchBookingId)
    //            .HasColumnName("MatchBookingID")
    //            .HasDefaultValueSql("(newid())");

    //        entity.Property(e => e.CreatedDate).HasColumnType("datetime");

    //        entity.HasOne(d => d.Match)
    //            .WithMany(p => p.MatchBookings)
    //            .HasForeignKey(d => d.MatchId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_Match_MatchId");

    //        entity.HasOne(d => d.User)
    //            .WithMany(p => p.MatchBookings)
    //            .HasForeignKey(d => d.UserId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_Match_UserId");
    //    });

    //    modelBuilder.Entity<Profile>(entity =>
    //    {
    //        entity.ToTable("Profile");

    //        entity.Property(e => e.ProfileId).HasDefaultValueSql("(newid())");

    //        entity.Property(e => e.CreatedDate).HasColumnType("datetime");

    //        entity.Property(e => e.NotificationMethod).HasMaxLength(500);

    //        entity.Property(e => e.Place).HasMaxLength(50);

    //        entity.Property(e => e.PlaysAgainst).HasMaxLength(500);

    //        entity.Property(e => e.Preferredgames).HasMaxLength(500);

    //        entity.Property(e => e.RadiusLocator).HasColumnType("decimal(18, 0)");

    //        entity.Property(e => e.RegularAvailability)
    //            .HasMaxLength(1000)
    //            .IsUnicode(false);

    //        entity.HasOne(d => d.User)
    //            .WithMany(p => p.Profiles)
    //            .HasForeignKey(d => d.UserId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_Profile_UserID");
    //    });

    //    modelBuilder.Entity<User>(entity =>
    //    {
    //        entity.HasIndex(e => e.Email, "UQ_Email")
    //            .IsUnique();

    //        entity.HasIndex(e => e.UserName, "UQ_UserName")
    //            .IsUnique();

    //        entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

    //        entity.Property(e => e.ConfirmPassword).HasMaxLength(50);

    //        entity.Property(e => e.CreatedDate).HasColumnType("datetime");

    //        entity.Property(e => e.Email).HasMaxLength(550);

    //        entity.Property(e => e.Name).HasMaxLength(250);

    //        entity.Property(e => e.Password).HasMaxLength(50);

    //        entity.Property(e => e.Telephone).HasMaxLength(50);

    //        entity.Property(e => e.UserName).HasMaxLength(150);
    //    });

    //    modelBuilder.Entity<UserImage>(entity =>
    //    {
    //        entity.ToTable("UserImage");

    //        entity.Property(e => e.UserImageId).HasDefaultValueSql("(newid())");

    //        entity.Property(e => e.CreatedDate).HasColumnType("datetime");

    //        entity.Property(e => e.ImageExtension).HasMaxLength(50);

    //        entity.Property(e => e.ImageTitle).HasMaxLength(50);

    //        entity.HasOne(d => d.User)
    //            .WithMany(p => p.UserImages)
    //            .HasForeignKey(d => d.UserId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_UserImage_UserId");
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

   // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
