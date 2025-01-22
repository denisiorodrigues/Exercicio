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
        string textoFinal = @"Entre o número da conta: Entre o titular da conta: Haverá depósito inicial (s/n)? Entre o valor de depósito inicial: 
                            Dados da conta:
                            Conta 5447, Titular: Milton Gonçalves, Saldo: $ 350.00

                            Entre um valor para depósito: Dados da conta atualizados:
                            Conta 5447, Titular: Milton Gonçalves, Saldo: $ 550.00

                            Entre um valor para saque: Dados da conta atualizados:
                            Conta 5447, Titular: Milton Gonçalves, Saldo: $ 347.50
                            ";
        
        var stringWriter = new StringWriter();
        var stringReader = new StringReader(readConsole);
        Console.SetOut(stringWriter);
        Console.SetIn(stringReader);
        // Act

        ProgramQuestao1.Main(new string[0]);

        // Assert
        var output = stringWriter.ToString().Replace("\r\n", "\n");
        textoFinal = textoFinal.Replace("\r\n", "\n");
        //Assert.AreEqual(expectedOutput, output); //Compara as strings
        Assert.True(output.Contains("Conta 5447, Titular: Milton Gonçalves, Saldo: $350.00"));
        Assert.True(output.Contains("Conta 5447, Titular: Milton Gonçalves, Saldo: $550.00"));
        Assert.True(output.Contains("Conta 5447, Titular: Milton Gonçalves, Saldo: $347.50"));

        // Clean up
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        Console.SetIn(new StreamReader(Console.OpenStandardInput()));
    }

    [Fact]
    public void FluxoCadastroContaSemSaldoInicial()
    {
        // Arrange
        string readConsole = "5139\r\nElza Soares\r\nn\r\n300.00\r\n298.00\r\n";
        string textoFinal = @"Entre o número da conta: Entre o titular da conta: Haverá depósito inicial (s/n)? Entre o valor de depósito inicial: 
                            Dados da conta:
                            Conta 5139, Titular: Elza Soares, Saldo: $ 0.00

                            Entre um valor para depósito: Dados da conta atualizados:
                            Conta 5139, Titular: Elza Soares, Saldo: $ 300.00

                            Entre um valor para saque: Dados da conta atualizados:
                            Conta 5139, Titular: Elza Soares, Saldo: $ -1.50
                            ";

        var stringWriter = new StringWriter();
        var stringReader = new StringReader(readConsole);
        Console.SetOut(stringWriter);
        Console.SetIn(stringReader);
        // Act

        ProgramQuestao1.Main(new string[0]);

        // Assert
        var output = stringWriter.ToString().Replace("\r\n", "\n");
        textoFinal = textoFinal.Replace("\r\n", "\n");
        //Assert.AreEqual(expectedOutput, output); //Compara as strings
        Assert.True(output.Contains("Conta 5139, Titular: Elza Soares, Saldo: $0.00"));
        Assert.True(output.Contains("Conta 5139, Titular: Elza Soares, Saldo: $300.00"));
        Assert.True(output.Contains("Conta 5139, Titular: Elza Soares, Saldo: -$1.50"));

        // Clean up
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        Console.SetIn(new StreamReader(Console.OpenStandardInput()));
    }
}
