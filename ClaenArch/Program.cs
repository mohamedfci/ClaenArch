using Application.MediatR;
using ClaenArch.Extensions;
using ClaenArch.Services;
using Domains.Data;
using Infrastructure;
using Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true);
builder.Services.AddApplicationservices(builder.Configuration);

//builder.Services.AddMediatR(assembly);
var app = builder.Build();

//var handler = builder.Services.BuildServiceProvider().GetRequiredService<IRequestHandler<GetAllQuery<Activity>, IEnumerable<Activity>>>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
