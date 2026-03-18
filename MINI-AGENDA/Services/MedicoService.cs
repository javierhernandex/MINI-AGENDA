using MINI_AGENDA.Models.Cita;
using MINI_AGENDA.Models.Medico;
using MINI_AGENDA.Repository;
using MINI_AGENDA.Repository.IRepository;
using MINI_AGENDA.Services.IServices;

namespace MINI_AGENDA.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedico _repo;
        private readonly ICita _cita;

        public MedicoService(IMedico repo, ICita cita)
        {
            _repo = repo;
            _cita = cita;
        }

        public async Task<IEnumerable<MMedico>> GetMedicosAll()
        {
            return await _repo.GetMedicosAll();
        }
        public async Task<MMedico> GetMedicoid(int idMedico)
        {

            return await _repo.GetMedicoid(idMedico);
        }
        public async Task<IEnumerable<MedicoEspecialidad>> MedicoEspecialidadAll()
        {
            return await _repo.MedicoEspecialidadAll();
        }
        public async Task<bool> MedicoTienEspecialidad(int idMedico)
        {
            return await _repo.MedicoTienEspecialidad(idMedico);
        }

        public async Task<IEnumerable<Especialidad>> GetAllEspecialides()
        {
            return await _repo.GetAllEspecialides();
        }
        public async Task<Especialidad> CreateEspecialida(Especialidad especialidad)
        {
            if (especialidad.DuracionCita <= 10)
                throw new Exception("la duracion no puede ser menor a 10 min");
            var newespecialidad = new Especialidad
            {
                Descripcion = especialidad.Descripcion,
                DuracionCita = especialidad.DuracionCita,
                Estatus = true
            };
            return await _repo.CreateEspecialida(newespecialidad);
        }
        public async Task<Especialidad> GetEspecialidad(int idEspecialidad)
        {
            return await _repo.GetEspecialidad(idEspecialidad);
        }
        public async Task<Especialidad> UpdateEspecialidad(Especialidad UpdateEspecialidad)
        {
            var existe = await _repo.GetEspecialidad(UpdateEspecialidad.idEspecialidad);
            if (existe == null)
            {
                throw new Exception("Id de especialidad no existe");
            }
            if (UpdateEspecialidad.DuracionCita <= 10)
                throw new Exception("la duracion no puede ser menor a 10 min");

            var especialidad = await _repo.UpdateEspecialidad(UpdateEspecialidad);
            if (especialidad == null)
            {
                throw new Exception("Error al actualizar especialidad");
            }
            return especialidad;
        }

        public async Task<bool> DeleteEspecialidad(int idEspecialidad)
        {
            var existe = await _repo.GetEspecialidad(idEspecialidad);
            if (existe == null)
            {
                throw new Exception("Id de especialidad no existe");
            }
            var tieneMedicos = await _repo.TieneMedicoEspecialidad(idEspecialidad);
            if (tieneMedicos)
            {
                throw new Exception("No se puede eliminar la especialidad, tiene medicos asociados");
            }
            return await _repo.DeleteEspecialidad(idEspecialidad);
        }
        public async Task<MMedico> CreatMedico(MMedico NewMedico)
        {
            var medico = new MMedico
            {
                nombre = NewMedico.nombre,
                apellido = NewMedico.apellido,
                FechaAlta = DateTime.Now,
                Estatus = true

            };
            return await _repo.CreatMedico(medico);
        }
        public async Task<MMedico> UpdateMedico(MMedico UpdateMedico)
        {
            var existe = await _repo.GetMedicoid(UpdateMedico.idMedico);
            if (existe == null)
            {
                throw new Exception("Id de Paciente no existe");
            }



            var medico = await _repo.UpdateMedico(UpdateMedico);
            if (medico == null)
            {
                throw new Exception("Error al actualizar medico");
            }
            return medico;
        }
        public async Task<bool> DeleteMedico(int idMedico)
        {
            var existe = await _repo.GetMedicoid(idMedico);
            if (existe == null)
            {
                throw new Exception("Id de medico no existe");
            }
            var tienecitasproximas = await _cita.TieneCitasProximasMedico(idMedico);
            if (tienecitasproximas)
            {
                throw new Exception("Medico no se puede eliminar, cuenta con citas proximas");
            }

            return await _repo.DeleteMedico(idMedico);
        }
        public async Task<bool> cretemedicoespecialida(MedicoEspecialidad medicoEspecialidad)
        {
            var existeMedico = await _repo.GetMedicoid(medicoEspecialidad.idMedico);
            if (existeMedico == null)
            {
                throw new Exception("Id de medico no existe");
            }
            var existeEspecialidad = await _repo.GetEspecialidad(medicoEspecialidad.idEspecialidad);
            if (existeEspecialidad == null)
            {
                throw new Exception("Id de especialidad no existe");
            }
            return await _repo.cretemedicoespecialida(medicoEspecialidad);
        }
        public async Task<IEnumerable<HorarioAtencion>> GetHorarioAtencionAsync(int? idMedico)
        {
            if (idMedico != null)
            {
                var existeMedico = await _repo.GetMedicoid(idMedico.Value);
                if (existeMedico == null)
                {
                    throw new Exception("Id de medico no existe");
                }
            }

            return await _repo.GetHorarioAtencionAsync(idMedico);
        }
        public async Task<bool> createhorarioatencion(HorarioAtencion horarioatencion)
        {
            var existeMedico = await _repo.GetMedicoid(horarioatencion.idMedico);
            if (existeMedico == null)
            {
                throw new Exception("Id de medico no existe");
            }


            if (horarioatencion.horainicio >= horarioatencion.horafin)
                throw new Exception("La hora de inicio debe ser menor que la hora fin");

            if (!(horarioatencion.diasemana >= 1 && horarioatencion.diasemana <= 7))
                throw new Exception("Dia de la semana va de 1 a 7");
            var horarioDisponible = await _repo.GetHorarioAtencionAsync(horarioatencion.idMedico);
            if (horarioDisponible.Any(h => h.diasemana == horarioatencion.diasemana))
                throw new Exception("El médico ya tiene horario registrado para ese día");
            var horario = new HorarioAtencion
            {
                idMedico = horarioatencion.idMedico,
                diasemana = horarioatencion.diasemana,
                horainicio = horarioatencion.horainicio,
                horafin = horarioatencion.horafin,
                Activo = true
            };

            return await _repo.createhorarioatencion(horario);
        }
        public async Task<HorarioAtencion> updatehorariotencion(HorarioAtencion horario)
        {
            var existeMedico = await _repo.GetMedicoid(horario.idMedico);
            if (existeMedico == null)
            {
                throw new Exception("Id de medico no existe");
            }
            if (horario.horainicio >= horario.horafin)
                throw new Exception("La hora de inicio debe ser menor que la hora fin");
            if (!(horario.diasemana >= 1 && horario.diasemana <= 7))
                throw new Exception("Dia de la semana va de 1 a 7");
            var horarioDisponible = await _repo.GetHorarioAtencionAsync(horario.idMedico);
            if (horarioDisponible.Any(h => h.diasemana == horario.diasemana && h.idHorario != horario.idHorario))
                throw new Exception("El médico ya tiene horario registrado para ese día");
            return await _repo.updatehorariotencion(horario);
        }
        public async Task<bool> DeleteHorarioAtencion(int idHorarioAtencion)
        {
            var existe = await _repo.GetHorarioAtencionAsync(null);
            if (existe == null || !existe.Any(h => h.idHorario == idHorarioAtencion))
            {
                throw new Exception("Id de horario de atención no existe");
            }
            return await _repo.DeleteHorarioAtencion(idHorarioAtencion);
        }
    }
}