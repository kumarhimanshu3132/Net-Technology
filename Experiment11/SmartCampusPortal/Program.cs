using SmartCampusPortal.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICampusRepository, CampusRepository>();

var app = builder.Build();

var masterConn = builder.Configuration.GetConnectionString("MasterConnection")
    ?? "Server=(localdb)\\MSSQLLocalDB;Database=master;Integrated Security=True;TrustServerCertificate=True;";
var defaultConn = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\MSSQLLocalDB;Database=B2026;Integrated Security=True;TrustServerCertificate=True;";

try
{
    DbInitializer.Initialize(masterConn, defaultConn);
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "Failed to initialize SQL Server database or tables.");
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Portal/Index");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Portal}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
