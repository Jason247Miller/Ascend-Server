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
var app = builder.Build();

//remove if statement to make documentation available for clients
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
