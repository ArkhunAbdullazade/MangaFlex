using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("ocelot.json", false, reloadOnChange: true);
builder.Services.AddOcelot();
builder.Services.AddSignalR();

builder.Services.AddCors();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(options => {
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
});

await app.UseOcelot();

app.Run();

