using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApp4Sec001.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddSystemsManager("/WebApp4Sec001");

var connectionBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("Connection2RDS"));
connectionBuilder.UserID = builder.Configuration["DBUser"];
connectionBuilder.Password = builder.Configuration["DBPassword"];
var connection = connectionBuilder.ConnectionString;
builder.Services.AddDbContext<SMSContext>(options => options.UseSqlServer(connection));

//builder.Services.AddDbContext<SMSContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection2RDS"));
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
