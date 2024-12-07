using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();


builder.Services.AddTransient<CountryService>();
builder.Services.AddTransient<ResortService>();
builder.Services.AddTransient<AccommodationService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services
    .AddAuthentication();
builder.Services.AddAuthorization();

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

app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Countries}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "resort",
    pattern: "{controller=Resorts}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "accommodation",
    pattern: "{controller=Accommodations}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

using var scope = app.Services.CreateScope();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

var roles = new[] { "User", "Manager", "Admin" };

foreach (var role in roles)
{
    if (!await roleManager.RoleExistsAsync(role))
        await roleManager.CreateAsync(new IdentityRole(role));
}

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

var email = "admin@admin.com";
var password = "Superadmin228";

if (await userManager.FindByEmailAsync(email) == null)
{
    var user = new User();

    user.FirstName = "Админ";
    user.LastName = "Админов";
    user.Email = email;
    user.UserName = email;

    var result = await userManager.CreateAsync(user, password);

    await userManager.AddToRoleAsync(user, "Admin");
}

app.Run();
