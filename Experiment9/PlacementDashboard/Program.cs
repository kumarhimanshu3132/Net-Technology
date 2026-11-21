using PlacementDashboard.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\MSSQLLocalDB;Database=B2026;Integrated Security=True;TrustServerCertificate=True;";
builder.Services.AddScoped<IPlacementDataAccess, PlacementDataAccess>();
var app = builder.Build();
DbInitializer.Initialize(connectionString);
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Placement}/{action=Index}/{id?}");
app.Run();