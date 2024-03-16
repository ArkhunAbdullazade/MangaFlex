using MangaFlex.Infrastructure.Data.User.Services;
using MangaFlex.Infrastructure.Data.DBContext;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Infrastructure.Data.Mangas.Services;

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

builder.Services.AddScoped<IUserService, UserService>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
