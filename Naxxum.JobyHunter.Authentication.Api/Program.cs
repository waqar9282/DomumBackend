
using Authentication.Application;
using Authentication.Application.Common.Interfaces;
using Authentication.Infra;
using Authentication.Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();



// For authentication
var _key = builder.Configuration["Jwt:Key"];
var _issuer = builder.Configuration["Jwt:Issuer"];
var _audience = builder.Configuration["Jwt:Audience"];
var _expirtyMinutes = builder.Configuration["Jwt:ExpiryMinutes"];

// Validate JWT configuration early to avoid null reference exceptions at runtime
if (string.IsNullOrWhiteSpace(_key) || string.IsNullOrWhiteSpace(_issuer)
    || string.IsNullOrWhiteSpace(_audience) || string.IsNullOrWhiteSpace(_expirtyMinutes))
{
    throw new InvalidOperationException("JWT configuration is missing. Please set Jwt:Key, Jwt:Issuer, Jwt:Audience and Jwt:ExpiryMinutes in configuration.");
}



// Configuration for token
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = _audience,
        ValidIssuer = _issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
    // ClockSkew should be a small tolerance window, not the token expiry value.
    // Use a small tolerance (e.g. 5 minutes) to account for minor clock differences.
    ClockSkew = TimeSpan.FromMinutes(5)

    };
});


// Inside builder.Services configuration:
builder.Services.AddScoped<IAccessService, AccessService>();
// Dependency injection with key
builder.Services.AddSingleton<ITokenGenerator>(new TokenGenerator(_key, _issuer, _audience, _expirtyMinutes));


// Include Application Dependency
builder.Services.AddApplicationServices();
// Include Infrastructur Dependency
builder.Services.AddInfrastructure(builder.Configuration);









builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        // Allow the API to be called from the Angular dev server (http://localhost:4200)
        // and the existing backend origin. Adjust or move to configuration for production.
        builder
            .WithOrigins("https://localhost:7219", "http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
