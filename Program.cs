// See https://aka.ms/new-console-template for more information
using HackAssembler;




if (args.Length != 1)
{
    Console.WriteLine(
        "Usage: Assembler <file.asm>");
    return;
}


string inputPath = args[0];


/*
 * Comment the above code and uncomment this code, and past in the file path to access the file this way.
string inputPath = "xxxx";
*/

string[] lines = File.ReadAllLines(inputPath);

List<string> outputLines = new();
List<Command> commands = new();

Parser parser = new();
Assembler assembler = new();


foreach (string line in lines)
{
    Command? commmand = parser.ParseOneLine(line);

    if (commmand == null)
    {
        continue;
    }

    commands.Add(commmand);

}

int ROMAddress = 0;

SymbolTable symbolTable = new SymbolTable();
foreach (Command command in commands)
{
    if (command.Type == CommandType.L)
    {
        symbolTable.AddEntry(command.Symbol, ROMAddress);

    }
    else
    {
        ROMAddress++;
    }


}
Console.WriteLine("Reached end of pass 1");

int nextVariableAddress = 16;

foreach (Command command in commands)
{
    /*
    if (command.Type == CommandType.A && !int.TryParse(command.Symbol, out int address) && !symbolTable.Contains(command.Symbol))
    {

        symbolTable.AddEntry(command.Symbol, nextVariableAddress);
        nextVariableAddress++;

    }

    string? output = assembler.AssembleOneCommand(command);
    if (output != null)
    {
        outputLines.Add(output);
    }*/

    if (command.Type == CommandType.L)
    {
        // Skip L commands in the second pass
        continue;
    }

    if (command.Type == CommandType.A)
    {

        if (!int.TryParse(command.Symbol, out int address))
        {
            if (!symbolTable.Contains(command.Symbol))
            {
                symbolTable.AddEntry(command.Symbol, nextVariableAddress);
                nextVariableAddress++;
            }
            address = symbolTable.GetAddress(command.Symbol);
        }
        command.Symbol = address.ToString();
    }


    outputLines.Add(assembler.AssembleOneCommand(command));

}

/*Console.WriteLine("Symbol Table:");
foreach (var entry in symbolTable.table)
{
    Console.WriteLine($"  {entry.Key}: {entry.Value}");
}*/


string outputPath = Path.ChangeExtension(inputPath, ".hack");

    File.WriteAllLines(outputPath, outputLines);




/*Parser parser = new();

string[] testCases =
{
    "@123",
    "@counter",
    "(LOOP)",
    "D=A",
    "M=D",
    "0;JMP",
    "D;JGT",
    "D=D+1;JGT",
    "AMD=D|M;JLE",
    "",
    "   ",
    "MD=D+1",
    "AD=D-A",
    "0;JMP",
    "AMD=D&M"
};

foreach (string test in testCases)
{
    Console.WriteLine($"INPUT: {test}");

    Command? cmd = parser.ParseOneLine(test);

    if (cmd == null)
    {
        Console.WriteLine("NULL COMMAND");
        Console.WriteLine();
        continue;
    }

    Console.WriteLine($"Type   : {cmd.Type}");
    Console.WriteLine($"Symbol : {cmd.Symbol}");
    Console.WriteLine($"Dest   : {cmd.Dest}");
    Console.WriteLine($"Comp   : {cmd.Comp}");
    Console.WriteLine($"Jump   : {cmd.Jump}");
    Console.WriteLine(new string('-', 30));
}

Assembler assembler = new();
Parser parser = new();

Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("@0")));

Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("@1")));


Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("@2")));


Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("@21")));



Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("@32767")));


Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("D=A")));


Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("M=D")));


Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("D=M")));



Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("0;JMP")));



Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("D;JGT")));


Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("D=D+1")));


Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("AMD=D|M")));


Console.WriteLine(
    assembler.AssembleOneCommand(
        parser.ParseOneLine("AMD=D|M;JLE")));

*/







