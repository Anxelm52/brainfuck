public class Program
{
    private static byte[] _memory = new byte[30000];
    private static int _memoryPointer = 0;

    private static char[] _allowedCommands = new char[] { '.', ',', '>', '<', '[', ']', '+', '-' };

    public static void Main()
    {
        var program = File.ReadAllText("c:\\Node\\test.bf");

        var cleanProgramCharArray = program.ToCharArray().Where(x => _allowedCommands.Contains(x)).ToArray();

        Execute(String.Join("", cleanProgramCharArray));

        Console.ReadKey();
    }

    private static void Execute(string program)
    {
        for (int commandIndex = 0; commandIndex < program.Length; commandIndex++)
        {
            var command = program[commandIndex];

            switch (command)
            {
                case '[':
                    {
                        var loopEndIndex = FindLoopEndIndex(program, commandIndex);
                        var loopBody = program.Substring(commandIndex + 1, loopEndIndex - commandIndex - 1);

                        while (_memory[_memoryPointer] != 0)
                        {
                            Execute(loopBody);
                        }

                        commandIndex = loopEndIndex;
                    }
                    break;
                case ']':
                    {
                        return;
                    }
                    break;
                case '>':
                    {
                        _memoryPointer++;
                    }
                    break;
                case '<':
                    {
                        _memoryPointer--;
                    }
                    break;
                case '+':
                    {
                        _memory[_memoryPointer]++;
                    }
                    break;
                case '-':
                    {
                        _memory[_memoryPointer]--;
                    }
                    break;
                case '.':
                    {
                        Console.Write(Convert.ToChar(_memory[_memoryPointer]));
                    }
                    break;
                case ',':
                    {
                        _memory[_memoryPointer] = Convert.ToByte(Console.Read());
                    }
                    break;
            }
        }
    }

    private static int FindLoopEndIndex(string program, int loopStartIndex)
    {
        var loopsCounter = 0;

        for (int lookupIndex = loopStartIndex + 1; lookupIndex < program.Length; lookupIndex++)
        {
            if (program[lookupIndex] == '[')
            {
                loopsCounter++;
            }
            else if (program[lookupIndex] == ']')
            {
                loopsCounter--;
            }
            if (loopsCounter < 0)
            {
                return lookupIndex;
            }
        }

        throw new Exception("invalid program");
    }
}