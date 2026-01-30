using CustomerManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Remove all default logging except our manual Console.WriteLine
builder.Logging.ClearProviders();
builder.Logging.AddFilter("Microsoft", LogLevel.None);
builder.Logging.AddFilter("System", LogLevel.None);
builder.Logging.AddConsole(); 

// Add services
builder.Services.AddControllersWithViews();

// Add Database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Check database connection 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        if (db.Database.CanConnect())
            Console.WriteLine("✅ Database connected successfully.");
        else
            Console.WriteLine("❌ Database connection failed.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Database connection error: {ex.Message}");
    }

    db.Database.EnsureCreated();
}

// Configure middleware / request pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Print host and port
var urls = app.Urls.Count > 0 ? string.Join(", ", app.Urls) : "http://localhost:5000";
Console.WriteLine($"🚀 App running on: {urls}");

app.Run();