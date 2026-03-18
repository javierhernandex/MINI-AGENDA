namespace MINI_AGENDA.Models.Pacientes
{
    public class Paciente
    {
        public int idPaciente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public DateTime fechaAlta { get; set; }
    }
}
