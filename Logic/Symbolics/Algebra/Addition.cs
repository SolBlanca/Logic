using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics.Algebra
{
    public class Addition : Operation
    {
        public Addition() : base("Add", "+")
        {

        }

        public override Symbol Process(Group group, Context context)
        {
            Evaluate(group, context);

            for (int i = 0; i < group.Elements.Count; i++)
            {
                var element = group[i];

                if (group[i] is Primitive<double>)
                {
                    double sum = ((Primitive<double>)group[i]).Value;

                    for (int j = group.Elements.Count - 1; j > i; j--)
                    {
                        if (group[j] is Primitive<double>)
                        {
                            sum += ((Primitive<double>)group[j]).Value;
                            group.Elements.RemoveAt(j);
                        }
                    }

                    if (sum == 0)
                    {
                        group.Elements.RemoveAt(i--);
                    }
                    else
                    {
                        group[i] = new Primitive<double>(sum);
                    }
                }
            }

            if (group.Elements.Count == 2)
            {
                return group[1];
            }

            return group;
        }
    }
}
