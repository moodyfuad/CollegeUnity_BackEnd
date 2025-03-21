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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<AddStatusCodeToResponseActionFilter>();
    options.Filters.Add<AddPaginationToResponseActionFilter>();
});

builder.Services.ConfigureModelValidationResponse();

#region this part is moved to CollegeUnity.EF.EntityFrameworkDIExtensions
//builder.Services.AddDbContext<CollegeUnityDbContext>(options =>
//{
//    //options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
//    //options.UseSqlServer(builder.Configuration.GetConnectionString("FaisalLocal"));
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
//});

#endregion

// EF DI
builder.Services.AddCollegeUnityDbContext(builder.Configuration);
builder.Services.AddEFLayerDI();

builder.Services.AddScoped<IServiceManager, ServiceManager>();

// Features DI
builder.Services.AddFeaturesLayerDI();


// jwt authentication
builder.Services.AddCustomJwtAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerCustomeGen();

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.ConfigureCustomeExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();

var filesPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Files");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(filesPath),
    RequestPath = "/Files"
});

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
