# SchoolSystem_ASP.NetCoreMVC

Moderní školní informační systém vyvinutý v ASP.NET Core 8.0 MVC s pokročilým systémem autentizace a autorizace.

## Obsah

- [Přehled projektu](#přehled-projektu)
- [Technologie](#technologie)
- [Funkce](#funkce)
- [Instalace](#instalace)
- [Konfigurace](#konfigurace)
- [Použití](#použití)
- [Architektura](#architektura)
- [Databáze](#databáze)
- [Bezpečnost](#bezpečnost)
- [Vývoj](#vývoj)
- [Autor](#autor)

## Přehled projektu

**SchoolSystem_ASP.NetCoreMVC** je komplexní webová aplikace navržená pro správu školního prostředí. Aplikace umožňuje správu studentů, učitelů, předmětů a známek s pokročilým systémem rolí a oprávnění.

### Klíčové vlastnosti:
- **Role-based přístup** - 4 úrovně oprávnění (Admin, Principal, Teacher, Student)
- **Moderní UI** - Responzivní design s Bootstrap 5
- **Bezpečnost** - ASP.NET Core Identity s HTTPS
- **Škálovatelnost** - Entity Framework Core s migracemi
- **Cross-platform** - Běží na Windows, Linux, macOS

## Technologie

### Backend
- **ASP.NET Core 8.0** - Moderní webový framework
- **Entity Framework Core 9.0.5** - ORM pro práci s databází
- **ASP.NET Core Identity 8.0.16** - Autentizace a autorizace
- **SQL Server** - Databázový systém (LocalDB pro vývoj)

### Frontend
- **Bootstrap 5.3.0** - CSS framework pro responzivní design
- **jQuery 3.x** - JavaScript knihovna
- **Razor Pages** - Server-side templating engine

### Nástroje
- **Visual Studio 2022** - IDE
- **Git** - Verzování kódu
- **NuGet** - Správa balíčků

## Funkce

### Správa uživatelů
- **Registrace a přihlášení** uživatelů
- **Role-based autorizace** (Admin, Principal, Teacher, Student)
- **Správa hesel** s bezpečnostními požadavky
- **Session management** s automatickým odhlášením

### Správa studentů
- **CRUD operace** pro studenty
- **Vyhledávání** podle jména
- **Zobrazení detailů** včetně data narození
- **Oprávnění** podle rolí

### Správa učitelů
- **CRUD operace** pro učitele
- **Správa platů** (viditelné pouze pro Admin/Principal)
- **Detailní informace** o učitelích

### Správa předmětů
- **CRUD operace** pro předměty
- **Validace názvů** (2-50 znaků, pouze písmena)
- **Dropdown seznamy** pro výběr

### Správa známek
- **Přiřazování známek** studentům za předměty
- **Témata hodnocení** (zkoušení, písemka, atd.)
- **Datum hodnocení**
- **Vztahy** mezi studenty a předměty

### Systém rolí
| Role | Oprávnění |
|------|-----------|
| **Admin** | Plná kontrola včetně správy uživatelů a rolí |
| **Principal** | Vše kromě správy uživatelů a rolí |
| **Teacher** | Známky, studenti (CRUD), učitelé (pouze čtení) |
| **Student** | Pouze čtení (omezené zobrazení) |

## Instalace

### Předpoklady
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) nebo [VS Code](https://code.visualstudio.com/)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

### Kroky instalace

1. **Klonování repozitáře**
```bash
git clone https://github.com/your-username/SchoolSystem_ASP.NetCoreMVC.git
cd SchoolSystem_ASP.NetCoreMVC
```

2. **Obnovení balíčků**
```bash
dotnet restore
```

3. **Spuštění migrací**
```bash
dotnet ef database update
```

4. **Spuštění aplikace**
```bash
dotnet run
```

5. **Otevření v prohlížeči**
```
https://localhost:5001
```

## Konfigurace

### Connection Strings
Aplikace podporuje dva režimy:

**Development (LocalDB):**
```json
{
  "ConnectionStrings": {
    "SchoolDbConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SchoolDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;MultipleActiveResultSets=true"
  }
}
```

**Production:**
```json
{
  "ConnectionStrings": {
    "ProductionConnection": "Server=your-server;Database=your-db;User Id=your-user;Password=your-password;Encrypt=False;MultipleActiveResultSets=True;"
  }
}
```

### Nastavení Identity
```csharp
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
});
```

## Použití

### Testovací účty

Aplikace obsahuje předpřipravené testovací účty:

| Role | Uživatelské jméno | Heslo |
|------|------------------|-------|
| **Principal** | StanislavTvrdy | standa123 |
| **Teacher** | PetraSpalena | petra123 |
| **Student** | JanNovak | jan123 |

> **Poznámka:** Admin účet není poskytnut z bezpečnostních důvodů.

### Navigace

1. **Přihlášení** - Použijte testovací údaje
2. **Hlavní menu** - Dynamické podle vaší role
3. **Správa dat** - CRUD operace podle oprávnění
4. **Odhlášení** - Bezpečné ukončení session

## Architektura

### MVC Pattern
```
Controllers/     # Řízení toku aplikace
├── HomeController.cs
├── AccountController.cs
├── StudentController.cs
├── TeacherController.cs
├── SubjectController.cs
├── GradeController.cs
├── UsersController.cs
└── RolesController.cs

Models/          # Datové modely
├── Student.cs
├── Teacher.cs
├── Subject.cs
├── Grade.cs
└── AppUsers.cs

Views/           # Uživatelské rozhraní
├── Home/
├── Account/
├── Student/
├── Teacher/
├── Subject/
├── Grade/
├── Users/
├── Roles/
└── Shared/
```

### Services Layer
```
Services/        # Business logika
├── StudentService.cs
├── TeacherService.cs
├── SubjectService.cs
└── GradeService.cs
```

### DTO Pattern
```
DTO/             # Data Transfer Objects
├── StudentDTO.cs
├── TeacherDTO.cs
├── SubjectDTO.cs
└── GradeDTO.cs
```

## Databáze

### Entity Relationship Diagram
```
Students (1) ←→ (N) Grades (N) ←→ (1) Subjects
Teachers (1) ←→ (N) [Future: Teaching assignments]

AppUsers (Identity)
├── AspNetUsers
├── AspNetRoles
├── AspNetUserRoles
└── AspNetUserClaims
```

### Migrace
```bash
# Vytvoření nové migrace
dotnet ef migrations add MigrationName

# Aplikace migrace
dotnet ef database update

# Vrácení migrace
dotnet ef database update PreviousMigrationName
```

## Bezpečnost

### Implementované bezpečnostní mechanismy:
- **HTTPS enforcement** v produkci
- **Anti-forgery tokens** proti CSRF útokům
- **Password policies** - silná hesla
- **Session timeout** - 10 minut s sliding expiration
- **Role-based authorization** - granular permissions
- **Input validation** - server-side i client-side
- **SQL injection protection** - Entity Framework

### Doporučení pro produkci:
- Změňte výchozí hesla
- Použijte silné connection strings
- Aktivujte HTTPS
- Nastavte proper logging
- Implementujte backup strategii

## Vývoj

### Struktura projektu
```
SchoolSystem_ASP.NetCoreMVC/
├── Controllers/          # MVC Controllers
├── Models/              # Data Models
├── Services/            # Business Logic
├── DTO/                 # Data Transfer Objects
├── ViewModels/          # View Models
├── Views/               # Razor Views
├── Migrations/          # EF Migrations
├── wwwroot/            # Static Files
├── Program.cs          # Application Entry Point
├── SchoolDbContext.cs  # Database Context
└── appsettings.json    # Configuration
```

### Development Commands
```bash
# Spuštění v development módu
dotnet run --environment Development

# Spuštění s hot reload
dotnet watch run

# Build pro produkci
dotnet publish -c Release

# Spuštění testů (pokud budou přidány)
dotnet test
```

### Best Practices
- Používejte DTO pattern pro bezpečnost
- Implementujte proper error handling
- Dodržujte SOLID principy
- Píšte unit testy
- Používejte dependency injection

### Možná vylepšení
- Unit testy
- API dokumentace (Swagger)
- Logging (Serilog)
- Caching (Redis)
- Email notifications
- File upload functionality
- Advanced reporting
- Mobile app integration

## Licence

Tento projekt je vytvořen pro vzdělávací účely. Všechna práva vyhrazena.

## Autor

**Robin Lassak**

- Student informatiky
- ASP.NET Core vývojář
- Vytvořeno jako školní projekt pro demonstraci znalostí ASP.NET Core MVC
- Kontakt: [váš-email@domain.com]

---

> **Poznámka:** Tento projekt byl vytvořen jako školní projekt pro účely vzdělávání a demonstrace znalostí moderních webových technologií. Jedná se o můj první MVC projekt, takže některé chyby mohou být přítomny. Plánuji ho dále rozvíjet a vylepšovat.

## Poděkování

Děkuji všem, kteří mi pomohli s vývojem tohoto projektu a poskytli cenné zpětné vazby.

---

**Poslední aktualizace:** Květen 2025
