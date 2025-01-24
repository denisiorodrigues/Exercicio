using Questao1.Model;
using ProgramQuestao1 = Questao1.Program;

namespace Exercicio.Test.Questao1;

public class ProgramTest
{
    [Fact]
    public void FluxoCadastroContaComSaldoInicial()
    {
        // Arrange
        string readConsole = "5447\r\nMilton Gonçalves\r\ns\r\n350.00\r\n200\r\n199\r\n";
        
        var stringWriter = new StringWriter();
        var stringReader = new StringReader(readConsole);
        Console.SetOut(stringWriter);
        Console.SetIn(stringReader);
        // Act

        ProgramQuestao1.Main(new string[0]);

        // Assert
        var output = stringWriter.ToString().Replace("\r\n", "\n");
        
        Assert.Contains("Conta 5447, Titular: Milton Gonçalves, Saldo: $350.00", output);
        Assert.Contains("Conta 5447, Titular: Milton Gonçalves, Saldo: $550.00", output);
        Assert.Contains("Conta 5447, Titular: Milton Gonçalves, Saldo: $347.50", output);

        // Clean up
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        Console.SetIn(new StreamReader(Console.OpenStandardInput()));
    }

    [Fact]
    public void FluxoCadastroContaSemSaldoInicial()
    {
        // Arrange
        string readConsole = "5139\r\nElza Soares\r\nn\r\n300.00\r\n298.00\r\n";

        var stringWriter = new StringWriter();
        var stringReader = new StringReader(readConsole);
        Console.SetOut(stringWriter);
        Console.SetIn(stringReader);
        // Act

        ProgramQuestao1.Main(new string[0]);

        // Assert
        var output = stringWriter.ToString().Replace("\r\n", "\n");

        Assert.Contains("Conta 5139, Titular: Elza Soares, Saldo: $0.00", output);
        Assert.Contains("Conta 5139, Titular: Elza Soares, Saldo: $300.00", output);
        Assert.Contains("Conta 5139, Titular: Elza Soares, Saldo: -$1.50", output);

        // Clean up
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        Console.SetIn(new StreamReader(Console.OpenStandardInput()));
    }
}
