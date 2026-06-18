
namespace HackAssembler
{
    internal class Parser
    {
        public Command? ParseOneLine(String line)
        {


            line = line.Trim();

            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }
            if (line.StartsWith("//"))
                return null;


            if (line.StartsWith("@"))
            {
                return new Command(CommandType.A, symbol: line.Substring(1));
            }
            else if (line.StartsWith("("))
            {
                return new Command(CommandType.L, symbol: line.Substring(1, line.Length - 2));
            }
            else
            {
                // C command
                return ParseCCommand(line);
            }

        }

        private Command? ParseCCommand(String CCommand)
        {
            String temp = CCommand;

            Command command = new (CommandType.C);

            if (CCommand.Contains('='))
            {
                int equalsSignIndex = CCommand.IndexOf('=');
                command.Dest = temp.Substring(0, equalsSignIndex);

                temp = CCommand.Substring(equalsSignIndex + 1);
            }

            if (temp.Contains(';'))
            {
                int semicolonIndex = temp.IndexOf(';');
                // command.Comp = temp.Substring(0, semicolonIndex);
                command.Jump = temp.Substring(semicolonIndex + 1);
                temp = temp.Substring(0, semicolonIndex);
            }

            command.Comp = temp;

            return command;

        }



       

    }



    internal class Command
    {
        public CommandType Type;

        public String? Symbol;

        // Used by C commands
        public string? Dest;
        public string? Comp;
        public string? Jump;


        public Command(CommandType type, string? symbol = null, string? dest = null, string? comp = null, string? jump = null)
        {
            Type = type;
            Symbol = symbol;
            Dest = dest;
            Comp = comp;
            Jump = jump;
        }
    }

    internal enum CommandType
    {
        A,
        C,
        L
    }
}
