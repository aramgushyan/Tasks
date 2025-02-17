using Microsoft.EntityFrameworkCore;
using TaskProject.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TaskDbContest>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TaaskString")));
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
