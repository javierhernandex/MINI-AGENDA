using MINI_AGENDA.Models.Cita;
using MINI_AGENDA.Models.Medico;
using MINI_AGENDA.Models.Pacientes;

namespace MINI_AGENDA.Services.IServices
{
    public interface ICitaService
    {
   
        Task <IEnumerable<Hora>> GetHorarioDisponible(int idMedico, DateTime fecha);
        Task<IEnumerable<Cita>> GetCitaIdMedico(int idmedico,DateTime? fecha);
        Task<IEnumerable<Cita>> GetCitaIdPaciente(int idpaciente, DateTime? fecha);
        Task<IEnumerable<Cita>> GetCitaAll();
        Task<bool> CancelarCita(int idcita, string motivo);
        Task<bool> Crearcita(Cita cita);
    }
}
