namespace MINI_AGENDA.Models.Exceptions
{
    public class BadRequestException:Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
