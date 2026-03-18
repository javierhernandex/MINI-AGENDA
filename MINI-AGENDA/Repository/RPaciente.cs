using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MINI_AGENDA.Models.Cita;
using MINI_AGENDA.Models.Pacientes;
using MINI_AGENDA.Repository.IRepository;

namespace MINI_AGENDA.Repository
{
    public class RPaciente : IPaciente
    {
        private readonly MiniAgendaDbContext _bd;

        public RPaciente(MiniAgendaDbContext bd_)
        {
            _bd = bd_;
        }

        public async Task<Paciente> GetPacienteid(int idpaciente)
        {
            return await _bd.Paciente
        .FirstOrDefaultAsync(x => x.idPaciente == idpaciente);

        }
        public async Task<Paciente> GetById(int id)
        {
            return await _bd.Paciente.FirstOrDefaultAsync(x => x.idPaciente == id);
        }

        public async Task<IEnumerable<Paciente>> GetAll()
        {
            return await _bd.Paciente.ToListAsync();
        }

        public async Task<Paciente> Add(Paciente paciente)
        {
            _bd.Paciente.Add(paciente);
            await _bd.SaveChangesAsync();
            return paciente;
        }

        public async Task<Paciente> Update(Paciente paciente)
        {
            //_bd.Paciente.Update(paciente);
            await _bd.SaveChangesAsync();
            return paciente;
        }

        public async Task<bool> Delete(int id)
        {
            var paciente = await _bd.Paciente.FindAsync(id);
            if (paciente == null) return false;

            _bd.Paciente.Remove(paciente);
            await _bd.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ExisteEmail(string email)
        {
            return await _bd.Paciente.AnyAsync(x => x.email == email);
        }
        public async Task<bool> ExisteTelefono(string telefono)
        {
            return await _bd.Paciente.AnyAsync(x => x.telefono == telefono);
        }
      
    }
}
