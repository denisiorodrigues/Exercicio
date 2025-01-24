using ProgramQuestao2 = Questao2.Program;

namespace Exercicio.Test.Questao2;

public class ProgramTests
{
    [Fact]
    public async void MainTest()
    {
        // Arrange
        var expectedPSG = "Team Paris Saint-Germain scored 109 goals in 2013";
        var expectedChelsea = "Team Chelsea scored 92 goals in 2014";


        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        await ProgramQuestao2.Main();

        // Assert
        var output = stringWriter.ToString().Replace("\r\n", "\n");
        var outputLines = output.Split(Environment.NewLine).Where(x => !string.IsNullOrEmpty(x)).ToList();

        Assert.Contains(expectedPSG, output);
        Assert.Contains(expectedChelsea, output);

        // Clean up
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }
}
