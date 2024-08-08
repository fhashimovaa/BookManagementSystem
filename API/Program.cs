using API;
using API.Filters;
using Application;
using Application.Helpers;
using DataAccess;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: false);

// Add services to the container.
Accessor.AppConfiguration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApi(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}).AddFluentValidation(x => x.AutomaticValidationEnabled = true)
  .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", " API"); });
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
