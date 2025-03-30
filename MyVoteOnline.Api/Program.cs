using Microsoft.EntityFrameworkCore;
using MyVoteOnline.Services.Interfaces;
using MyVoteOnline.Services.Repositories;
using MyVotOnline.DataBaseLayer.DataContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VoteContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoginRepository, UserLoginRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure logging
var logger = app.Services.GetRequiredService<ILogger<Program>>();
var environment = app.Environment.EnvironmentName;
logger.LogInformation($"Application environment: {environment}");

// Enable Swagger only in non-production environments
if (!app.Environment.IsProduction())
{
    logger.LogInformation("Enabling Swagger in Development environment.");
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyVoteOnline API V1");
    });
}
else
{
    logger.LogInformation("Swagger is disabled for non-Development environments.");
}

var devport = builder.Configuration["DockerPort:Port"];
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

if (isDocker)
{
	app.Urls.Add($"http://*:{devport}");
	logger.LogInformation($"Application is running inside Docker on port: {devport}");
}
else
{
	// Running locally, don't bind to the Docker-specific port
	logger.LogInformation("Running locally, no Docker port configuration applied.");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
