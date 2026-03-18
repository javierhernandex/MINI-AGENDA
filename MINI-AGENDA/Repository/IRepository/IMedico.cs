using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using MINI_AGENDA.Models.Medico;
namespace MINI_AGENDA.Repository.IRepository
{
    public interface IMedico
    {
        Task<IEnumerable<MMedico>> GetMedicosAll();
        Task<MMedico> GetMedicoid(int idmedico);
        Task<IEnumerable<Especialidad>> GetAllEspecialides();
        Task<bool> MedicoTienEspecialidad(int idmedico);
        Task <IEnumerable< MedicoEspecialidad>> MedicoEspecialidadAll();
        Task<MedicoEspecialidad> GetMedicoEspecialidad(int idmedico);
        Task<bool> TieneMedicoEspecialidad(int idespecialidad);
        Task<Especialidad> CreateEspecialida(Especialidad especialidad);
        Task<Especialidad> GetEspecialidad(int idespecialidad);
        Task<Especialidad> UpdateEspecialidad(Especialidad especialidad);
        Task<bool> DeleteEspecialidad(int idespecialidad);  
        Task<MMedico> CreatMedico(MMedico Medico);
        Task <MMedico> UpdateMedico(MMedico Medico);
         Task<bool> DeleteMedico(int idmedico);
        Task<bool> cretemedicoespecialida(MedicoEspecialidad medicoEspecialidad);
        Task<IEnumerable<HorarioAtencion>> GetHorarioAtencionAsync(int? idmedico);
        Task<bool> createhorarioatencion(HorarioAtencion horarioatencion);
        Task<HorarioAtencion> updatehorariotencion(HorarioAtencion horario);
        Task<bool> DeleteHorarioAtencion(int idhorarioatencion);



    }
}
