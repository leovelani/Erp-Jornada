using AutoMapper;
using Erp_Jornada.Configs;
using Erp_Jornada.Data;
using Erp_Jornada.Dtos;
using Erp_Jornada.Mapper;
using Erp_Jornada.Middlewares;
using Erp_Jornada.Repository;
using Erp_Jornada.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<DTOValidationFilter>();
})
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;

    });
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddServiceSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddHttpClient();

#region ADICIONANDO SERVICES
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<MarcaRepository>();
builder.Services.AddScoped<MarcaService>();
builder.Services.AddScoped<FabricaRepository>();
builder.Services.AddScoped<FabricaService>();
#endregion
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddServiceJwt();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var corsName = "cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsName,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

var app = builder.Build();
app.UseMiddleware(typeof(ErrorMiddleware));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsName);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
