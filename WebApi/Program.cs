using Libreria.LogicaAccesoDatos.EF;
using Libreria.LogicaAplicacion.CasosDeUso.CUArticulos;
using Libreria.LogicaAplicacion.CasosDeUso.CUMovimientos;
using Libreria.LogicaAplicacion.CasosDeUso.CUTiposMovientos;
using Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUAltaMovimiento;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUArticulo;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUMovimiento;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUTipoMoviento;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUUsuario;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure the DbContext with the connection string
            builder.Services.AddDbContext<PapeleriaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register the repository
            builder.Services.AddScoped<IRepositorioArticulo, RepositorioArticuloEF>();
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuarioEF>();
            builder.Services.AddScoped<IRepositorioTipoMovimiento, RepositorioTipoMovimientoEF>();
            builder.Services.AddScoped<IRepositorioMovimientoStock, RepositorioMovimientoStockEF>();
            builder.Services.AddScoped<IRepositorioConfiguracion, RepositorioConfiguracionEF>();


            // Register the use case
            builder.Services.AddScoped<ICUGetAllArticulos, CUGetAllArticulos>();
            builder.Services.AddScoped<ICUGetArticulosPorRangoFechas, CUGetArticulosPorRangoFechas>();
            builder.Services.AddScoped<ICUAltaTipoMovimiento, CUAltaTipoMovimiento>();
            builder.Services.AddScoped<ICUDeleteTipoMovimiento, CUDeleteTipoMovimiento>();
            builder.Services.AddScoped<ICUEditTipoMovimiento, CUEditTipoMovimiento>();
            builder.Services.AddScoped<ICUGetTipoMovimiento, CUGetTipoMovimiento>();
            builder.Services.AddScoped<ICUAltaMovimiento, CUAltaMovimiento>();
            builder.Services.AddScoped<ICUGetMovimiento, CUGetMovimiento>();
            builder.Services.AddScoped<ICUGetResumen, CUGetResumen>();
            builder.Services.AddScoped<ICUGetMovimientosPorArticuloYTipo, CUGetMovimientosPorArticuloYTipo>();
            builder.Services.AddScoped<ICULoginUsuario, CULoginUsuario>();
            builder.Services.AddScoped<ICUGetMovimientosPaginados, CUGetMovimientosPaginados>();

            var claveDificil = "ClaveMuySecreta1_ClaveMuySecreta1_ClaveMuySecreta1_ClaveMuySecreta1_ClaveMuySecreta1_ClaveMuySecreta1";
            var claveDificilEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));

            #region Registro de servicios JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    //Definir las verificaciones a realizar
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = claveDificilEncriptada
                };

            });
            #endregion

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestor de depositos", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestor de depositos API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

