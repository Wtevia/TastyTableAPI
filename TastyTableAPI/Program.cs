using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddScoped<Core.AppSettings>(builder => 
    configuration.GetSection("AppSettings").Get<Core.AppSettings>() 
    ?? throw new InvalidOperationException());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
BLL.DependencyRegistrar.ConfigureServices(builder.Services);

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

Console.WriteLine(configuration.GetSection("GoogleAuth")["ClientId"]);
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer()//(options => { })
    .AddGoogle(options =>
    {
        options.ClientId = configuration.GetSection("GoogleAuth")["ClientId"]!;
        options.ClientSecret = configuration.GetSection("GoogleAuth")["ClientSecret"]!;
    });
builder.Services.AddAuthorization(options =>
{
    
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
