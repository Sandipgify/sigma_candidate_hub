using candidatehub.Application;
using candidatehub.Web.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication().
    AddPresentation().
    AddInfrastructure();

builder.Services.ConfigureSerilog(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddTransient<GlobalErrorHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandler>();

app.MapControllers();

app.Run();
