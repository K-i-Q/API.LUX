namespace Infra.LUX.Exceptions
{
    public class EntityAlreadyExistsException : System.Exception
    {
        public EntityAlreadyExistsException() : base() { }

        public EntityAlreadyExistsException(string message) : base(message) { }
    }
}
