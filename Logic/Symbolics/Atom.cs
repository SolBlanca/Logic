using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics
{
    public class Atom : Symbol
    {
		public override SymbolType Type {
			get {
				return SymbolType.Atom;
			}
		}

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

		public override bool Equals (object obj)
		{
			var atom = obj as Atom;
			if (atom != null) {
				return atom.Name == Name;
			}

			return false;
		}

        public static readonly Atom Null = new Atom("Null");
        public static readonly Atom True = new Atom("True");
        public static readonly Atom False = new Atom("False");

		public static readonly Atom AtomType = new Atom("Atom");
		public static readonly Atom Group = new Atom("Group");
		public static readonly Atom Operation = new Atom("Operation");
		public static readonly Atom Primitive = new Atom("Primitive");


		public static Atom FromBoolean(bool value) {
			return (value) ? True : False;
		}
    }
}
