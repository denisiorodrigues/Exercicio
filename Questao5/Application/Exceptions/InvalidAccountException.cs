namespace Questao5.Application.Exceptions
{
    public class InvalidAccountException: CustomErrorException
    {
        public InvalidAccountException() : base("INVALID_ACCOUNT", "Apenas contas correntes cadastradas não podem receber movimentação")
        {
        }
    }
}
