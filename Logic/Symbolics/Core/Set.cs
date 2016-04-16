using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics.Core
{
    public class Set : Operation
    {
        public Set() : base("set", "=")
        {

        }

        public override Symbol Process(Group group, Context context)
        {
            var atom = group[1] as Atom;
            var value = group[2];

            context.Set(atom, context.Process(value));

            return value;
        }
    }
}
