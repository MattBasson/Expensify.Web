
using Expensify.Database;
using Expensify.Library.Modules.Database;
using Expensify.Web.Controllers;
using Microsoft.EntityFrameworkCore;

using MySqlConnector;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

//builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

builder.Services.AddDbContext<ExpensifyContext>((provider, options) =>
{
    var configuration = builder.Configuration;
    var connectionString = new MySqlConnectionStringBuilder
    {
        Server = configuration["MySql:Server"],
        Port = configuration.GetValue<uint>("MySql:Port"),
        Database = configuration["MySql:Database"],
        //Set with user secrets
        UserID = configuration["MySql:User"],
        Password = configuration["MySql:Password"],
        ConnectionTimeout = 3000
    }.ToString();
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
        builder => { builder.CommandTimeout(60); });
});
builder.Services.AddScoped<ExpenseQuery>();



var app = builder.Build();



// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();