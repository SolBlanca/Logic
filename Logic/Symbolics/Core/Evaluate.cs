using System;

namespace Logic.Symbolics.Core
{
	public class Evaluate : Operation
	{
		public Evaluate() : base("Evaluate", "Eval")
		{

		}

		public override Symbol Process(Group group, Context context)
		{
			if (group.Count == 2) {
				return Atom.FromBoolean(group [1].Type == SymbolType.Atom);
			}

			return group;
		}

		public static Symbol Eval(Symbol symbol, Context context)
		{
			switch (symbol.Type)
			{
			case SymbolType.Group:
				var group = symbol as Group;

				for (int i = 0; i < group.Count; i++) {
					group [i] = Eval( group [i], context );
				}

				return null;

			case SymbolType.Atom:
				return context.Resolve(symbol as Atom);
			default:
				return symbol;
			}
		}

		public static Symbol Apply(Symbol symbol, Context context)
		{
			return null;
		}
	}
}

