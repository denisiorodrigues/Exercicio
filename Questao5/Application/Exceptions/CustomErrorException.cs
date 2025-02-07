namespace Questao5.Application.Exceptions
{
    public class CustomErrorException : Exception
    {
        public string Message { get; }
        public string Type { get; }

        public CustomErrorException(string type, string message) : base(message)
        {
            this.Message = message;
            this.Type = type;
        }
    }
}
