using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics
{
    public abstract class Symbol
    {
		public abstract SymbolType Type { get; }

		public virtual bool Matches (Symbol value)
		{
			return Equals( value );
		}

        public abstract Symbol Clone();

        public static Symbol Parse(string value)
        {
            Stack<Group> groupings = new Stack<Group>();
            var root = new Group();
            groupings.Push(root);

            for (int i = 0; i < value.Length; )
            {
                switch (value[i])
                {
                    case '(':
                        var group = new Group();
                        groupings.Peek().Elements.Add(group);
                        groupings.Push(group);

                        i++;
                        break;
                    case ')':
                        groupings.Pop();

                        i++;
                        break;
                    default:
                        if (value[i] == ',' || Char.IsWhiteSpace(value[i]))
                        {
                            i++;
                        }
                        else if (value[i] == '"')
                        {
                            var end = value.IndexOfAny(new char[] { '"' }, i);
                            var text = value.Substring(i, end - i);

                            groupings.Peek().Elements.Add(new Primitive<string>(text));

                            i = end;
                        }
                        else if (Char.IsNumber(value[i]))
                        {
                            var end = value.IndexOfAny(new char[] { ' ', ',', ')' }, i);

                            if (end == -1)
                            {
                                end = value.Length;
                            }

                            var number = value.Substring(i, end - i);

                            groupings.Peek().Elements.Add(new Primitive<double>(double.Parse(number)));

                            i = end;
                        }
                        else
                        {
                            var end = value.IndexOfAny(new char[] { ' ', ',', ')' }, i);

                            if (end == -1)
                            {
                                end = value.Length;
                            }

                            var text = value.Substring(i, end - i);

                            groupings.Peek().Elements.Add(new Atom(text));

                            i = end;
                        }
                        break;
                }
            }

            if (root.Elements.Count > 1)
            {
                return root;
            }
            else if (root.Elements.Count == 1)
            {
                return root[0];
            }
            return Atom.Null;
        }
    }
}
