using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MINI_AGENDA.Controllers;
using MINI_AGENDA.Models.Medico;
using MINI_AGENDA.Models.Pacientes;
using MINI_AGENDA.Models.Cita;
namespace MINI_AGENDA
{
    public class MiniAgendaDbContext : DbContext
    {
        public MiniAgendaDbContext(DbContextOptions<MiniAgendaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<MMedico>(eb =>
                {
                    eb.HasKey("idMedico");
                })
                .Entity<Especialidad>(eb =>
                {
                    eb.HasKey("idEspecialidad");
                })
                .Entity<HorarioAtencion>(eb =>
                {
                    eb.HasKey("idHorario");
                })
                .Entity<Paciente>(eb =>
                    eb.HasKey("idPaciente")
                )
                .Entity<Cita>(eb =>
                    eb.HasKey("idCita")
                )
                .Entity<Hora>(eb =>
                    eb.HasNoKey()
                )
                .Entity<MedicoEspecialidad>()
                     .HasKey(me => new { me.idMedico, me.idEspecialidad })


                



            ;


        }
        public DbSet<MMedico> Medico { get; set; }
        public DbSet<Especialidad> Especialidad { get; set; }
        public DbSet<HorarioAtencion> HorarioAtencion { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Cita> Cita { get; set; }
        public DbSet<MedicoEspecialidad> MedicoEspecialidad { get; set; }
        public DbSet<Hora> Hora { get; set; }
    }
}