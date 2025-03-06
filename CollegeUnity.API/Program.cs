using CollegeUnity.API.Filters;
using CollegeUnity.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CollegeUnity.Core.Constants;
using CollegeUnity.API;
using CollegeUnity.EF.Repositories;
using CollegeUnity.Services;
using System.Runtime.CompilerServices;
using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.EF_Contract;
using EmailService;
using EmailService.EmailService;
using CollegeUnity.Contract.SharedFeatures.Authentication;
using CollegeUnity.Services.SharedFeatures.Authentication;
using CollegeUnity.Contract.StudentFeatures.Account;
using CollegeUnity.Services.StudentFeatures.Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ModelValidateActionFilter>();
    options.Filters.Add<EditResponseActionFilter>();
});

builder.Services.AddDbContext<CollegeUnityDbContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("FaisalLocal"));
    //options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
});

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IEmailServices, EmailServices>();


// Features DI
builder.Services.AddFeaturesID();


// jwt authentication
builder.Services.AddCustomJwtAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerCustomeGen();

var app = builder.Build();

//app.ConfigureCustomeExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
