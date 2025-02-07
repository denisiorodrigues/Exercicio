using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Questao5.Application.Exceptions
{
    public class IncativeAccountException : CustomErrorException
    {
        public IncativeAccountException() : base("INACTIVE_ACCOUNT", "Apenas contas correntes ativas podem receber movimentação")
        {
        }
    }
}
