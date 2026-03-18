namespace MINI_AGENDA.Models.Medico
{
    public class HorarioAtencion
    {
        public int idHorario { get; set; }
        public int idMedico { get; set; }
        public byte diasemana { get; set; }
        public TimeSpan horainicio { get; set; }
        public TimeSpan horafin { get; set; }
        public bool Activo { get; set; }

    }
}
