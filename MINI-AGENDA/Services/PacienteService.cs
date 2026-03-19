using MINI_AGENDA.Models.Exceptions;
using MINI_AGENDA.Models.Pacientes;
using MINI_AGENDA.Repository;
using MINI_AGENDA.Repository.IRepository;
using MINI_AGENDA.Services.IServices;

namespace MINI_AGENDA.Services
{
    public class PacienteService:IPacienteService
    {
        private readonly IPaciente _repo;
        private readonly ICita _Cita;

        public PacienteService(IPaciente repo, ICita cita_)
        {
            _repo = repo;
            _Cita = cita_;
        }

        public async Task<Paciente> GetPaciente(int id)
        {
            var existe = await _repo.GetById(id);

            if (existe==null)
               throw new NotFoundException("Id de Paciente no existe");

            return existe;
        }

        public async Task<IEnumerable<Paciente>> GetPacientes()
        {
            return await _repo.GetAll();
        }

        public async Task<Paciente> CrearPaciente(Paciente paciente)
        {
            var existeemail = await _repo.ExisteEmail(paciente.email);

            if (existeemail)
                throw new ConflictException("El email ya está registrado");
            var existetelefono = await _repo.ExisteTelefono(paciente.telefono);
            if (existetelefono)
                throw new ConflictException("El telefono ya está registrado");
            paciente.fechaAlta = DateTime.Now;

            return await _repo.Add(paciente);
        }

        public async Task<Paciente> ActualizarPaciente(Paciente paciente)
        {
            if (paciente==null)
                throw new BadRequestException("Datos invalidos");

            var existe = await _repo.GetPacienteid(paciente.idPaciente);

            if (existe == null)
                throw new NotFoundException("Paciente no encontrado");
            var existeemail = await _repo.ExisteEmail(paciente.email);
            if (existeemail)
                throw new ConflictException("El email ya está registrado");
            var existetelefono = await _repo.ExisteTelefono(paciente.telefono);
            if (existetelefono)
                throw new ConflictException("El telefono ya está registrado");
            existe.nombre = paciente.nombre;
            existe.apellido = paciente.apellido;
            existe.email = paciente.email;
            existe.telefono = paciente.telefono;


            return await _repo.Update(existe);
        }

        public async Task<bool> EliminarPaciente(int idpaciente)

        {
            if (idpaciente <= 0)
                throw new BadRequestException("Id inválido");


            var tieneCitas = await _Cita.TieneCitasProximas(idpaciente);

            if (tieneCitas)
                throw new ConflictException("El paciente tiene citas próximas y no se puede eliminar");



            var existe = await _repo.GetPacienteid(idpaciente);

            if (existe == null)
                throw new NotFoundException("Paciente no encontrado");
            return await _repo.Delete(idpaciente);
        }
      
    }
}
