using MangaFlex.Infrastructure.Data.DBContext;
using MangaFlex.Core.Data.Users.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Infrastructure.Data.Mangas.Services;
using MangaFlex.Infrastructure.Data.Users.Repository;
using MangaFlex.Core.Data.Users.Repository;
using MangaFlex.Core.Data.FriendShip.Repository;
using MangaFlex.Infrastructure.Data.FriendShip.Repository;
using MangaFlex.Core.Data.LastWatches.Repository;
using MangaFlex.Infrastructure.Data.LastWatches.Repository;

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

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
    //options.AccessDeniedPath = "/ErrorPage/Error";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
