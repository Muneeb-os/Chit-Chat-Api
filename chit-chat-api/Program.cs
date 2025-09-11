using chit_chat_api.DB_Data;
using chit_chat_api.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var JwtSetting = builder.Configuration.GetSection("JWTSetting");

// add dbcontext
builder.Services.AddDbContext<_dbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

// add  authentication
builder.Services.AddAuthentication(x => { x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSetting.GetSection("SecurityKey").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddScoped<GenerateJwtToken>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title="Chit Chat Api",Version="v1" }); });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())


{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chit Chat Api");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
