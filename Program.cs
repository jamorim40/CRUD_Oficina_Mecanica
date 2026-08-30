using Mecanica.Datas;
using Mecanica.Models.Entities;
using Mecanica.Repositories.Interfaces;
using Mecanica.Repositories.Repository;
using Mecanica.Services.Interfaces;
using Mecanica.Services.Service;
using Mecanica.Validations.Interfaces.Cliente;
using Mecanica.Validations.Interfaces.OrdemServico;
using Mecanica.Validations.Interfaces.Veiculo;
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
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<VeiculoRepository>();
builder.Services.AddScoped<OrdemServico>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddScoped<IOrdemServicoService, OrdemServicoService>();
builder.Services.AddScoped<IClienteValidator, ClienteValidator>();
builder.Services.AddScoped<IVeiculoValidador, VeiculoValidador>();
builder.Services.AddScoped<IOrdemServicoValidator, OrdemServicoValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
