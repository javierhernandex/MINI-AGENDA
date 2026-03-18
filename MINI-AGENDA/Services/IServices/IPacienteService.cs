using MINI_AGENDA.Models.Pacientes;

namespace MINI_AGENDA.Services.IServices
{
    public interface IPacienteService
    {
        Task<Paciente> GetPaciente(int id);
        Task<IEnumerable<Paciente>> GetPacientes();
        Task<Paciente> CrearPaciente(Paciente paciente);
        Task<Paciente> ActualizarPaciente(Paciente paciente);
        Task<bool> EliminarPaciente(int id);
        
    }
}
