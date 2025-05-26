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
using CollegeUnity.Services.Hubs;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<AddStatusCodeToResponseActionFilter>();
    options.Filters.Add<AddPaginationToResponseActionFilter>();
});

builder.Services.ConfigureModelValidationResponse();

// EF DI
builder.Services.AddCollegeUnityDbContext(builder.Configuration);
builder.Services.AddEFLayerDI();

//builder.Services.AddScoped<IServiceManager, ServiceManager>();

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
Directory.CreateDirectory(filesPath);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(filesPath),
    RequestPath = "/Files"
});

app.MapHub<BaseChatHub>("/chatHub");

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
