using System.Security.Claims;
using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var staffConnectionString = builder.Configuration.GetConnectionString("staffDbConnection");
var hotelConnectionString = builder.Configuration.GetConnectionString("hotelDbConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(staffConnectionString));
builder.Services.AddDbContext<HotelDataContext>(options =>
    options.UseSqlServer(hotelConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

// My own service
builder.Services.AddTransient<IReservationService, ReservationService>();

Claim[] claimTypes = new Claim[3]
{
    new Claim("ReceptionAccess",""),
    new Claim("WaiterAccess", ""),
    new Claim("KitchenAccess", "")
};

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReceptionStaff",
        policyBuilder => policyBuilder.RequireClaim("ReceptionAccess")
    );
    options.AddPolicy("WaiterStaff",
        policyBuilder => policyBuilder.RequireClaim("WaiterAccess"));
});
    

// Disable most requires so that we can test this
// Only requirements: Password length >= 6, 1 Uppercase, 1 Lowercase
builder.Services.Configure<IdentityOptions>(
    options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredUniqueChars = 0;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedAccount = false;
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
