//Dependency Injection
using ASP.NetCoreMVC_SchoolSystem;
using ASP.NetCoreMVC_SchoolSystem.Models;
using ASP.NetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddDbContext<SchoolDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbConnection"));
});
//Prihlasovani - Autentizace
builder.Services.AddIdentity<AppUsers, IdentityRole>().AddEntityFrameworkStores<SchoolDbContext>().AddDefaultTokenProviders();
//Servisky
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<TeacherService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<GradeService>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;

});
//Cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10); //urcuje jak dlouho zustane uzivatel prihlasen
    options.SlidingExpiration = true; //pokud uzivatel v polovine casu neco provede, prihlasovaci cas se vyresetuje
    options.LoginPath = "/Account/Login";
});
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

//Prihlasovani
app.UseAuthentication();
//Kdo ma kam pristup
app.UseAuthorization();

//Specifikuje cesty... Tato appka zacina na controlleru Home a akci Index, id volitelne
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
