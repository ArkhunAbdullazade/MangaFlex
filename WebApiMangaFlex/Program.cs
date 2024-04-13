using WebApiMangaFlex.Mangas.Data.Mangas.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
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
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddScoped<IMangaService, MangaService>();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.InjectStylesheet("/swagger-ui/themes/3.x/custom.css");
});
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
