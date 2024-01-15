using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spotify.Models;
using Spotify.Models.context;
using Spotify.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddRazorPages();

// Add services to the container.
services.AddControllersWithViews();
services.AddDbContextPool<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

services.AddControllersWithViews().AddRazorRuntimeCompilation();
services.AddScoped<IAccountService, AccountService>();
services.AddTransient<IEmailSender, EmailSender>();
services.AddTransient<IProfile, ProfileService>();
services.AddTransient<IHome, HomeService>();
services.AddTransient<ISong, SongService>();
services.AddTransient<IArtist, ArtistService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "/Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();