using SimpleInterpreter.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write(">>> ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                Lexer lexer = new Lexer(input);
                Parser parser = new Parser(lexer.Tokenize());
                int result = parser.Parse();
                Console.WriteLine(result);
            }
        }
    }
}
