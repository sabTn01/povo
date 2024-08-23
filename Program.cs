using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using POVO.Backend.Configurations;
using POVO.Backend.Infrastructure.Contexts;
using POVO.Backend.Infrastructure.Exceptions;

var builder = WebApplication.CreateBuilder(args);
// Configure Logging
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));


// Configure Options
var povoConfigurationOptions = builder.Configuration.GetSection("Povo").Get<PovoConfigurationOptions>();
builder.Services.Configure<PovoConfigurationOptions>(builder.Configuration.GetSection("Povo"));

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register service in DI
DependencyInjectionConfiguration.Configure(builder.Services);

// Configure database
builder.Services.AddDbContext<PovoDbContext>(options =>
    options.UseNpgsql(povoConfigurationOptions.Infrastructure.ConnectionString));

// Create AutoMapper
var config = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure Custom Middelwares
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
