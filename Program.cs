using System;
using System.Text;


namespace Practichna3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;

            PrintGuide();
            ParsedExpression expression = ParseInput(new Parser());
            Calculator calc = new Calculator();
            decimal result = calc.EvaluateExpression(expression);
            Console.WriteLine($"Answer is {result}");
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("Press Enter to continiue");
            Console.ReadLine();
        }

        static void PrintGuide()
        {
            Console.WriteLine("Explanations:");

            Console.WriteLine("Write math expression, containing the left operand, right operand and operation, " +
                @"Spaces and '=' are ignored in the expression, also you can use '.' and ',' to split integer and fraction ");
            Console.WriteLine();

            Console.WriteLine("Available operations:");
            Console.WriteLine(@"Addition  - '+'");
            Console.WriteLine(@"Subtraction - '-'");
            Console.WriteLine(@"Multiplication - '*'");
            Console.WriteLine();
        }

        static ParsedExpression ParseInput(Parser parser)
        {
            try
            {
                if (parser.ProcessInput(Console.ReadLine()))
                    return parser.GetResult();
                else
                    return ParseInput(parser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error - {ex.Message}");
                return ParseInput(parser);
            }
        }
    }
}
