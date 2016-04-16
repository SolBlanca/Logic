using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics
{
    public class Scope
    {
        public Dictionary<string, Symbol> Variables { get; private set; }

        public Scope()
        {
            Variables = new Dictionary<string, Symbol>();
        }

        public Symbol Resolve(string name)
        {
            Symbol symbol = null;

            Variables.TryGetValue(name, out symbol);

            return symbol;
        }
    }

    public class Global : Scope
    {
        public Global()
        {
            Variables.Add("+", new Algebra.Addition());
            Variables.Add("*", new Algebra.Multiplication());

            Variables.Add("==", new Core.Equal());
            Variables.Add("Set", new Core.Set());
            Variables.Add("N", new Core.Numeric());
            Variables.Add("Define", new Core.Define());

            Variables.Add("Sin", new Trigonometry.Sine());

            Variables.Add("D", new Calculus.Differentiate());

            Variables.Add("_", new Atom("Blank"));
            Variables.Add("__", new Atom("BlankSequence"));
        }
    }
}
