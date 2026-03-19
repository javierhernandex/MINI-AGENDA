namespace MINI_AGENDA.Models.Exceptions
{
    public class ConflictException:Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}
