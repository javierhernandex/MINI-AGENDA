namespace MINI_AGENDA.Models.Medico
{
 
    public class MMedico
    {
        public int idMedico { get; set; }
        public string nombre { get; set; } 
        public string apellido { get; set; }

        public DateTime FechaAlta { get; set; }
        public bool Estatus { get; set; }
    }
}
