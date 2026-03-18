using MINI_AGENDA.Models.Medico;

namespace MINI_AGENDA.Services.IServices
{
    public interface IMedicoService
    {
        Task <IEnumerable<MMedico>> GetMedicosAll();
        Task<MMedico> GetMedicoid(int id);
        Task <bool> MedicoTienEspecialidad(int idmedico);
        Task<IEnumerable<MedicoEspecialidad>> MedicoEspecialidadAll();
        Task <Especialidad> GetEspecialidad(int idespecialidad);
        Task<Especialidad> UpdateEspecialidad(Especialidad especialidad);
        Task<bool> DeleteEspecialidad(int idespecialidad);
        Task <MMedico> CreatMedico(MMedico Medico); 
        Task<MMedico> UpdateMedico(MMedico Medico);
         Task<bool> DeleteMedico(int idmedico);
        Task <IEnumerable<Especialidad>> GetAllEspecialides();
        Task<Especialidad> CreateEspecialida(Especialidad especialidad);
        Task<bool> cretemedicoespecialida(MedicoEspecialidad medicoEspecialidad);
        Task<IEnumerable<HorarioAtencion>> GetHorarioAtencionAsync(int? idmedico);
        Task<bool> createhorarioatencion(HorarioAtencion horarioatencion);
        Task<HorarioAtencion> updatehorariotencion(HorarioAtencion horario);
        Task<bool> DeleteHorarioAtencion(int idhorarioatencion);
    }
}
