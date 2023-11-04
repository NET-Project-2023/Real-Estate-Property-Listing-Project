using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using PropertyListing.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Options;
//using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//IConfiguration configuration = builder.Configuration;


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//builder.Configuration.AddJsonFile("appsettings.json");


builder.Services.AddDbContext<PropertyListingContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("RealEstateDatabase"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
