using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MINI_AGENDA.Repository;
using MINI_AGENDA.Repository.IRepository;
using MINI_AGENDA.Services;
using MINI_AGENDA.Services.IServices;

namespace MINI_AGENDA
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer();

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContextPool<MiniAgendaDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MiniAgenda"));
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });
            services.AddScoped<IMedicoService, MedicoService> ();
            services.AddScoped<IMedico, RMedico>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IPaciente, RPaciente>();
            services.AddScoped<ICitaService, CitaService>();
            services.AddScoped < ICita,RCita>();
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MiniAgenda",
                    Description = "Api",
                });
                
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }




        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();

            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiniAgenda"));
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoint =>
            {

                endpoint.MapControllers();

            });

        }
    }
}
