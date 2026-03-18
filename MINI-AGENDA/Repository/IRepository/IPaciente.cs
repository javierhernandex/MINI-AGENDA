using MINI_AGENDA.Models.Cita;
using MINI_AGENDA.Models.Pacientes;

namespace MINI_AGENDA.Repository.IRepository
{
    public interface IPaciente
    {
        Task<Paciente> GetPacienteid(int idpaciente);
        Task<Paciente> GetById(int id);
        Task<IEnumerable<Paciente>> GetAll();
        Task<Paciente> Add(Paciente paciente);
        Task<Paciente> Update(Paciente paciente);
        Task<bool> Delete(int id);
        Task<bool> ExisteEmail(string email);
        Task<bool> ExisteTelefono(string telefono);

    }
}
