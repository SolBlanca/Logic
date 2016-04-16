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
            var atomL = left as Atom;
            if (atomL != null)
            {
                var atomR = right as Atom;
                if (atomR != null)
                {
                    return atomL.Name == atomR.Name;
                }

                return false;
            }

            var numberL = left as Primitive<double>;
            if (numberL != null)
            {
                var numberR = right as Primitive<double>;
                if (numberR != null)
                {
                    return numberL.Value == numberR.Value;
                }

                return false;
            }

            var operationL = left as Operation;
            if (operationL != null)
            {
                var operationR = right as Operation;
                if (operationR != null)
                {
                    return operationL == operationR;
                }

                return false;
            }

            var groupL = left as Group;
            if (groupL != null)
            {
                var groupR = right as Group;
                if (groupR != null)
                {
                    if (groupL.Count != groupR.Count)
                    {
                        return false;
                    }

                    for (int i = 0; i < groupL.Count; i++)
                    {
                        if (!Equals(groupL[i], groupR[i]))
                        {
                            return false;
                        }
                    }

                    return true;
                }

                return false;
            }

            throw new NotImplementedException();
        }
    }
}
