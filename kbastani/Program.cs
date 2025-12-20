using Common.Configuration;
using Common.DependencyInjection;
using DataAccess;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------
// Add services to the container.
// ---------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ---------------------------------------------------
// Add Identity Core Services
// ---------------------------------------------------

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;

    // User settings
    options.User.RequireUniqueEmail = true;

    // Sign-in settings
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<AppDbContext>() // link Identity to EF Core
.AddUserManager<UserManager<AppUser>>()     // explicitly add UserManager
.AddSignInManager<SignInManager<AppUser>>() // explicitly add SignInManager
.AddRoleManager<RoleManager<AppRole>>()
.AddDefaultTokenProviders(); // needed for password reset, email confirmation, etc.

// ---------------------------------------------------
// Add authentication cookie configuration (optional)
// ---------------------------------------------------
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});

// ---------------------------------------------------
// Other Important Configs
// ---------------------------------------------------
builder.Services.AddAuthenticationCore();
builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("CanPublish", p => p.RequireRole("Admin"));
    options.AddPolicy("CanComment", p => p.RequireAuthenticatedUser());
    options.AddPolicy("CanModerate", p => p.RequireRole("Admin"));
    options.AddPolicy("CanLockThread", p => p.RequireRole("Admin"));
});

//builder.Services.AddCors(option => option
//.AddPolicy("MuCorsPolicy", plc =>
//    plc.AllowAnyOrigin()
//    .AllowAnyMethod()
//    .AllowAnyHeader()
//    ));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Config mappings
MapperConfig.ConfiguerServices(builder.Services, builder.Configuration);
// Config Dependency Injections
Bootstrap.ConfigureService(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Seed Initial Data
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

    await SeedData.InitializeAsync(userManager, roleManager);
}
catch (Exception ex)
{
    Console.WriteLine("Error seeding data: " + ex.Message);
}

app.Run();
