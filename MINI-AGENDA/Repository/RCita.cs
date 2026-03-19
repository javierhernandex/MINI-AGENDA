using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MINI_AGENDA.Models.Cita;
using MINI_AGENDA.Models.Medico;
using MINI_AGENDA.Models.Pacientes;
using MINI_AGENDA.Repository.IRepository;

namespace MINI_AGENDA.Repository
{
    public class RCita : ICita
    {
        private readonly MiniAgendaDbContext _bd;

        public RCita(MiniAgendaDbContext bd_)
        {
            _bd = bd_;
        }

        public async Task<IEnumerable<Hora>> GetHorarioDisponible(int idMedico, DateTime fecha)
        {
            var hora = await _bd
    .Set<Hora>()
    .FromSqlRaw("EXEC sp_DisponibilidadMedico @idMedico, @fecha",
        new SqlParameter("@idMedico", idMedico),
        new SqlParameter("@fecha", fecha.Date))
    .ToListAsync();

            return hora;

        }
        public async Task<HorarioAtencion> GetHorarioAtencions(int idmedico, int diasemana)
        {
            var horario = await _bd.HorarioAtencion
               .FirstOrDefaultAsync(x => x.idMedico == idmedico
                                     && x.diasemana == diasemana
                                     && x.Activo);
            return horario;


        }
        public async Task<IEnumerable<Cita>> GetCitasAll()
        {
            var citas = await (from m in _bd.Cita

                               select new Cita
                               {
                                   idCita = m.idCita,
                                   idmedico = m.idmedico,
                                   idpaciente = m.idpaciente,
                                   fechaCita = m.fechaCita,
                                   horaCita = m.horaCita,
                                   estado = m.estado,
                                   motivo = m.motivo,
                                   fechaCreacion = m.fechaCreacion,
                                   fechaCancelacion = m.fechaCancelacion,
                                   motivoCancelacion = m.motivoCancelacion
                               }).ToListAsync();
            return citas;
        }
        public async Task<Cita> CreateCita(Cita cita)
        {
            _bd.Cita.Add(cita);
            await _bd.SaveChangesAsync();
            return cita;
        }
        public async Task<IEnumerable<Cita>> GetCitaIdMedico(int idmedico,DateTime? fecha)
        {

            var query = _bd.Cita
                        .Where(m => m.idmedico == idmedico);

           
            if (fecha.HasValue)
            {
                query = query.Where(m => m.fechaCita.Date == fecha.Value.Date);
            }

            var citas = await query
                .OrderBy(m => m.fechaCita)
                .ThenBy(m => m.horaCita)
                .Select(m => new Cita
                {
                    idCita = m.idCita,
                    idmedico = m.idmedico,
                    idpaciente = m.idpaciente,
                    fechaCita = m.fechaCita,
                    horaCita = m.horaCita,
                    estado = m.estado,
                    motivo = m.motivo,
                    fechaCreacion = m.fechaCreacion,
                    fechaCancelacion = m.fechaCancelacion,
                    motivoCancelacion = m.motivoCancelacion
                })
                .ToListAsync();

            return citas;
        }
        public async Task<IEnumerable<Cita>> GetCitaIdPaciente(int idpaciente, DateTime? fecha)
        {

            var query = _bd.Cita
                       .Where(m => m.idpaciente == idpaciente);


            if (fecha.HasValue)
            {
                query = query.Where(m => m.fechaCita.Date == fecha.Value.Date);
            }

            var citas = await query
                .OrderBy(m => m.fechaCita)
                .ThenBy(m => m.horaCita)
                .Select(m => new Cita
                {
                    idCita = m.idCita,
                    idmedico = m.idmedico,
                    idpaciente = m.idpaciente,
                    fechaCita = m.fechaCita,
                    horaCita = m.horaCita,
                    estado = m.estado,
                    motivo = m.motivo,
                    fechaCreacion = m.fechaCreacion,
                    fechaCancelacion = m.fechaCancelacion,
                    motivoCancelacion = m.motivoCancelacion
                })
                .ToListAsync();

            return citas;
        }
        public async Task<bool> CancelarCita(int idcita, string motivo)
        {
            var cita = await _bd.Cita.FindAsync(idcita);
            if (cita == null)
                return false;
            cita.estado = "Cancelada";
            cita.motivoCancelacion = motivo;
            cita.fechaCancelacion = DateTime.Now;

            await _bd.SaveChangesAsync();
            return true;
        }
        public async Task<Cita> GetCitaId(int idcita)
        {
            var cita = await _bd.Cita.FindAsync(idcita);
            return cita;
        }

        public async Task<bool> CrearCita(Cita cita)
        {
            cita.estado = "Activa";
            _bd.Cita.Add(cita);
            await _bd.SaveChangesAsync();
            return true;
        }
        public async Task<bool> TieneCitasProximas(int idpaciente)
        {
            return await _bd.Cita.AnyAsync(c =>
                c.idpaciente == idpaciente &&
                c.estado == "Activa" &&
                c.fechaCita >= DateTime.Today);
        }

        public async Task<bool> TieneCitasProximasMedico(int idmedico)
        {
            return await _bd.Cita.AnyAsync(c =>
                c.idmedico == idmedico &&
                c.estado == "Activa" &&
                c.fechaCita >= DateTime.Today);
        }
        public async Task<int> ContarCancelacionesPaciente(int idpaciente)
        {
            return await _bd.Cita.CountAsync(c =>
                c.idpaciente == idpaciente &&
                c.estado == "Cancelada");
        }
    } }
