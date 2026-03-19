using Microsoft.AspNetCore.Http.HttpResults;
using MINI_AGENDA.Models.Cita;
using MINI_AGENDA.Models.Exceptions;
using MINI_AGENDA.Models.Medico;
using MINI_AGENDA.Models.Pacientes;
using MINI_AGENDA.Repository;
using MINI_AGENDA.Repository.IRepository;
using MINI_AGENDA.Services.IServices;

namespace MINI_AGENDA.Services
{
    public class CitaService : ICitaService
    {
        private readonly ICita _repo;
        private readonly IMedico _repoMedico;
        private readonly IPaciente _repoPaciente;

        public CitaService(ICita repo, IMedico repoMedico, IPaciente repoPaciente)
        {
            _repo = repo;
            _repoMedico = repoMedico;
            _repoPaciente = repoPaciente;
        }

        public async Task<IEnumerable<Hora>> GetHorarioDisponible(int idMedico, DateTime fecha)
        {
            var diaSemana = (int)fecha.DayOfWeek;
            var existemedico = await _repoMedico.GetMedicoid(idMedico);
            if (existemedico == null)
            {
                throw new NotFoundException("Medico no encontrado");
            }

            var horario = await _repo.GetHorarioAtencions(idMedico, diaSemana);

            if (horario == null)
                throw new NotFoundException("El médico no tiene horario ese día");

            return await _repo.GetHorarioDisponible(idMedico, fecha);
        }

        public async Task<IEnumerable<Cita>> GetCitaAll()
        {
            var citas = await _repo.GetCitasAll();
            if (citas.Count() == 0)
            {
                throw new NotFoundException("No se encontraron citas");
            }
            return await _repo.GetCitasAll();
        }
        public async Task<IEnumerable<Cita>> GetCitaIdMedico(int idmedico, DateTime? fecha)
        {
            var existemedico = await _repoMedico.GetMedicoid(idmedico);
            if (existemedico == null)
            {
                throw new NotFoundException("Medico no encontrado");
            }
            var citas = await _repo.GetCitaIdMedico(idmedico,fecha);
            if (citas.Count() == 0)
            {
                throw new NotFoundException($"No se encontraron citas para el médico con ID {idmedico} ");
            }

            return await _repo.GetCitaIdMedico(idmedico,fecha);
        }
        public async Task<IEnumerable<Cita>> GetCitaIdPaciente(int idpaciente, DateTime? fecha)
        {
            var existepaciente = await _repoPaciente.GetPacienteid(idpaciente);

            if (existepaciente == null)
            {
                throw new NotFoundException($"No existe paciente con ID {idpaciente}");
            }


            var citas = await _repo.GetCitaIdPaciente(idpaciente,fecha);
            if (citas.Count() == 0)
            {
                throw new NotFoundException($"No se encontraron citas para el paciente con ID {idpaciente}");
            }

            return await _repo.GetCitaIdPaciente(idpaciente,fecha);
        }
        public async Task<CancelarCitaResponse> CancelarCita(int idcita, string motivo)
        {
            var existecita = await _repo.GetCitaId(idcita);

            if (existecita == null)
            {
                throw new NotFoundException($"No existe cita con ID {idcita}");
            }
            if (existecita.estado == "Cancelada")
            {
                throw new ConflictException($"La cita con ID {idcita} ya está cancelada");
            }
            var fechaHoraCita = existecita.fechaCita.Date + existecita.horaCita;

            if (fechaHoraCita < DateTime.Now)
                throw new BadRequestException("No puedes cancelar una cita pasada");

            var result = await _repo.CancelarCita(idcita, motivo);
            if (!result)
            {
                throw new BadRequestException($"No se pudo cancelar la cita con ID {idcita}");
            }
            var totalCancelaciones = await _repo.ContarCancelacionesPaciente(existecita.idpaciente);

            return new CancelarCitaResponse
            {
                success = true,
                mensaje = totalCancelaciones > 3
                    ? "Cita cancelada. Advertencia: el paciente tiene múltiples cancelaciones"
                    : "Cita cancelada correctamente",
                advertencia = totalCancelaciones >= 3
            };
          
        }
        public async Task<bool> Crearcita(Cita cita)
        {
            var existemedico = await _repoMedico.GetMedicoid(cita.idmedico);
            if (existemedico == null)
            {
                throw new NotFoundException("Medico no encontrado");
            }
            var medicotieneespecialidad = await _repoMedico.MedicoTienEspecialidad(cita.idmedico);
            if (!medicotieneespecialidad)
                throw new ConflictException("El médico no tiene especialidad asignada");

            var existepaciente = await _repoPaciente.GetPacienteid(cita.idpaciente);
            if (existepaciente == null)
            {
                throw new NotFoundException($"No existe paciente con ID {cita.idpaciente}");
            }
            var fechaHoraCita = cita.fechaCita.Date + cita.horaCita;
            if (fechaHoraCita < DateTime.Now)
                throw new ConflictException("No puedes crear una cita en el pasado");

            var especialidadmedico = await _repoMedico.GetMedicoEspecialidad(cita.idmedico);
            var duracion = await _repoMedico.GetEspecialidad(especialidadmedico.idEspecialidad);


            var horaInicio = cita.horaCita;
            var horaFin = cita.horaCita.Add(TimeSpan.FromMinutes(duracion.DuracionCita));
            var diaSemana = (int)cita.fechaCita.DayOfWeek;
            var horario = await _repo.GetHorarioAtencions(cita.idmedico, diaSemana);

            if(horario==null)
                throw new ConflictException("El medico no cuenta con horario en esta fecha");

            if (horaInicio < horario.horainicio || horaFin > horario.horafin)
                throw new ConflictException("La cita está fuera del horario del médico");

            var horarioDisponible = await _repo.GetHorarioDisponible(cita.idmedico, cita.fechaCita);
            if (!horarioDisponible.Any(h => h.hora == cita.horaCita))
            {
                
                var sugerencias = horarioDisponible
                                 .Where(h => h.hora > cita.horaCita)
                                .OrderBy(h => h.hora)
                                .Take(5)
                                .Select(h => h.hora.ToString(@"hh\:mm")) 
                                .ToList();

              
                string listaSugerencias = sugerencias.Any()
                    ? string.Join(", ", sugerencias)
                    : "No hay más citas disponibles para hoy";

                throw new ConflictException($"El horario {cita.horaCita:hh\\:mm} no está disponible. " +
                                            $"Próximas opciones: {listaSugerencias}");
               
            }
          
            var newcita = new Cita
            {
                idmedico = cita.idmedico,
                idpaciente = cita.idpaciente,
                fechaCita = cita.fechaCita.Date,
                horaCita = cita.horaCita,
                motivo = cita.motivo,
                estado = "Activa",


                fechaCreacion = DateTime.Now
            };

            var result = await _repo.CrearCita(newcita);
            if (!result)
            {
                throw new BadRequestException("No se pudo crear la cita");
            }
            return result;
        }
    }
}
