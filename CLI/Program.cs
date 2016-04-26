using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Logic;
using Logic.Symbolics;

namespace CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();
            Global global = new Global();

            context.Scopes.Add(global);

            Console.Write("> ");
            var line = Console.ReadLine();

			while (!string.IsNullOrEmpty(line))
            {
                Symbol parse = Symbol.Parse(line);
                Symbol evaluate = context.Process(parse);

                Console.WriteLine(evaluate.ToString());
                Console.WriteLine();

                Console.Write("> ");
                line = Console.ReadLine();
            }
        }
    }
}
