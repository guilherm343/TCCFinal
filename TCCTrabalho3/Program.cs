using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TCCTrabalho3.Data;
using TCCTrabalho3.Services.CarrinhoService;

var builder = WebApplication.CreateBuilder(args);

// Adicionar os serviços ao contêiner.
builder.Services.AddScoped<ICursosService, CursosService>();
builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();

// Configuração da string de conexão com o banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configuração do DbContext para a Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuração do DbContext para as entidades do sistema
builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(connectionString));

// Habilitar o filtro de página de desenvolvedor para migrações
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configuração da identidade para os usuários
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>(); // Associa a identidade ao contexto de dados

     
// Adicionar controllers e views
builder.Services.AddControllersWithViews();

// Configuração da sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
    options.Cookie.HttpOnly = true; // O cookie só será acessível pelo servidor
    options.Cookie.IsEssential = true; // O cookie será enviado mesmo em configurações de privacidade estritas
});

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
   // app.UseMigrationsEndPoint(); // Usado para migrações no ambiente de desenvolvimento
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Tratamento de erros
    app.UseHsts(); // Configura HSTS para segurança
}

app.UseHttpsRedirection(); // Redireciona para HTTPS
app.UseStaticFiles(); // Serve arquivos estáticos como CSS, JS, etc.

app.UseRouting(); // Habilita o roteamento

app.UseSession(); // Adiciona o middleware para a sessão
app.UseAuthentication(); // Adiciona o middleware de autenticação
app.UseAuthorization(); // Habilita a autorização para a aplicação

// Configuração das rotas do controlador
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Mapear Razor Pages, se houver
app.MapRazorPages();

// Inicia a aplicação
app.Run();
