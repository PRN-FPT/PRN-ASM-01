using ASM_01.DataAccessLayer;
using ASM_01.DataAccessLayer.Persistence;
using ASM_01.DataAccessLayer.Repositories;
using ASM_01.DataAccessLayer.Repositories.Abstract;
using ASM_01.BusinessLayer.Services;
using ASM_01.BusinessLayer.Services.Abstract;
using ASM_01.BusinessLayer.Mappers;
using ASM_01.BusinessLayer.Mappers.Abstract;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EVRetailsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection for repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IDealerRepository, DealerRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IDistributionRequestRepository, DistributionRequestRepository>();

// Dependency Injection for mappers
builder.Services.AddScoped<IVehicleMapper, VehicleMapper>();
builder.Services.AddScoped<IDistributionMapper, DistributionMapper>();
builder.Services.AddScoped<IDealerMapper, DealerMapper>();

// Dependency Injection for services
builder.Services.AddScoped<ISimpleAuthService, SimpleAuthService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IDistributionManagementService, DistributionManagementService>();
builder.Services.AddScoped<IDealerInventoryService, DealerInventoryService>();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/Denied";
        options.Cookie.Name = "EVRetails.auth";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EVRetailsDbContext>();
    await MigrateDatabase.ApplyMigrations(dbContext);
}

app.Run();
