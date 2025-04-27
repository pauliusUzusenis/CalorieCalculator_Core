using AspNetCore.Unobtrusive.Ajax;
using CalorieCalculatorCore.Data;
using CalorieCalculatorCore.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddControllersWithViews();
builder.Services.AddUnobtrusiveAjax();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseUnobtrusiveAjax();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calculator}/{action=Index}/{id?}");

app.Run();
