namespace Questao5.Application.Exceptions
{
    public class InvalidValueException : CustomErrorException
    {
        public InvalidValueException() : base("INVALID_VALUE", "Apenas valores positivos podem ser recebidos")
        {
        }
    }
}
