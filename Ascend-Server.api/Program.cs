using Microsoft.EntityFrameworkCore;
using Data;
using IServices;
using Services;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Ascend_Server.api.ActionFilters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Audience = "api://c6d1ad12-2efe-4072-9bde-4d9ac568762c";
        options.Authority = "https://login.microsoftonline.com/07fcd2cd-de40-4214-bf28-7818722bd2d8/";
    });

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader()
        );
});

//allow the controller action methods to return a custom response if invalid, instead of an automatic response done by the ModelStateInvalidFilter
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ascend API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<IWellnessRatingService, WellnessRatingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHabitService, HabitService>();
builder.Services.AddScoped<IHabitCompletionLogService, HabitCompletionLogService>();
builder.Services.AddScoped<IGuidedJournalEntryService, GuidedJournalEntryService>();
builder.Services.AddScoped<IGuidedJournalLogService, GuidedJournalLogService>();
builder.Services.AddScoped<ModelStateActionFilter>();
builder.Services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("ApiContext"));
var app = builder.Build();

//configure CORS
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwagger();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApiContext>();

    context.Database.EnsureCreated();

    context.Seed(services);

}

app.Run();
