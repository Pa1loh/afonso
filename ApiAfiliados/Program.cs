using ApiAfiliados.ColecaoServicos;
using InfraEstrutura.BancoDeDados;

var builder = WebApplication.CreateBuilder(args);

// configuracao de servicos.
builder.Services.RegistrarServices(builder.Configuration);
builder.Services.AdicionarModelos(builder.Configuration);

builder.Services.AddDbContext<ApiDBContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//if (app.Environment.IsDevelopment())

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();