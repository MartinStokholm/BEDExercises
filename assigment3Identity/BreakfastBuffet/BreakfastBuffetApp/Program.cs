using BreakfastBuffetApp.Data;
using BreakfastBuffetApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 0;
});

builder.Services.AddSignalR();

builder.Services.AddRazorPages();

// inject the breakfast controller/service class for use in the razor pages
builder.Services.AddTransient<IBreakfastService, BreakfastService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Reception",
        policyBuilder => policyBuilder.RequireClaim("ReceptionAccess")
    );
    options.AddPolicy("Waiter",
        policyBuilder => policyBuilder.RequireClaim("WaiterAccess"));
    
    options.AddPolicy("Kitchen",
        policyBuilder => policyBuilder.RequireClaim("KitchenAccess")
    );
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

app.MapHub<KitchenReportService>("/KitchenReportService");

app.Run();
