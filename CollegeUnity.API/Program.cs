using CollegeUnity.API.Filters;
using CollegeUnity.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CollegeUnity.Core.Constants;
using CollegeUnity.API;
using CollegeUnity.Contract;
using CollegeUnity.EF.Repositories;
using CollegeUnity.Services;
using System.Runtime.CompilerServices;
using CollegeUnity.API.Middlerware_Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ModelValidateActionFilter>();
});
// 

builder.Services.AddDbContext<CollegeUnityDbContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("FaisalLocal"));
});

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();

//jwt authentication
builder.Services.AddCustomJwtAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerCustomeGen();

var app = builder.Build();

app.ConfigureCustomeExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
