using Microsoft.EntityFrameworkCore;
using MKTDotNetCore.RestApi.Db;
using MKTDotNetCore.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionstring = builder.Configuration.GetConnectionString("DbConnection")!;
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(connectionstring);
},ServiceLifetime.Transient,ServiceLifetime.Transient);
builder.Services.AddScoped(n=>new AdoDotNetService(connectionstring));
builder.Services.AddScoped(n => new DapperService(connectionstring));

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
