using System.Globalization;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
var builder=WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(o=>o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
builder.Services.AddDbContext<ApplicationDbContext>(o=>o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(o=>{o.Password.RequiredLength=8;o.Password.RequireUppercase=true;o.Password.RequireLowercase=true;o.Password.RequireDigit=true;o.User.RequireUniqueEmail=true;o.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(10);o.Lockout.MaxFailedAccessAttempts=5;}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(o=>{o.LoginPath="/Account/Login";o.Cookie.HttpOnly=true;o.Cookie.SameSite=SameSiteMode.Lax;o.Cookie.SecurePolicy=CookieSecurePolicy.Always;});
builder.Services.AddHttpContextAccessor();builder.Services.AddScoped<ICurrentUserService,CurrentUserService>();builder.Services.AddScoped<ITransactionService,TransactionService>();builder.Services.AddScoped<ICategoryService,CategoryService>();builder.Services.AddScoped<IBudgetService,BudgetService>();builder.Services.AddScoped<IDashboardService,DashboardService>();builder.Services.AddScoped<IReportService,ReportService>();builder.Services.AddScoped<ICsvExportService,CsvExportService>();
var app=builder.Build();if(!app.Environment.IsDevelopment()){app.UseExceptionHandler("/Home/Error");app.UseHsts();}app.UseStatusCodePagesWithReExecute("/Home/Status/{0}");app.UseHttpsRedirection();app.UseStaticFiles();app.UseRequestLocalization(new RequestLocalizationOptions{DefaultRequestCulture=new RequestCulture("en-IN"),SupportedCultures=new[]{new CultureInfo("en-IN")},SupportedUICultures=new[]{new CultureInfo("en-IN")}});app.UseRouting();app.UseAuthentication();app.UseAuthorization();app.MapControllerRoute("default","{controller=Dashboard}/{action=Index}/{id?}");
if(!app.Environment.IsEnvironment("Testing")){using var scope=app.Services.CreateScope();var db=scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();try{await db.Database.MigrateAsync();await DbSeeder.SeedAsync(db);}catch(Exception ex){app.Logger.LogError(ex,"Database initialization failed");throw;}}
app.Run();public partial class Program {}
