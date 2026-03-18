using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MINI_AGENDA.Models.Cita;
using MINI_AGENDA.Models.Pacientes;
using MINI_AGENDA.Models.Medico;
using MINI_AGENDA.Repository.IRepository;
using MINI_AGENDA.Repository;
using MINI_AGENDA.Services.IServices;

namespace MINI_AGENDA.Controllers
{
    public class CitasController : Controller
    {
        private readonly MiniAgendaDbContext _MiniAgendaDbContext;
        private readonly ICita _repositorios;
        private readonly ICitaService _citaService;
        public CitasController(MiniAgendaDbContext MPBConfigDbContext_, ICita repositorios,ICitaService citaService_)
        {
            this._MiniAgendaDbContext = MPBConfigDbContext_;
            this._repositorios = repositorios;
            this._citaService = citaService_;
        }
        /// <summary>
        /// busca y devuelve una lista de citas.
        /// </summary>

        /// <returns></returns>
        [HttpGet("Citas")]
        public async Task<IActionResult> GetCitas()
        {
            try
            {
                var disponibles = await _citaService.GetCitaAll();
                return Ok(disponibles);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);

            }

        }
        /// <summary>
        /// Busca y devuelve una lista de citas de un paciente específico, filtrando por el ID del paciente.
        /// </summary>

        /// <returns></returns>
        [HttpGet("CitasPaciente/{idpaciente}")]
        public async Task<IActionResult> GetCitasPaciente(int idpaciente, DateTime? fecha)
        {

            try
            {
                var disponibles = await _citaService.GetCitaIdPaciente(idpaciente,fecha);
                return Ok(disponibles);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);

            }

        }
        /// <summary>
        /// Busca y devuelve una lista de citas de un paciente específico desde la base de datos, filtrando por el ID del medico.
        /// </summary>

        /// <returns></returns>
        [HttpGet("CitasMedico/{idmedico}")]
        public async Task<IActionResult> GetCitasMEdico(int idmedico, DateTime? fecha)
        {

            try
            {
                var disponibles = await _citaService.GetCitaIdMedico(idmedico,fecha);
                return Ok(disponibles);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);

            }
        }
        /// <summary>
        /// Agenda una nueva cita médica validando que la fecha no sea en el pasado, que el médico tenga una especialidad asignada, que la cita esté dentro del horario de atención del médico y que el horario esté disponible.
        /// </summary>
        [HttpPost("CreaCitas")]
        public async Task<IActionResult> AgendarCita([FromBody] Cita model)
        {
            if (model == null)
                return BadRequest("Datos inválidos");

            try
            {
                var cita = await _citaService.Crearcita(model);
                return Ok(new
                {
                    mensaje = "Cita agendada correctamente",
                    cita
                });
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }
        /// <summary>
        /// Cancelar cita validando que no esté ya cancelada, que no sea una cita pasada y registrando el motivo de cancelación.
        /// </summary>
        [HttpPut("CancelarCita/{idcita}")]
        public async Task<IActionResult> CancelarCita(int idcita, string motivo)
        {
            try {
                var cita = await _citaService.CancelarCita(idcita, motivo);

                return Ok(new
                {
                    mensaje = "Cita cancelada correctamente"
                   
                });
            }
             catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            

           
        }
        /// <summary>
        /// muestra la disponibilidad de un médico para una fecha específica, devolviendo los horarios disponibles para agendar citas.
        /// </summary>
        [HttpGet("DisponibilidadMedico/{idmedico}/{fecha}")]
        public async Task<IActionResult> DisponibilidadMedico(int idmedico, DateTime fecha)
        {
            
            try
            {
                var disponibles = await _citaService.GetHorarioDisponible(idmedico, fecha.Date);
                return Ok(disponibles);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
