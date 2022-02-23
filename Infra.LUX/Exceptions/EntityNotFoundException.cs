namespace Infra.LUX.Exceptions
{
    public class EntityNotFoundException : System.Exception
    {
        public EntityNotFoundException() : base() { }

        public EntityNotFoundException(string message) : base(message) { }
    }
}
