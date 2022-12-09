using Breakfast.Services.Breakfasts;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddSingleton<IBreakfastService, BreakfastService>();
}

// Add services to the container.


var app = builder.Build();
{
app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
}



