using System.Text;
using Core.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    // .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddSingleton<Core.AppSettings>(provider => 
    configuration.GetSection("AppSettings").Get<Core.AppSettings>() 
    ?? throw new InvalidOperationException());

builder.Services.AddControllers();

builder.Services.AddAuthentication(x =>  
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>  
    {  
        x.RequireHttpsMetadata = false;  
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters  
        {  
            ValidIssuer = builder.Configuration["Jwt:Issuer"],  
            ValidAudience = builder.Configuration["Jwt:Audience"],  
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),  
            ValidateIssuer = true,  
            ValidateAudience = true,  
            ValidateIssuerSigningKey = true,  
            ValidateLifetime = true,  
            ClockSkew = TimeSpan.Zero,  
        };  
    })
    .AddGoogle(options =>
    {
        options.ClientId = configuration.GetSection("GoogleAuth")["ClientId"]!;
        options.ClientSecret = configuration.GetSection("GoogleAuth")["ClientSecret"]!;
        options.SaveTokens = true;
        //options.CallbackPath = "/api/Auth/signinGoogle";
    });
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string",
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

BLL.DependencyRegistrar.ConfigureServices(builder.Services);
// builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//     .AddEntityFrameworkStores<RestaurantContext>()
//     .AddDefaultTokenProviders();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// app.UseDefaultFiles();
// app.UseStaticFiles();

app.UseRouting();
app.UseCors();

app.UseAuthentication();  
app.UseAuthorization();  
  
app.MapControllers();

app.Run();
