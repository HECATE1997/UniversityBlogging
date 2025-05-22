using Auth;
using Auth.Models;
using Auth.Persistence;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryClients(IdentityConfig.Clients)
    .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
    .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
    .AddDeveloperSigningCredential();

builder.Services.AddControllers();
var app = builder.Build();

app.UseIdentityServer();
app.MapControllers();

app.Run();
