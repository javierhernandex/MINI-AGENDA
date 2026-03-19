using Microsoft.AspNetCore.Mvc;
using MINI_AGENDA.Models.Medico;
using MINI_AGENDA.Services.IServices;

namespace MINI_AGENDA.Controllers
{
    [Route("api/Medicos")]
    [ApiController]
    public class MedicosController : Controller
    {
      
        private readonly IMedicoService _MedicoService;
        public MedicosController(IMedicoService medicoService_)
        {
            this._MedicoService = medicoService_;
        }

        /// <summary>
        /// Busca Informacion del los medicos registrados en el sistema, se muestra el id, nombre, apellido, fecha de alta y estatus del medico.
        /// </summary>

        /// <returns></returns>
        [HttpGet("Medicos")]
        public async Task<IActionResult> GetMedicos()
        {
            
            var existe = await _MedicoService.GetMedicosAll();
            if (existe == null)
                return NotFound("No se encontraron médicos");   

            return Ok(existe);



        }
        /// <summary>
        /// Crea un nuevo médico en el sistema
        /// </summary>
        [HttpPost("Medicos")]
        public async Task<IActionResult> CrearMedico([FromBody] MMedico model)
        {
            if (model == null)
                return BadRequest("Datos inválidos");
            var crear = await _MedicoService.CreatMedico(model);

            return Ok(new
            {
                mensaje="Medico creado correctamente",
                crear
            });
        }
        /// <summary>
        /// Actualiza un médico existente
        /// </summary>
        [HttpPut("Medicos/{id}")]
        public async Task<IActionResult> ActualizarMedico(int id, [FromBody] MMedico model)
        {
           var medico = await _MedicoService.UpdateMedico(new MMedico
            {
                idMedico = id,
                nombre = model.nombre,
                apellido = model.apellido,
                Estatus = model.Estatus
            });

            return Ok(new
            {
                mensaje = "Medico actualizado correctamente",
                medico
            });
        }
        /// <summary>
        /// Elimina un médico por id
        /// </summary>
        [HttpDelete("Medicos/{id}")]
        public async Task<IActionResult> EliminarMedico(int id)
        {
            try
            {
                var delete = await _MedicoService.DeleteMedico(id);

                return Ok(new
                {
                    mensaje = "Elminado Correctamente"
                }
                    );
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }

        }

        /// <summary>
        /// trae informacion de las especialidades registradas en el sistema, se muestra el id, descripcion, duracion de cita y estatus de la especialidad.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Especialidades")]
        public async Task<IActionResult> GetEspecialidades()
        {
           

            try
            {
                var especialidades = await _MedicoService.GetAllEspecialides();

                return Ok(especialidades);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }
        /// <summary>
        /// trae informacion de una especialidad en especifico, se muestra el id, descripcion, duracion de cita y estatus de la especialidad.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Especialidad/{idEspecialidad}")]
        public async Task<IActionResult> GetEspecialid(int idEspecialidad)
        {

         
            try
            {
                var especialidad = await _MedicoService.GetEspecialidad(idEspecialidad);
                if (especialidad == null)
                    return NotFound("Especialidad no encontrada");

                return Ok(especialidad);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }


        }
        /// <summary>
        /// Crea una nueva especialidad
        /// </summary>
        [HttpPost("Especialidad")]
        public async Task<IActionResult> CrearEspecialidad([FromBody] Especialidad model)
        {
            if (model == null)
                return BadRequest("Datos inválidos");
          
          
            try
            {
                var especialidad = await _MedicoService.CreateEspecialida(model);

                return Ok(new
                {
                    mensaje = "Especialidad creada",
                    especialidad
                });
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
        /// <summary>
        /// Actualiza una especialidad existente
        /// </summary>
        [HttpPut("Especialidad/{idEspecialidad}")]
        public async Task<IActionResult> ActualizarEspecialidad(int idEspecialidad, [FromBody] Especialidad model)
        {

            try
            {
                model.idEspecialidad = idEspecialidad;
                var update = await _MedicoService.UpdateEspecialidad(model);

                return Ok(new
                {
                    mensaje = "Especialidad actualizada",
                    update
                });
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
         
        }
        /// <summary>
        /// Elimina una especialidad
        /// </summary>
        [HttpDelete("Especialidad/{idEspecialidad}")]
        public async Task<IActionResult> EliminarEspecialidad(int idEspecialidad)
        {
            try
            {
                var update = await _MedicoService.DeleteEspecialidad(idEspecialidad);

                return Ok(new
                {
                    mensaje = "Especialidad eliminada"
                   
                });
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// trae informacion de las especialidades asignadas a los medicos registrados en el sistema, se muestra el id del medico y el id de la especialidad.
        /// </summary>
        /// <returns></returns>
        [HttpGet("MedicoEspecialidad")]
        public async Task<IActionResult> GetMedicoEspecialidad()
        {

            try
            {
                var all = await _MedicoService.MedicoEspecialidadAll();

                return Ok(all);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }



        }
        /// <summary>
        /// Crea una nueva relacion medico -especialidad
        /// </summary>
        [HttpPost("MedicoEspecialidad")]
        public async Task<IActionResult> CrearMedicoEspecialidad([FromBody] MedicoEspecialidad model)
        {

            try
            {
                var creado = await _MedicoService.cretemedicoespecialida(model);

                if (creado==false)
                    return NotFound("No se pudo crear la relación médico-especialidad");    


                return Ok("Creado correctamente");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


        /// <summary>
        /// trae informacion de los horarios de atencion registrados en el sistema, se muestra el id del horario, id del medico, hora de inicio, hora de fin y estatus del horario.
        /// </summary>
        /// <returns></returns>
        [HttpGet("HorarioAtencion")]
        public async Task<IActionResult> GetHorarioAtencion(int? idmedico)
        {
            try
            {
                var horario = await _MedicoService.GetHorarioAtencionAsync(idmedico);

                return Ok(horario);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
           

        }

        /// <summary>
        /// Crea un nuevo horario de atención
        /// </summary>
        [HttpPost("HorarioAtencion")]
        public async Task<IActionResult> CrearHorario([FromBody] HorarioAtencion model)
        {
            try
            {
                var crear = await _MedicoService.createhorarioatencion(model);
               if (crear==false)
                    return BadRequest("No se pudo crear el horario");

                return Ok("Horario creado");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
        /// <summary>
        /// Actualiza un horario de atención
        /// </summary>
        [HttpPut("HorarioAtencion/{id}")]
        public async Task<IActionResult> ActualizarHorario(int id, [FromBody] HorarioAtencion model)
        {
            try
            {
                var update = await _MedicoService.updatehorariotencion(new HorarioAtencion
                {
                    idHorario = id,
                    idMedico = model.idMedico,
                    diasemana = model.diasemana,
                    horainicio = model.horainicio,
                    horafin = model.horafin,
                    Activo = model.Activo
                });
                if (update == null)
                    return BadRequest("No se pudo actualizar el horario");

                return Ok("Horario actualizado");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
        /// <summary>
        /// Elimina un horario
        /// </summary>
        [HttpDelete("HorarioAtencion/{id}")]
        public async Task<IActionResult> EliminarHorario(int id)
        {
            try
            {
                var update = await _MedicoService.DeleteHorarioAtencion(id);
                if (update == false)
                    return BadRequest("No se pudo eliminar el horario");

                return Ok("Horario eliminado");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

    }
}
