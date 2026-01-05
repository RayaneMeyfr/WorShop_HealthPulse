using ConsultationService.Application.Services;
using ConsultationService.Domaine.Ports;
using ConsultationService.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Consultation Service API", Version = "v1" });
});

// Dependency Injection
builder.Services.AddSingleton<IConsultationRepository, InMemoryConsultationRepository>();
builder.Services.AddScoped<IConsultationAppService, ConsultationAppService>();

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