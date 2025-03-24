using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddNewtonsoftJson();

builder.Services
    .AddCafestoreDbContext(builder.Configuration)
    .AddDomainServices()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Services.CreateDbIfNotExists();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();