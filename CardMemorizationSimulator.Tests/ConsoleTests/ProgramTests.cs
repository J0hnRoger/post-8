using System.Text;
using Xunit.Abstractions;

namespace CardMemorizationSimulatorTests.ConsoleTests;

public class ProgramTests
{
    private readonly ITestOutputHelper output;

    public ProgramTests(ITestOutputHelper output)
    {
        this.output = output;
        output.WriteLine("ProgramTests launched");
    }
    
    [Fact(Skip = "Not used")]
    public void Program_Launch()
    {
        System.Console.SetOut(new XUnitTextWriter(output));
        
        var entryPoint = typeof(Program).Assembly.EntryPoint!;
        entryPoint.Invoke(null, new object[] { Array.Empty<string>() });
    }
}

public class XUnitTextWriter : TextWriter
{
    public override Encoding Encoding { get; }
    
    private readonly ITestOutputHelper _testOutput;

    public XUnitTextWriter(ITestOutputHelper testOutput)
    {
        _testOutput = testOutput;
    }
    
   public override void WriteLine(string? value)
    {
        _testOutput.WriteLine(value);
    }
}