using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics.Trigonometry
{
    public class Sine : Operation
    {
        public Sine() : base("Sine", "sin")
        {

        }

        public override Symbol Process(Group group, Context context)
        {
            Evaluate(group, context);

            if (context.Numeric)
            {
                if (group[1] is Primitive<double>)
                {
                    return new Primitive<double>(Math.Sin(((Primitive<double>)group[1]).Value));
                }
            }

            return group;
        }
    }
}
