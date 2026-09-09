using Mecanica.Datas;
using Mecanica.Extensions;
using Mecanica.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddSwaggerGen();
builder.Services.AddInjecaoDependencias();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseMiddleware<ExcecoesMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
