using System;

namespace Logic.Symbolics.Core
{
	public class IsAtom : Operation
	{
		public IsAtom() : base("IsAtom", "Atom?")
		{
			
		}

		public override Symbol Process(Group group, Context context)
		{
			if (group.Count == 2) {
				return Atom.FromBoolean(group [1].Type == SymbolType.Atom);
			}

			return group;
		}
	}
}

