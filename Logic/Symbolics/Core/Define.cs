using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics.Core
{
    public class Define : Operation
    {
        public Define() : base("Define", "def")
        {

        }

        public override Symbol Process(Group group, Context context)
        {
            var atom = group[1] as Atom;
            var parameters = group[2] as Group;
            var function = group[3] as Symbol;

            context.Set(atom, new Definition(atom.Name, parameters, function));

            return function;
        }
    }

    public class Definition : Operation
    {
        public Group Parameters { get; private set; }
        public Symbol Function { get; private set; }

        public Definition(string name, Group parameters, Symbol function) : base(name)
        {
            Parameters = parameters;
            Function = function;
        }

        public override Symbol Process(Group group, Context context)
        {
            Scope scope = new Scope();

            for (int i = 0; i < Parameters.Count; i++)
            {
                var parameter = Parameters[i] as Atom;

                scope.Variables.Add(parameter.Name, group[i + 1]);
            }

            context.Scopes.Add(scope);
            var value = context.Process(Function.Clone());
            context.Scopes.Remove(scope);

            return value;
        }
    }
}
