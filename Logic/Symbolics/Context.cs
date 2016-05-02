using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics
{
    public class Context
    {
        public List<Scope> Scopes { get; private set; }

        public bool Numeric { get; set; }

        public Context()
        {
            Scopes = new List<Scope>();
        }

        public Symbol Resolve(Atom atom)
        {
            Symbol symbol = null;

            for (int i = Scopes.Count - 1; i >= 0; i--)
            {
                symbol = Scopes[i].Resolve(atom.Name);

                if (symbol != null)
                {
                    return symbol;
                }
            }

            return atom;
        }

        public void Set(Atom atom, Symbol value)
        {
            if (Scopes.Count == 0)
            {
                Scopes.Add(new Scope());
            }

            var scope = Scopes[Scopes.Count - 1];
            scope.Variables.Add(atom.Name, value);
        }
        
        public Symbol Process(Symbol symbol)
        {
            var group = symbol as Group;
            if (group != null)
            {
                if (group.Count > 0)
                {
                    // process the head
                    group[0] = Process(group[0]);

                    var head = group[0] as Operation;
                    if (head != null)
                    {
                        return head.Process(group, this);
                    }

                    // list without operation
                    Operation.Evaluate(group, this);
                    return group;
                }
            }

            var atom = symbol as Atom;
            if (atom != null)
            {
                // resolve variables
                var value = Resolve(atom);

                if (value != null)
                {
                    return value;
                }
            }

            return symbol;
        }
    }
}
