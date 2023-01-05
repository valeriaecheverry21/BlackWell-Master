using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackWell.BusinessLayer.Context;
using BlackWell.BusinessLayer.Contract;
using BlackWell.BusinessLayer.Interfaces;
using BlackWell.BusinessLayer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BlackWell.BusinessLayer.Support
{
    public static class DatabaseExtension
    {
        public static string ConfigurationSection = "SQLSettings";
        public static IServiceCollection AddCustomizedDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStorageStartup<DataContext>(configuration);
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
        
        public static IServiceCollection AddStorageStartup<TContext>(
            this IServiceCollection services,
            IConfiguration configuration
            ) where TContext : DbContext
        {

            var storageSettings = GetSettings(services, configuration);
            if (storageSettings != null)
            {
                services.AddSingleton(storageSettings);
                #region Unit Of Work
                services.AddTransient<ISQLUnitOfWork, SQLUnitOfWork>();
                #endregion

                #region DbContext
                services.AddScoped<DbContext, TContext>();
                services.AddDbContext<TContext>(
                        options => _ = storageSettings.DbType switch
                        {
                            DatabaseType.SQLSERVER => options.UseSqlServer(
                                storageSettings.ConnectionString),

                            _ => throw new Exception($"Base de Datos - no se pudo configurar {storageSettings.DbType}")
                        });
                #endregion
            }

            return services;
        }

        /// <summary>
        /// Obtiene la configuracion para base de datos
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static StorageSettings GetSettings(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            try
            {
                serviceCollection.Configure<StorageSettings>(configuration.GetSection(ConfigurationSection));

                var storageSettings = serviceCollection.BuildServiceProvider().GetService<IOptions<StorageSettings>>();
                return storageSettings.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static void MigrationInitialization(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<DataContext>().Database.Migrate();
            }
        }
    }
}
