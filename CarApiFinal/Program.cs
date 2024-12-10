using CarApiFinal.Models;
using CarApiFinal.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<CarService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
