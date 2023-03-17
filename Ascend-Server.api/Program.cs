using Microsoft.EntityFrameworkCore; 
using Models;
using Services; 


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//dependency injection
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWellnessRatingService, WellnessRatingService>(); 
builder.Services.AddScoped<IUserService, UserService>(); 
builder.Services.AddScoped<IHabitService, HabitService>();
builder.Services.AddScoped<IHabitCompletionLogService, HabitCompletionLogService>();
builder.Services.AddScoped<IHabitCompletionLogService, HabitCompletionLogService>();
builder.Services.AddScoped<IGuidedJournalEntryService, GuidedJournalEntryService>();
builder.Services.AddScoped<IGuidedJournalLogService, GuidedJournalLogService>();
builder.Services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("ApiContext"));
var app = builder.Build();

//configure CORS
app.UseCors(builder => {
    builder.WithOrigins("http://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod();
});

//remove if statement to make documentation available for clients
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    
        var context = services.GetRequiredService<ApiContext>();
        context.Database.EnsureCreated();
        context.Seed(services);
    
    
}


app.Run();
