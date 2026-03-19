using MINI_AGENDA.Models.Cita;
using MINI_AGENDA.Models.Medico;

namespace MINI_AGENDA.Repository.IRepository
{
    public interface ICita
    {
        Task<IEnumerable<Hora>> GetHorarioDisponible(int idMedico, DateTime fecha);
        Task<HorarioAtencion> GetHorarioAtencions(int idmedico,int diasemana);
        Task<IEnumerable<Cita>> GetCitaIdMedico(int idmedico,DateTime? fecha);
        Task<IEnumerable<Cita>> GetCitaIdPaciente(int idpaciente, DateTime? fecha);
        Task<IEnumerable<Cita>> GetCitasAll();
        Task<Cita> GetCitaId(int idcita);
        Task <bool> CancelarCita(int idcita, string motivo);
        Task<bool> CrearCita(Cita cita);
        Task <bool> TieneCitasProximas(int idpaciente);
        Task<bool> TieneCitasProximasMedico(int idmedico);
        Task<int> ContarCancelacionesPaciente(int idpaciente);
    }
}
