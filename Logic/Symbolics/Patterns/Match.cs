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
            var expression = group[1];

            return group;
        }

        public bool Matches(Symbol expression, Symbol pattern)
        {
            var patternGroup = pattern as Group;
            if (patternGroup != null)
            {
                var patternOperation = patternGroup[0] as Operation;

                // alternatives
                if (patternOperation is Alternatives)
                {
                    return true;
                }


                // just a group
                var group = expression as Group;
                for (int i = 0, j = 0; i < group.Count && j < patternGroup.Count; i++, j++)
                {

                }
            }

            if (pattern is Blank)
            {
                return true;
            }

            return false;
        }
    }

    public class Blank : Operation
    {
        public Blank() : base("Blank")
        {

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
    }
}
