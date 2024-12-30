var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
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
app.UseSession();

app.MapControllerRoute(
name: "MP",
pattern: "Home/MP",
defaults: new { controller = "Home", action = "MyPage" });

app.MapControllerRoute(
name: "MP2",
pattern: "MP/{number}/{name},{other}",
defaults: new { controller = "Home", action = "MyPage2" });

app.MapControllerRoute(
name: "Solve",
pattern: "Tool/Solve/{a}/{b}/{c}",
defaults: new { controller = "Tool", action = "Solve" });

app.MapControllerRoute(
name: "Set",
pattern: "Set,{newRange}",
defaults: new { controller = "Game", action = "Set" });

app.MapControllerRoute(
name: "Draw",
pattern: "Draw",
defaults: new { controller = "Game", action = "Draw" });

app.MapControllerRoute(
name: "Guess",
pattern: "Guess,{guessedNumber}",
defaults: new { controller = "Game", action = "Guess" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
