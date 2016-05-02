using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics
{
    public class Group : Symbol
	{
		public override SymbolType Type {
			get {
				return SymbolType.Group;
			}
		}

        public List<Symbol> Elements { get; private set; }

        public Group()
        {
            Elements = new List<Symbol>();
        }

        public Group(params Symbol[] symbols)
        {
            Elements = new List<Symbol>(symbols);
        }

        public Group(Symbol head, Group group)
        {
            Elements.Add(head);
            Elements.AddRange(group.Elements);
        }

        public Symbol this[int index]
        {
            get
            {
                return Elements[index];
            }
            set
            {
                Elements[index] = (value == null) ? Atom.Null : value;
            }
        }

        public int Count
        {
            get
            {
                return Elements.Count;
            }
        }

        public override string ToString()
        {
            StringBuilder value = new StringBuilder();

            value.Append("( ");

            foreach (var symbol in Elements)
            {
                value.Append(symbol.ToString());
                value.Append(' ');
            }

            value.Append(')');

            return value.ToString();
        }

        public override Symbol Clone()
        {
            Group clone = new Group();

            foreach (var symbol in Elements)
            {
                clone.Elements.Add(symbol.Clone());
            }

            return clone;
        }


        public Group After(int index)
        {
            var group = new Group();

            for (int i = index; i < Count; i++)
            {
                group.Elements.Add(this[i]);
            }

            return group;
        }

        public Group After(Symbol head, int index)
        {
            var group = new Group();
            group.Elements.Add(head);

            for (int i = index; i < Count; i++)
            {
                group.Elements.Add(this[i]);
            }

            return group;
        }

		public override bool Equals (object obj)
		{
			var group = obj as Group;
			if (group != null && group.Count == Count) {

				for (int i = 0; i < Count; i++) {
					if (!this [i].Equals( group [i] )) {
						return false;
					}
				}

				return true;
			}

			return false;
		}

		public override bool Matches(Symbol value)
		{
			var group = value as Group;
			if (group != null && group.Count == Count) {

				// if it's a match operator then go through that operator instead of group match

				for (int i = 0; i < Count; i++) {
					if (!this [i].Matches( group [i] )) {
						return false;
					}
				}

				return true;
			}

			return false;
		}
    }
}
