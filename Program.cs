//Dependency Injection
using ASP.NetCoreMVC_SchoolSystem;
using ASP.NetCoreMVC_SchoolSystem.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SchoolDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbConnection"));
});
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<SubjectService>();
var app = builder.Build();

// Configure the HTTP request pipeline. - Zalezi na poradi prikazu tzv. middleware

//Tato cast rika ze middleware je omezen jen na produkni prostredi
if (!app.Environment.IsDevelopment())
{
    //Smeruje chybove hlasky na urcitou cestu
    app.UseExceptionHandler("/Home/Error");

    //Aktivuje bezpecnostni mechanismus ktery chrani web
    app.UseHsts();
}

//Vynucuje pouziti HTTPS
app.UseHttpsRedirection();

//Pro pouziti statickych souboru... JS, CSS... Bez toho nefunguji
app.UseStaticFiles();

//Propojuje pozadavky na endpointy
app.UseRouting();

app.UseAuthorization();

//Specifikuje cesty... Tato appka zacina na controlleru Home a akci Index, id volitelne
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
