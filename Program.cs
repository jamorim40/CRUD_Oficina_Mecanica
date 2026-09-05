using Mecanica.Datas;
using Mecanica.Middleware;
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
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteValidation, ClienteValidation>();
builder.Services.AddScoped<VeiculoRepository>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddScoped<IVeiculoValidation, VeiculoValidation>();
builder.Services.AddScoped<OrdemServicoRepository>();
builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
builder.Services.AddScoped<IOrdemServicoService, OrdemServicoService>();
builder.Services.AddScoped<IOrdemServicoValidation, OrdemServicoValidation>();
builder.Services.AddScoped<CargoRepository>();
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<ICargoService, CargoService>();
builder.Services.AddScoped<FuncionarioRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();


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
