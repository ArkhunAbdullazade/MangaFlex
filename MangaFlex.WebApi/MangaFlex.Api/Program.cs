using MangaFlex.Api.Hubs;
using MangaFlex.Core.Data.FriendsChat.Repository;
using MangaFlex.Core.Data.FriendShip.Repository;
using MangaFlex.Core.Data.JWT.Options;
using MangaFlex.Core.Data.LastWatches.Repository;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Core.Data.Message.Repository;
using MangaFlex.Core.Data.RefreshToken.Repository;
using MangaFlex.Core.Data.UserMangas.Repositories;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Repository;
using MangaFlex.Infrastructure.Data.DBContext;
using MangaFlex.Infrastructure.Data.FriendsChat.Repository;
using MangaFlex.Infrastructure.Data.FriendShip.Repository;
using MangaFlex.Infrastructure.Data.LastWatches.Repository;
using MangaFlex.Infrastructure.Data.Mangas.Services;
using MangaFlex.Infrastructure.Data.Message.Repository;
using MangaFlex.Infrastructure.Data.RefreshToken;
using MangaFlex.Infrastructure.Data.UserMangas.Repositories;
using MangaFlex.Infrastructure.Data.Users.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var jwtOptionsSection = builder.Configuration
    .GetSection("JwtOptions");

var jwtOptions = jwtOptionsSection.Get<JwtOptions>() ?? throw new Exception("Couldn't create jwt options object");

builder.Services.Configure<JwtOptions>(jwtOptionsSection);
builder.Services.AddSingleton(jwtOptions);

builder.Services.AddControllers();

builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    const string scheme = "Bearer";

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MangaFlex API",
        Description = "A Web API for managing MangaFlex API",
        Contact = new OpenApiContact
        {
            Name = "Notion",
            Url = new Uri("https://www.notion.so/929bccbbb60c4075b89f55be7459936e")
        },
        License = new OpenApiLicense
        {
            Name = "Github",
            Url = new Uri("https://github.com/ArkhunAbdullazade/MangaFlex")
        }
    });

    options.AddSecurityDefinition(
        name: scheme,

        new OpenApiSecurityScheme()
        {
            Description = "Enter here jwt token with Bearer",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = scheme
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement() {
            {
                new OpenApiSecurityScheme() {
                    Reference = new OpenApiReference() {
                        Id = scheme,
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new string[] {}
            }
        }
    );

    // var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


builder.Services.AddAuthentication((o) =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(jwtOptions.KeyInBytes),

            ValidateLifetime = true,

            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,

            ValidateIssuer = true,
            ValidIssuers = jwtOptions.Issuers,
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .Build();
});

builder.Services.AddDbContext<MangaFlexDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("MangaFlex"),
    useSqlOptions =>
    {
        useSqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
    });
});

builder.Services.AddMediatR(configurations =>
{
    configurations.RegisterServicesFromAssembly(Assembly.Load("MangaFlex.Core"));
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<MangaFlexDbContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFriendShipRepository, FriendShipRepository>();
builder.Services.AddScoped<ILastWatchesRepository, LastWatchesRepository>();
builder.Services.AddScoped<IUserMangaRepository, UserMangaRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddHttpClient<IMangaService, MangaService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IFriendsChatRepository, FriendsChatRepository>();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});



var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.InjectStylesheet("/swagger-ui/themes/3.x/custom.css");
});
app.UseResponseCompression();
app.UseCors("AllowSpecificOrigin");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chathub");
    endpoints.MapHub<ProfileHub>("/profilehub");
    endpoints.MapControllers();
});
app.UseStaticFiles();

app.UseHttpsRedirection();


app.Run();