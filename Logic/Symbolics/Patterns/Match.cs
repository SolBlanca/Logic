using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics.Patterns
{
    public class Match : Operation
    {
        public Match() : base("Match")
        {
            // ( Match a (1 2) )

            // ( Replace a ( (1 b) (2 c) )

            
        }

        public override Symbol Process(Group group, Context context)
        {
			Evaluate(group, context);

			if (group.Count == 3) {
				return Atom.FromBoolean(group [2].Matches( group [1] ));
			}

            return group;
        }
    }

    public class Blank : Operation
    {
        public Blank() : base("Blank")
        {

        }

		public override bool Matches(Symbol symbol)
		{
			return true;
		}
    }
    
    public class BlankSequence : Operation
    {
        public BlankSequence() : base("BlankSequence")
        {

        }
    }
    public class Alternatives : Operation
    {
        public Alternatives() : base("Alternatives")
        {
			
		}

		public override bool Matches(Symbol symbol)
		{
			return true;
		}
    }
}
