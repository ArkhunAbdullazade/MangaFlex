using MangaFlex.AdminApp.Middleware;
using MangaFlex.Core.Data.FriendShip.Repository;
using MangaFlex.Core.Data.LastWatches.Repository;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Repository;
using MangaFlex.Infrastructure.Data.DBContext;
using MangaFlex.Infrastructure.Data.FriendShip.Repository;
using MangaFlex.Infrastructure.Data.LastWatches.Repository;
using MangaFlex.Infrastructure.Data.Mangas.Services;
using MangaFlex.Infrastructure.Data.Users.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MangaFlexDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("MangaFlex"),
    useSqlOptions =>
    {
        useSqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
    });
});

builder.Services.AddMediatR(configurations => {
    configurations.RegisterServicesFromAssembly(Assembly.Load("MangaFlex.Core"));
});

builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<MangaFlexDbContext>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFriendShipRepository, FriendShipRepository>();
builder.Services.AddScoped<ILastWatchesRepository, LastWatchesRepository>();
builder.Services.AddScoped<IMangaService, MangaService>();

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseMiddleware<CheckIsBannedMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Identity}/{action=Home}");


app.Run();
