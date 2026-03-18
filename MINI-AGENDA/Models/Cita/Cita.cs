using Microsoft.Identity.Client;

namespace MINI_AGENDA.Models.Cita
{
    public class Cita
    {
        public int idCita { get; set; }
        public int idmedico { get; set; }
        public int idpaciente { get; set; }
        public DateTime fechaCita { get; set; }
        public TimeSpan horaCita { get; set; }
        public  string estado { get; set; }
        public string motivo { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaCancelacion { get; set; }
        public string motivoCancelacion { get; set; }
    }
}
