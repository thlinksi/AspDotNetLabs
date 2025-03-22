using Microsoft.EntityFrameworkCore;
using Lab2.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<JobDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:JobPortalConnection"]);
});
builder.Services.AddScoped<IJobRepository, EFJobRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();