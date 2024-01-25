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
services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue; 
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
services.AddTransient<IUpload, UploadService>();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User" };

    foreach(var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}
app.Run();