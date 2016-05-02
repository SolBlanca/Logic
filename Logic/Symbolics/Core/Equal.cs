using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics.Core
{
    public class Equal : Operation
    {
        public Equal() : base("Equal")
        {
            
        }

        public override Symbol Process(Group group, Context context)
        {
            Evaluate(group, context);

            return Equals(group[1], group[2]) ? Atom.True: Atom.False;
        }

        public static bool Equals(Symbol left, Symbol right)
        {
			return left.Equals( right );
        }
    }
}
