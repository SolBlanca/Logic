using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics
{
    public class Atom : Symbol
    {
        public string Name { get; private set; }

        public Atom(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public override Symbol Clone()
        {
            return this;
        }

        public static readonly Atom Null = new Atom("Null");
        public static readonly Atom True = new Atom("True");
        public static readonly Atom False = new Atom("False");
    }
}
