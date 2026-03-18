namespace MINI_AGENDA.Models.Medico
{
    public class Especialidad
    {
        public int idEspecialidad { get; set; }
        public string Descripcion { get; set; }

        public Int16 DuracionCita { get; set; }
        public bool Estatus { get; set; }
    }

}
