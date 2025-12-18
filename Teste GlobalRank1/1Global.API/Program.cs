using _1Global.Domain.Context;
using Asp.Versioning;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(delegate (CorsOptions options)
{
    options.AddPolicy(MyAllowSpecificOrigins, delegate (CorsPolicyBuilder policy)
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddDbContext<DeviceContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("DeviceLogin")));

// Add services to the container.
builder.Services.AddTransient<_1Global.Domain.V1.Repository.Interfaces.IDeviceRepository, _1Global.Domain.V1.Repository.DeviceRepository>();
builder.Services.AddTransient<_1Global.Application.V1.Services.Interfaces.IDeviceService, _1Global.Application.V1.Services.DeviceService>();
builder.Services.AddTransient<_1Global.Domain.V2.Repository.Interfaces.IDeviceRepository, _1Global.Domain.V2.Repository.DeviceRepository>();
builder.Services.AddTransient<_1Global.Application.V2.Services.Interfaces.IDeviceService, _1Global.Application.V2.Services.DeviceService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1", new OpenApiInfo { 
        Title = " V1",
        Version = "v1" ,
        Description = "Descrição da versão 1 da API"
    });
    options.SwaggerDoc("v2", new OpenApiInfo { 
        Title = "V2", 
        Version = "v2",
        Description = "Descrição da versão 1 da API"
    });
    options.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName == docName);
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint($"/swagger/v1/swagger.json", "API version 1");
    options.SwaggerEndpoint($"/swagger/v2/swagger.json", "API version 2");
});

app.MapControllers();

app.Run();
