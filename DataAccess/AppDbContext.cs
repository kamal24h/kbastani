using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace DataAccess;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
      
    //private readonly IHttpContextAccessor _httpContextAccessor;

    /* Define a DbSet for each entity of the application */
    public DbSet<BlogCategory> BlogCategories { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<BlogPostTag> BlogPostTags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ForumCategory> ForumCategories { get; set; }
    public DbSet<ForumPost> ForumPosts { get; set; }
    public DbSet<ForumThread> ForumThreads { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTech> ProjectTechs { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Education> Educations { get; set; }
    //public DbSet<Education> Educations => Set<Education>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Customize ASP.NET Core Identity table names and relationships if needed
        modelBuilder.Entity<AppUser>(b =>
            {
                b.ToTable("AppUsers");
                b.Property(u => u.DisplayName).HasMaxLength(256);
            });

        modelBuilder.Entity<AppRole>(b =>
            {
                b.ToTable("AppRoles");
                b.Property(r => r.Description).HasMaxLength(256);
            });

        modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("AppUserRoles");
            });

        modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("AppUserClaims");
            });

        modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("AppUserLogins");
            });

        modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("AppRoleClaims");
            });

        modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("AppUserTokens");
            });

        // Optional: Add seed data (default admin role/user)
        modelBuilder.Entity<AppRole>().HasData(new AppRole()
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN",
                Description = "Administrator Role"
            });

        modelBuilder.Entity<BlogPostTag>().HasKey(x => new { x.PostId, x.TagId });
        modelBuilder.Entity<BlogPost>()
            .HasIndex(p => p.Slug)
            .IsUnique();
        modelBuilder.Entity<BlogCategory>()
            .HasIndex(c => c.Slug)
            .IsUnique();
        modelBuilder.Entity<ForumCategory>()
            .HasIndex(c => c.Slug)
            .IsUnique();

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(d => d.Post)
                  .WithMany(p => p.Comments)
                  .HasForeignKey(d => d.PostId)
                  .OnDelete(DeleteBehavior.NoAction)
                  .IsRequired();
        });

        modelBuilder.Entity<BlogPostTag>(entity =>
        {
            entity.HasOne(d => d.Post)
                  .WithMany(p => p.Tags)
                  .HasForeignKey(d => d.PostId)
                  .OnDelete(DeleteBehavior.NoAction)
                  .IsRequired();
        });

        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasOne(d => d.Category)
                  .WithMany(p => p.Posts)
                  .HasForeignKey(d => d.CategoryId)
                  .OnDelete(DeleteBehavior.NoAction)
                  .IsRequired();
        });

        modelBuilder.Entity<ForumPost>(entity =>
        {
            entity.HasOne(d => d.Thread)
                  .WithMany(p => p.Posts)
                  .HasForeignKey(d => d.ThreadId)
                  .OnDelete(DeleteBehavior.NoAction)
                  .IsRequired();
        });

        modelBuilder.Entity<ForumThread>(entity =>
        {
            entity.HasOne(d => d.Category)
                  .WithMany(p => p.Threads)
                  .HasForeignKey(d => d.CategoryId)
                  .OnDelete(DeleteBehavior.NoAction)
                  .IsRequired();
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasOne(d => d.Profile)
                  .WithMany(p => p.Projects)
                  .HasForeignKey(d => d.UserProfileId)
                  .OnDelete(DeleteBehavior.NoAction)
                  .IsRequired();
        });

        modelBuilder.Entity<ProjectTech>(entity =>
        {
            entity.HasOne(d => d.Project)
                  .WithMany(p => p.Techs)
                  .HasForeignKey(d => d.ProjectId)
                  .OnDelete(DeleteBehavior.NoAction)
                  .IsRequired();
        });


        //modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
        //{
        //    b.Property<string>("Id")
        //        .HasColumnType("nvarchar(450)");

        //    b.Property<string>("ConcurrencyStamp")
        //        .IsConcurrencyToken()
        //        .HasColumnType("nvarchar(max)");

        //    b.Property<string>("Name")
        //        .HasColumnType("nvarchar(256)")
        //        .HasMaxLength(256);

        //    b.Property<string>("NormalizedName")
        //        .HasColumnType("nvarchar(256)")
        //        .HasMaxLength(256);

        //    b.HasKey("Id");
        //    b.ToTable("AppRoles");
        //});

        //modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
        //{
        //    b.Property<int>("Id")
        //        .ValueGeneratedOnAdd()
        //        .HasColumnType("int")
        //        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        //    b.Property<string>("ClaimType")
        //        .HasColumnType("nvarchar(max)");

        //    b.Property<string>("ClaimValue")
        //        .HasColumnType("nvarchar(max)");

        //    b.Property<string>("RoleId")
        //        .IsRequired()
        //        .HasColumnType("nvarchar(450)");

        //    b.HasKey("Id");

        //    b.HasIndex("RoleId");

        //    b.ToTable("AppRoleClaims");
        //});

        //modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
        //{
        //    b.Property<string>("Id")
        //        .HasColumnType("nvarchar(450)");

        //    b.HasKey("Id");

        //    b.Property<int>("AccessFailedCount")
        //        .HasColumnType("int");

        //    b.Property<string>("ConcurrencyStamp")
        //        .IsConcurrencyToken()
        //        .HasColumnType("nvarchar(max)");

        //    b.Property<string>("Email")
        //        .HasColumnType("nvarchar(256)")
        //        .HasMaxLength(256);

        //    b.Property<bool>("EmailConfirmed")
        //        .HasColumnType("bit");

        //    b.Property<bool>("LockoutEnabled")
        //        .HasColumnType("bit");

        //    b.Property<DateTimeOffset?>("LockoutEnd")
        //        .HasColumnType("datetimeoffset");

        //    b.Property<string>("NormalizedEmail")
        //        .HasColumnType("nvarchar(256)")
        //        .HasMaxLength(256);

        //    b.Property<string>("NormalizedUserName")
        //        .HasColumnType("nvarchar(256)")
        //        .HasMaxLength(256);

        //    b.Property<string>("PasswordHash")
        //        .HasColumnType("nvarchar(max)");

        //    b.Property<string>("PhoneNumber")
        //        .HasColumnType("nvarchar(max)");

        //    b.Property<bool>("PhoneNumberConfirmed")
        //        .HasColumnType("bit");

        //    b.Property<string>("SecurityStamp")
        //        .HasColumnType("nvarchar(max)");

        //    b.Property<bool>("TwoFactorEnabled")
        //        .HasColumnType("bit");

        //    b.Property<string>("UserName")
        //        .HasColumnType("nvarchar(256)")
        //        .HasMaxLength(256);

        //    b.Property<string>("FirstName")
        //        .HasColumnType("nvarchar(50)")
        //        .HasMaxLength(50);

        //    b.Property<string>("LastName")
        //        .HasColumnType("nvarchar(50)")
        //        .HasMaxLength(50);

        //    b.Property<string>("DisplayUserName")
        //        .HasColumnType("nvarchar(50)")
        //        .HasMaxLength(50);          

        //b.ToTable("AppUsers");            
        //});

        //modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
        //{
        //    b.Property<int>("Id")
        //        .ValueGeneratedOnAdd()
        //        .HasColumnType("int")
        //        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        //    b.Property<string>("ClaimType")
        //        .HasColumnType("nvarchar(max)");

        //    b.Property<string>("ClaimValue")
        //        .HasColumnType("nvarchar(max)");

        //    b.Property<string>("UserId")
        //        .IsRequired()
        //        .HasColumnType("nvarchar(450)");

        //    b.HasKey("Id");

        //    b.HasIndex("UserId");

        //    b.ToTable("AppUserClaims");
        //});

        //modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
        //{
        //    b.Property<string>("LoginProvider")
        //        .HasColumnType("nvarchar(128)")
        //        .HasMaxLength(128);

        //    b.Property<string>("ProviderKey")
        //        .HasColumnType("nvarchar(128)")
        //        .HasMaxLength(128);

        //    b.Property<string>("ProviderDisplayName")
        //        .HasColumnType("nvarchar(max)");

        //    b.Property<string>("UserId")
        //        .IsRequired()
        //        .HasColumnType("nvarchar(450)");

        //    b.HasKey("LoginProvider", "ProviderKey");

        //    b.HasIndex("UserId");

        //    b.ToTable("AppUserLogins");
        //});

        //modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
        //{
        //    b.Property<string>("UserId")
        //        .HasColumnType("nvarchar(450)");

        //    b.Property<string>("RoleId")
        //        .HasColumnType("nvarchar(450)");

        //    b.HasKey("UserId", "RoleId");

        //    b.HasIndex("RoleId");

        //    b.ToTable("AppUserRoles");
        //});

        //modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
        //{
        //    b.Property<string>("UserId")
        //        .HasColumnType("nvarchar(450)");

        //    b.Property<string>("LoginProvider")
        //        .HasColumnType("nvarchar(128)")
        //        .HasMaxLength(128);

        //    b.Property<string>("Name")
        //        .HasColumnType("nvarchar(128)")
        //        .HasMaxLength(128);

        //    b.Property<string>("Value")
        //        .HasColumnType("nvarchar(max)");

        //    b.HasKey("UserId", "LoginProvider", "Name");

        //    b.ToTable("AppUserTokens");
        //});

        //modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
        //{
        //    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
        //        .WithMany()
        //        .HasForeignKey("RoleId")
        //        .OnDelete(DeleteBehavior.Cascade)
        //        .IsRequired();
        //});

        //modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
        //{
        //    b.HasOne("AppUser", null)
        //        .WithMany()
        //        .HasForeignKey("UserId")
        //        .OnDelete(DeleteBehavior.Cascade)
        //        .IsRequired();
        //});

        //modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
        //{
        //    b.HasOne("AppUser")
        //        .WithMany()
        //        .HasForeignKey("UserId")
        //        .OnDelete(DeleteBehavior.Cascade)
        //        .IsRequired();
        //});

        //modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
        //{
        //    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
        //        .WithMany()
        //        .HasForeignKey("RoleId")
        //        .OnDelete(DeleteBehavior.Cascade)
        //        .IsRequired();

        //    b.HasOne("AppUser", null)
        //        .WithMany()
        //        .HasForeignKey("UserId")
        //        .OnDelete(DeleteBehavior.Cascade)
        //        .IsRequired();
        //});

        //modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
        //{
        //    b.HasOne("AppUser", null)
        //        .WithMany()
        //        .HasForeignKey("UserId")
        //        .OnDelete(DeleteBehavior.Cascade)
        //        .IsRequired();
        //});



        //modelBuilder.Entity<Unit>(entity =>
        //{
        //    entity.HasOne(d => d.Building)
        //          .WithMany(p => p.Units)
        //          .HasForeignKey(d => d.BuildingId)
        //          .OnDelete(DeleteBehavior.NoAction)
        //          .IsRequired();
        //});

        //modelBuilder.Entity<Resident>(entity =>
        //{
        //    entity.HasOne(d => d.Unit)
        //          .WithMany(p => p.Residents)
        //          .HasForeignKey(d => d.ResidentId)
        //          .OnDelete(DeleteBehavior.NoAction)
        //          .IsRequired();
        //});

        //modelBuilder.Entity<Resident>(entity =>
        //{
        //    entity.HasIndex(d => d.ResidentId, "IX_Resident_Id")
        //          .IsUnique();
        //});



        ////admin config
        //string ADMIN_ID = "4b859c11-79f9-4104-9fe9-276aeaf5f115";
        //string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

        ////seed admin role
        //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        //{
        //    Name = "SuperAdmin",
        //    NormalizedName = "SUPERADMIN",
        //    Id = ROLE_ID,
        //    ConcurrencyStamp = ROLE_ID
        //});

        //////create user
        //var appUser = new AppUser
        //{
        //    Id = ADMIN_ID,
        //    Email = "kbastani@gmail.com",
        //    EmailConfirmed = true,
        //    FirstName = "Kamal",
        //    LastName = "Bastani",
        //    UserName = "kbastani@gmail.com",
        //    NormalizedUserName = "KBASTANI@GMAIL.COM"
        //};

        //////set user password
        //PasswordHasher<AppUser> ph = new();
        //appUser.PasswordHash = ph.HashPassword(appUser, "Operation@123");

        ////seed user
        //modelBuilder.Entity<AppUser>().HasData(appUser);
        //////modelBuilder.Entity<IdentityUser>().HasData(appUser);

        //////set user role to admin
        //modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        //{
        //    RoleId = ROLE_ID,
        //    UserId = ADMIN_ID
        //});
    }
}
