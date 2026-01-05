using PrescriptionService.Application.Services;
using PrescriptionService.Domaine.Ports;
using PrescriptionService.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Prescription Service API", Version = "v1" });
});

// Dependency Injection
builder.Services.AddSingleton<IPrescriptionRepository, InMemoryPrescriptionRepository>();
builder.Services.AddScoped<IPrescriptionAppService, PrescriptionAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();