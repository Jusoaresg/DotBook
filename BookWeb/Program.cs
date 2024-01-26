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

/*
// SQL Server Connection String
var server = builder.Configuration["DbServer"] ?? "127.0.0.1";
var port = builder.Configuration["DbPort"] ?? "1433";
var user = builder.Configuration["DbUser"] ?? "SA";
var password = builder.Configuration["Password"] ?? "yourStrong(!)Password";
var database = builder.Configuration["Database"] ?? "bookBase";
var connectionString = $"Server={server},{port};Database={database};User ID={user};Password={password};TrustServerCertificate=True";
*/

// PostgreSQL Connection String
var host = builder.Configuration["DbHost"] ?? "127.0.0.1";
var database = builder.Configuration["Database"] ?? "dotbookbase";
var user = builder.Configuration["DbUser"] ?? "admin";
var password = builder.Configuration["Password"] ?? "admin";
var connectionString = $"Host={host}; Database={database}; Username={user}; Password={password}";

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer(connectionString));
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

//DatabaseManagementService.MigrationInitialization(app);

app.Run();
