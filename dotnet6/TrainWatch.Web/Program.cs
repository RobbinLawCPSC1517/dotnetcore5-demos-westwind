//https://medium.com/executeautomation/asp-net-core-6-0-minimal-api-with-entity-framework-core-69d0c13ba9ab

//using statements added
using Microsoft.EntityFrameworkCore;
using TrainWatch.Services;
using TrainWatch.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Get connection string.
var connectionString = builder.Configuration.GetConnectionString("TWDB");
// TrainWatchContext class as a DbContext using SQL Server
builder.Services.AddDbContext<TrainWatchContext>(context => 
    context.UseSqlServer(connectionString));
// TrainWatchServices class as a transient service
builder.Services.AddTransient<TrainWatchServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
