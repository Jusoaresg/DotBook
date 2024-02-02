using Book.DataAccess.Data;
using Book.DataAccess.Repository;
using Book.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client.Extensibility;
using Book.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Book.DataAccess.Services;
using System.Configuration;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL Connection String
var dbConfig = builder.Configuration.GetSection("Database");
var host = dbConfig.GetValue<string>("Host");
var database = dbConfig.GetValue<string>("Database");
var user = dbConfig.GetValue<string>("User");
var password = dbConfig.GetValue<string>("Password");

var connectionString = $"Host={host}; Database={database}; Username={user}; Password={password}";

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
    
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

await DatabaseManagementService.MigrationInitialization(app);

app.Run();
