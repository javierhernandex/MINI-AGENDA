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
            modelBuilder.Entity<MMedico>(entity =>
            {
                entity.HasKey(e => e.idMedico);

                entity.Property(e => e.nombre)
                    .HasMaxLength(50);

                entity.Property(e => e.nombre)
                    .HasMaxLength(50);

                entity.Property(e => e.FechaAlta)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Estatus)
                    .HasDefaultValue(true);
            });

            modelBuilder.Entity<HorarioAtencion>(entity =>
            {
                entity.HasKey(e => e.idHorario);

                entity.Property(e => e.Activo)
                    .HasDefaultValue(true);

                entity.ToTable(t =>
                {
                    t.HasCheckConstraint("CHK_Horario_Valido", "HoraFin > HoraInicio");
                    t.HasCheckConstraint("CHK_Dia_Semana", "DiaSemana BETWEEN 1 AND 7");
                });
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.idEspecialidad);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50);

                entity.Property(e => e.Estatus)
                    .HasDefaultValue(true);
            });
            modelBuilder.Entity<MedicoEspecialidad>(entity =>
            {
                entity.HasKey(e => new { e.idMedico, e.idEspecialidad });
            });
            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.idPaciente);

                entity.Property(e => e.nombre)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.apellido)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.telefono)
                    .HasMaxLength(15)
                    .IsRequired();

                entity.Property(e => e.email)
                    .HasMaxLength(50);

                entity.Property(e => e.fechaAlta)
                    .HasDefaultValueSql("GETDATE()");
            });
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(e => e.idCita);

                entity.Property(e => e.estado)
                    .HasMaxLength(20);

                entity.Property(e => e.motivo)
                    .HasMaxLength(100);

                entity.Property(e => e.motivoCancelacion)
                    .HasMaxLength(100);

                entity.Property(e => e.fechaCreacion)
                    .HasDefaultValueSql("GETDATE()");
            });
            modelBuilder.Entity<HorarioAtencion>()
    .HasOne<MMedico>()
    .WithMany()
    .HasForeignKey(h => h.idMedico);

            modelBuilder.Entity<MedicoEspecialidad>()
                .HasOne<MMedico>()
                .WithMany()
                .HasForeignKey(m => m.idMedico);

            modelBuilder.Entity<MedicoEspecialidad>()
                .HasOne<Especialidad>()
                .WithMany()
                .HasForeignKey(m => m.idEspecialidad);

            modelBuilder.Entity<Cita>()
                .HasOne<Paciente>()
                .WithMany()
                .HasForeignKey(c => c.idpaciente);

            modelBuilder.Entity<Cita>()
                .HasOne<MMedico>()
                .WithMany()
                .HasForeignKey(c => c.idpaciente);

            modelBuilder
                .Entity<Hora>(eb =>
                    eb.HasNoKey()
                )
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