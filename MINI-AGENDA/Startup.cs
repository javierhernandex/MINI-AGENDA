using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MINI_AGENDA.Models.Cita;
using MINI_AGENDA.Models.Medico;
using MINI_AGENDA.Models.Pacientes;
using MINI_AGENDA.Repository;
using MINI_AGENDA.Repository.IRepository;
using MINI_AGENDA.Services;
using MINI_AGENDA.Services.IServices;
using System.Reflection;

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
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MINIAGENDA"),
            sqlServerOptionsAction: sqlOptions =>
            {
               
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,         
                    maxRetryDelay: TimeSpan.FromSeconds(30), 
                    errorNumbersToAdd: null    
                );
            });
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
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MiniAgendaDbContext>();

                var retries = 10;
                while (retries > 0)
                {
                    try
                    {
                        db.Database.Migrate();
                        if (!db.Medico.Any())
                        {
                            db.Medico.AddRange(
                                new MMedico { nombre = "Javier", apellido = "Hernandez", Estatus = true },
                                new MMedico { nombre = "Dulce", apellido = "Fernandez", Estatus = true },
                                new MMedico { nombre = "Angela Fernanda", apellido = "Lopez Perez", Estatus = true },
                                new MMedico { nombre = "Mariana Anahi", apellido = "Fernandez", Estatus = true }
                            );
                            

                            db.SaveChanges();
                       
                            db.Especialidad.AddRange(
                                 new Especialidad {  Descripcion = "Medicina General", DuracionCita = 20 },
                                 new Especialidad {  Descripcion = "Cardiología", DuracionCita = 30 },
                                 new Especialidad {  Descripcion = "Cirugía", DuracionCita = 45 },
                                 new Especialidad {  Descripcion = "Pediatría", DuracionCita = 20 },
                                 new Especialidad {  Descripcion = "Ginecología", DuracionCita = 30 },
                                 new Especialidad {  Descripcion = "Urologia", DuracionCita = 50 },
                                 new Especialidad {  Descripcion = "Oncologia", DuracionCita = 30 },
                                 new Especialidad {  Descripcion = "Neumologia", DuracionCita = 45 }
                             );

                            db.SaveChanges();

                            db.MedicoEspecialidad.AddRange(
                                new MedicoEspecialidad { idMedico = 1, idEspecialidad = 1 },
                                new MedicoEspecialidad { idMedico = 2, idEspecialidad = 2 },
                                new MedicoEspecialidad { idMedico = 3, idEspecialidad = 8 },
                                new MedicoEspecialidad { idMedico = 4, idEspecialidad = 5 }
                             );

                            db.SaveChanges();

                            db.HorarioAtencion.AddRange(
                                  // Medico 1
                                  new HorarioAtencion { idMedico = 1, diasemana = 1, horainicio = TimeSpan.Parse("08:00:00"), horafin = TimeSpan.Parse("14:00:00"),Activo=true },
                                  new HorarioAtencion { idMedico = 1, diasemana = 2, horainicio = TimeSpan.Parse("08:00:00"), horafin = TimeSpan.Parse("14:00:00"), Activo = true },
                                  new HorarioAtencion { idMedico = 1, diasemana = 3, horainicio = TimeSpan.Parse("08:00:00"), horafin = TimeSpan.Parse("14:00:00"), Activo = true },
                                  new HorarioAtencion { idMedico = 1, diasemana = 4, horainicio = TimeSpan.Parse("08:00:00"), horafin = TimeSpan.Parse("14:00:00"), Activo = true },
                                  new HorarioAtencion { idMedico = 1, diasemana = 5, horainicio = TimeSpan.Parse("08:00:00"), horafin = TimeSpan.Parse("14:00:00"), Activo = true },
                                  new HorarioAtencion { idMedico = 1, diasemana = 6, horainicio = TimeSpan.Parse("09:00:00"), horafin = TimeSpan.Parse("12:00:00"), Activo = true },

                                  // Medico 2
                                  new HorarioAtencion { idMedico = 2, diasemana = 1, horainicio = TimeSpan.Parse("09:00:00"), horafin = TimeSpan.Parse("17:00:00"), Activo = true },
                                  new HorarioAtencion { idMedico = 2, diasemana = 2, horainicio = TimeSpan.Parse("09:00:00"), horafin = TimeSpan.Parse("15:00:00"), Activo = true },
                                  new HorarioAtencion { idMedico = 2, diasemana = 3, horainicio = TimeSpan.Parse("09:00:00"), horafin = TimeSpan.Parse("12:00:00"), Activo = true },
                                  new HorarioAtencion { idMedico = 2, diasemana = 4, horainicio = TimeSpan.Parse("09:00:00"), horafin = TimeSpan.Parse("14:00:00") , Activo = true },
                                  new HorarioAtencion { idMedico = 2, diasemana = 5, horainicio = TimeSpan.Parse("09:00:00"), horafin = TimeSpan.Parse("14:00:00") , Activo = true },

                                  // Medico 3
                                  new HorarioAtencion { idMedico = 3, diasemana = 2, horainicio = TimeSpan.Parse("09:00:00"), horafin = TimeSpan.Parse("17:00:00"), Activo = true },
                                  new HorarioAtencion { idMedico = 3, diasemana = 3, horainicio = TimeSpan.Parse("09:00:00"), horafin = TimeSpan.Parse("15:00:00") , Activo = true },
                                  new HorarioAtencion { idMedico = 3, diasemana = 5, horainicio = TimeSpan.Parse("09:00:00"), horafin = TimeSpan.Parse("12:00:00") , Activo = true },
                                  new HorarioAtencion { idMedico = 3, diasemana = 6, horainicio = TimeSpan.Parse("09:00:00"), horafin = TimeSpan.Parse("14:00:00") , Activo = true },
                                  new HorarioAtencion { idMedico = 3, diasemana = 7, horainicio = TimeSpan.Parse("09:00:00"), horafin = TimeSpan.Parse("14:00:00") , Activo = true },

                                  // Medico 4
                                  new HorarioAtencion { idMedico = 4, diasemana = 1, horainicio = TimeSpan.Parse("08:00:00"), horafin = TimeSpan.Parse("20:00:00"), Activo = true },
                                  new HorarioAtencion { idMedico = 4, diasemana = 2, horainicio = TimeSpan.Parse("14:00:00"), horafin = TimeSpan.Parse("20:00:00") , Activo = true }
                              );

                            db.SaveChanges();

                            db.Paciente.AddRange(
                                new Paciente {  nombre = "Juan Pedro", apellido = "Valencia Guzman", fechaNacimiento = DateTime.Parse("1993-01-03"), telefono = "9991231212", email = "example@gmail.com" },
                                new Paciente { nombre = "Fernanda Maria", apellido = "Guzman Gonzales", fechaNacimiento = DateTime.Parse("1995-03-17"), telefono = "9996576212", email = "fernanda@gmail.com" },
                                new Paciente {  nombre = "Jesus", apellido = "Puc", fechaNacimiento = DateTime.Parse("1990-10-03"), telefono = "99912319292", email = "jpuc@gmail.com" }
                            );

                            db.SaveChanges();

                            db.Cita.AddRange(
                                 new Cita {  idpaciente = 1, idmedico = 1, fechaCita = DateTime.Parse("2026-03-23"), horaCita = TimeSpan.Parse("08:00:00"), estado = "Activa", motivo = "Dolor constante" },
                                 new Cita { idpaciente = 2, idmedico = 2, fechaCita = DateTime.Parse("2026-03-25"), horaCita = TimeSpan.Parse("09:30:00"), estado = "Activa", motivo = "Taticardia" },
                                 new Cita {  idpaciente = 3, idmedico = 3, fechaCita = DateTime.Parse("2026-03-28"), horaCita = TimeSpan.Parse("09:45:00"), estado = "Activa", motivo = "Estornudos Por las noches" },
                                 new Cita {  idpaciente = 3, idmedico = 3, fechaCita = DateTime.Parse("2026-03-28"), horaCita = TimeSpan.Parse("10:30:00"), estado = "Cancelada", motivo = "Estornudos Por las noches", fechaCancelacion = DateTime.Parse("2026-03-18"), motivoCancelacion = "Viaje de imprevisto" }
                             );

                            db.SaveChanges();
                        }
                        break;
                    }
                    catch
                    {
                        retries--;
                        Thread.Sleep(5000);
                    }
                }
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
