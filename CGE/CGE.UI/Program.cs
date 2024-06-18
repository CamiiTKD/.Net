using CGE.UI.Components;

//agregamos estas directivas using
using CGE.Repositorios;
using CGE.Aplicacion.CasosDeUso;
using CGE.Aplicacion.Interfaces;
using CGE.Aplicacion.Validadores;
using CGE.Aplicacion.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add our own services to the container. 
CGEContext contexto = new CGEContext();
// EXPEDIENTE
builder.Services.AddTransient<CasoDeUsoExpedienteAlta>();
builder.Services.AddTransient<CasoDeUsoExpedienteBaja>();
builder.Services.AddTransient<CasoDeUsoExpedienteConsultaPorId>();
builder.Services.AddTransient<CasoDeUsoExpedienteConsultaTodos>();
builder.Services.AddTransient<CasoDeUsoExpedienteModificacion>();
// TRAMITE
builder.Services.AddTransient<CasoDeUsoTramiteAlta>();
builder.Services.AddTransient<CasoDeUsoTramiteBaja>();
builder.Services.AddTransient<CasoDeUsoTramiteConsultaPorEtiqueta>();
builder.Services.AddTransient<CasoDeUsoTramiteModificacion>();
// USUARIO
builder.Services.AddTransient<CasoDeUsoUsuarioAlta>();
builder.Services.AddTransient<CasoDeUsoUsuarioBaja>();
builder.Services.AddTransient<CasoDeUsoUsuarioConsultaTodos>();
builder.Services.AddTransient<CasoDeUsoUsuarioModificacion>();
// VALIDADOR
builder.Services.AddSingleton<ExpedienteValidador>();
builder.Services.AddSingleton<TramiteValidador>();
// SERVICIO
builder.Services.AddSingleton<EspecificacionCambioEstado>();
builder.Services.AddSingleton<ServicioActualizacionEstado>();
builder.Services.AddSingleton<IServicioAutorizacion, ServicioAutorizacionProvisorio>();
// REPOSITORIO
builder.Services.AddSingleton<IExpedienteRepositorio, Repositorio>(ExpedienteRepo => new Repositorio(contexto));
builder.Services.AddSingleton<ITramiteRepositorio, Repositorio>(TramiteRepo => new Repositorio(contexto));
builder.Services.AddSingleton<IUsuarioRepositorio, Repositorio>(UsuarioRepo => new Repositorio(contexto));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
