using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics.Calculus
{
    public class Differentiate : Operation
    {
        public Differentiate() : base("Differentiate")
        {

        }

        public override Symbol Process(Group group, Context context)
        {
            var function = group[1];
            var respect = group[2] as Atom;

            var agroup = function as Group;
            if (agroup != null)
            {
                agroup[0] = context.Process(agroup[0]);
                var operation = agroup[0] as Operation;

                if (operation is Algebra.Addition)
                {
                    for (int i = 1; i < agroup.Count; i++)
                    {
                        agroup[i] = Process(Wrap(agroup[i], respect), context);
                    }

                    return context.Process(agroup);
                }


                if (operation is Algebra.Multiplication)
                {
                    var first = agroup[1];
                    Symbol rest;
                    
                    if (agroup.Count == 3)
                    {
                        rest = agroup[2];
                    }
                    else
                    {
                        rest = agroup.After(operation, 2);
                    }

                    // ( D ( * f ... ) x ) -> ( + ( * f ( D ... x ) ) ( * ( D f x ) ... ) )


                    var left = new Group(new Atom("*"), new Group(this, first, respect), rest);
                    var right = new Group(new Atom("*"), first, new Group(this, rest, respect));

                    var plus = new Group(new Atom("+"), left, right);

                    return context.Process(plus);

                }
            }


            var atom = function as Atom;
            if (atom != null)
            {
                if (atom.Name == respect.Name)
                {
                    return new Primitive<double>(1);
                }

                return atom;
            }

            var number = function as Primitive<double>;
            if (number != null)
            {
                return new Primitive<double>(0);
            }

            return group;
        }

        public Group Wrap(Symbol value, Atom respect)
        {
            var group = new Group();
            group.Elements.Add(this);
            group.Elements.Add(value);
            group.Elements.Add(respect);

            return group;
        }
    }
}
