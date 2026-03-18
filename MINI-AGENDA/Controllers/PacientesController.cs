using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MINI_AGENDA.Models.Medico;
using MINI_AGENDA.Models.Pacientes;
using MINI_AGENDA.Repository;
using MINI_AGENDA.Repository.IRepository;
using MINI_AGENDA.Services;
using MINI_AGENDA.Services.IServices;

namespace MINI_AGENDA.Controllers
{
    [Route("api/Pacientes")]
    [ApiController]
    public class PacientesController : Controller
    {
       
        private readonly IPacienteService _service;

        public PacientesController( IPacienteService service)
        {
           
            _service = service;
        }


        /// <summary>
        /// Busca y devuelve una lista de pacientes desde la base de datos.
        /// </summary>

        /// <returns></returns>
        [HttpGet("Pacientes")]
        public async Task<IActionResult> GetPacientes()
        {

            var data = await _service.GetPacientes();
            return Ok(data);
         
        }
        /// <summary>
        /// Busca y devuelve un paciente específico desde la base de datos, filtrando por el ID del paciente.
        /// </summary>

        /// <returns></returns>
        [HttpGet("PacientesId/{idpaciente}")]
        public async Task<IActionResult> GetPacientesId(int idpaciente)
        {
            try
            {
                var data = await _service.GetPaciente(idpaciente);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
          
        }
        /// <summary>
        /// Crea un nuevo paciente
        /// </summary>
        [HttpPost("Pacientes")]
        public async Task<IActionResult> CrearPaciente([FromBody] Paciente model)
        {
            if (model == null)
                return BadRequest("Datos inválidos");

           
            if (string.IsNullOrEmpty(model.nombre) || string.IsNullOrEmpty(model.apellido))
                return BadRequest("Nombre y apellido son obligatorios");
           
            if (model.fechaNacimiento >= DateTime.Now)
                return BadRequest("Fecha de nacimiento inválida");

            try
            {
                var paciente = await _service.CrearPaciente(model);
                return Ok(new 
                {
                    mensaje="Paciente creado correctamente",
                    paciente });
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
        /// <summary>
        /// Actualiza un paciente existente
        /// </summary>
        [HttpPut("Pacientes")]
        public async Task<IActionResult> ActualizarPaciente( [FromBody] Paciente model)
        {
           
            try
            {
                var actpaciente = await _service.ActualizarPaciente(model);
                return Ok(new {
                mensaje="Paciente actualizado correctamente",
                        actpaciente
                    });
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Elimina un paciente existente validando si cuenta con citas prximas asociadas activas, si es así no se permite eliminarlo.
        /// </summary>
        [HttpDelete("Pacientes/{idpaciente}")]
        public async Task<IActionResult> EliminarPaciente(int idpaciente)
        {
            try
            {
                var elimpaciente = await _service.EliminarPaciente(idpaciente);
                return Ok(new
                {
                    mensaje = "Paciente eliminado correctamente"
                    
                });
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

           

        }
    }
}
