using System.Text;



namespace HackAssembler
{
    internal class Assembler
    {

        public String AssembleOneCommand(Command command)
        {
            if (command.Type == CommandType.A)
            {

                int value = int.Parse(command.Symbol);

                string binary = Convert.ToString(value, 2);

                string result = binary.PadLeft(16, '0');

                return result;
            }
            else if (command.Type == CommandType.C)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("111");
                stringBuilder.Append(Code.Comp(command.Comp));
                stringBuilder.Append(Code.Dest(command.Dest));
                stringBuilder.Append(Code.Jump(command.Jump));



                return stringBuilder.ToString();
            }
            else if (command.Type == CommandType.L)
            {
                return null;

            }
            else
            {
                throw new ArgumentException("Invalid command type");
            }
        }
    }
}
