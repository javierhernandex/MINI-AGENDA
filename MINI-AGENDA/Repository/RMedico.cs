using Microsoft.EntityFrameworkCore;
using MINI_AGENDA.Controllers;
using MINI_AGENDA.Models.Cita;
using MINI_AGENDA.Models.Medico;
using MINI_AGENDA.Models.Pacientes;
using MINI_AGENDA.Repository.IRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MINI_AGENDA.Repository
{
    public class RMedico : IMedico
    {
        private readonly MiniAgendaDbContext _bd;

        public RMedico(MiniAgendaDbContext bd_)
        {
            _bd = bd_;
        }

        public async Task<IEnumerable<MMedico>> GetMedicosAll()
        {
            return await _bd.Medico.ToListAsync();
        }
        public async Task<MMedico> GetMedicoid(int idmedico)
        {
            return await _bd.Medico
        .FirstOrDefaultAsync(x => x.idMedico == idmedico);

        }
        public async Task<IEnumerable<Especialidad>> GetAllEspecialides()
        {

            return await _bd.Especialidad.ToListAsync();
        }
        public async Task<bool> MedicoTienEspecialidad(int idmedico)
        {
            var especialidad = await _bd.MedicoEspecialidad
               .FirstOrDefaultAsync(x => x.idMedico == idmedico);
            if (especialidad == null)
            {
                return false;
            }

            return true;
        }
        public async Task<IEnumerable<MedicoEspecialidad>> MedicoEspecialidadAll()
        {
            return await _bd.MedicoEspecialidad.ToListAsync();
        }
        public async Task<MedicoEspecialidad> GetMedicoEspecialidad(int idmedico)
        {
            return await _bd.MedicoEspecialidad.FirstOrDefaultAsync(x => x.idMedico == idmedico);
        }
        public async Task<Especialidad> GetEspecialidad(int idepecialidad)
        {
            var especialidad = await _bd.Especialidad
               .FirstOrDefaultAsync(x => x.idEspecialidad == idepecialidad);

            return especialidad;
        }
        public async Task<Especialidad> CreateEspecialida(Especialidad especialidad)
        {
            _bd.Especialidad.Add(especialidad);
            await _bd.SaveChangesAsync();
            return especialidad;
        }

        public async Task<Especialidad> UpdateEspecialidad(Especialidad especialidad)
        {
            var especialidadExistente = await _bd.Especialidad.FirstOrDefaultAsync(x => x.idEspecialidad == especialidad.idEspecialidad);
            if (especialidadExistente == null)
            {
                return null;
            }
            especialidadExistente.Descripcion = especialidad.Descripcion;
            especialidadExistente.DuracionCita = especialidad.DuracionCita;
            especialidadExistente.Estatus = especialidad.Estatus;
            _bd.Especialidad.Update(especialidadExistente);
            await _bd.SaveChangesAsync();
            return especialidadExistente;
        }
        public async Task<bool> DeleteEspecialidad(int idEspecialidad)
        {
            var especialidad = await _bd.Especialidad.FirstOrDefaultAsync(x => x.idEspecialidad == idEspecialidad);
            if (especialidad == null)
            {
                return false;
            }

            _bd.Especialidad.Remove(especialidad);
            await _bd.SaveChangesAsync();
            return true;
        }

        public async Task<MMedico> CreatMedico(MMedico Medico)
        {
            _bd.Medico.Add(Medico);
            await _bd.SaveChangesAsync();
            return Medico;
        }
        public async Task<MMedico> UpdateMedico(MMedico Medico)
        {
            var medico = await _bd.Medico.FirstOrDefaultAsync(x => x.idMedico == Medico.idMedico);
            if (medico == null)
            {
                return null;
            }
            medico.nombre = Medico.nombre;
            medico.apellido = Medico.apellido;
            medico.Estatus = Medico.Estatus;
            _bd.Medico.Update(medico);
            await _bd.SaveChangesAsync();
            return medico;
        }
        public async Task<bool> DeleteMedico(int idmedico)
        {
            var medico = await _bd.Medico.FirstOrDefaultAsync(x => x.idMedico == idmedico);
            if (medico == null)
            {
                return false;
            }
            _bd.Medico.Remove(medico);
            await _bd.SaveChangesAsync();
            return true;
        }
        public async Task<bool> TieneMedicoEspecialidad(int idespecialidad)
        {
            var medicoEspecialidad = await _bd.MedicoEspecialidad.FirstOrDefaultAsync(x => x.idEspecialidad == idespecialidad);
            if (medicoEspecialidad == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> cretemedicoespecialida(MedicoEspecialidad medicoEspecialidad)
        {
            _bd.MedicoEspecialidad.Add(medicoEspecialidad);
            await _bd.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<HorarioAtencion>> GetHorarioAtencionAsync(int? idmedico)
        {
            var query = _bd.HorarioAtencion.AsQueryable();

            if (idmedico.HasValue && idmedico != 0)
            {
                query = query.Where(m => m.idMedico == idmedico);
            }
            return await query
                .OrderBy(m => m.diasemana)
                .ThenBy(m => m.horainicio)
                .ToListAsync();

        }
        public async Task<bool> createhorarioatencion(HorarioAtencion horarioatencion)
        {
            _bd.HorarioAtencion.Add(horarioatencion);
            await _bd.SaveChangesAsync();
            return true;
        }
        public async Task<HorarioAtencion> updatehorariotencion(HorarioAtencion horario)
        {
            var horarioExistente = await _bd.HorarioAtencion.FirstOrDefaultAsync(x => x.idHorario == horario.idHorario);
            if (horarioExistente == null)
            {
                return null;
            }
            horarioExistente.idMedico = horario.idMedico;
            horarioExistente.diasemana = horario.diasemana;
            horarioExistente.horainicio = horario.horainicio;
            horarioExistente.horafin = horario.horafin;
            _bd.HorarioAtencion.Update(horarioExistente);
            await _bd.SaveChangesAsync();
            return horarioExistente;
        }
        public async Task<bool> DeleteHorarioAtencion(int idhorarioatencion)
        {
            var horario = await _bd.HorarioAtencion.FirstOrDefaultAsync(x => x.idHorario == idhorarioatencion);
            if (horario == null)
            {
                return false;
            }
            _bd.HorarioAtencion.Remove(horario);
            await _bd.SaveChangesAsync();
            return true;
        }
    }
}
