using System;

namespace Logic.Symbolics.Core
{
	public class TypeOf : Operation
	{
		public TypeOf() : base("TypeOf")
		{

		}

		public override Symbol Process(Group group, Context context)
		{
			Evaluate(group, context);

			if (group.Count == 2) {
				switch (group [1].Type) {
				case SymbolType.Atom:
					return Atom.AtomType;
				case SymbolType.Group:
					return Atom.Group;
				case SymbolType.Operation:
					return Atom.Operation;
				}
			}

			return group;
		}
	}
}

