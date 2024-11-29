using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TCCTrabalho3.Data;
using TCCTrabalho3.Services.CarrinhoService;

var builder = WebApplication.CreateBuilder(args);

// Adicionar os servi�os ao cont�iner.
builder.Services.AddScoped<ICursosService, CursosService>();
builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();

// Configura��o da string de conex�o com o banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configura��o do DbContext para a Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configura��o do DbContext para as entidades do sistema
builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(connectionString));

// Habilitar o filtro de p�gina de desenvolvedor para migra��es
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configura��o da identidade para os usu�rios
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>(); // Associa a identidade ao contexto de dados

     
// Adicionar controllers e views
builder.Services.AddControllersWithViews();

// Configura��o da sess�o
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expira��o da sess�o
    options.Cookie.HttpOnly = true; // O cookie s� ser� acess�vel pelo servidor
    options.Cookie.IsEssential = true; // O cookie ser� enviado mesmo em configura��es de privacidade estritas
});

var app = builder.Build();

// Configura��o do pipeline de requisi��o HTTP
if (app.Environment.IsDevelopment())
{
   // app.UseMigrationsEndPoint(); // Usado para migra��es no ambiente de desenvolvimento
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Tratamento de erros
    app.UseHsts(); // Configura HSTS para seguran�a
}

app.UseHttpsRedirection(); // Redireciona para HTTPS
app.UseStaticFiles(); // Serve arquivos est�ticos como CSS, JS, etc.

app.UseRouting(); // Habilita o roteamento

app.UseSession(); // Adiciona o middleware para a sess�o
app.UseAuthentication(); // Adiciona o middleware de autentica��o
app.UseAuthorization(); // Habilita a autoriza��o para a aplica��o

// Configura��o das rotas do controlador
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Mapear Razor Pages, se houver
app.MapRazorPages();

// Inicia a aplica��o
app.Run();
