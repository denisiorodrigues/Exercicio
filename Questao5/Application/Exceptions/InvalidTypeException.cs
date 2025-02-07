namespace Questao5.Application.Exceptions
{
    public class InvalidTypeException : CustomErrorException
    {
        public InvalidTypeException() : base("INVALID_TYPE", "Apenas os tipos “débito” ou “crédito” podem ser aceitos")
        {
        }
    }
}
