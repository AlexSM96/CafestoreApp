var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
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