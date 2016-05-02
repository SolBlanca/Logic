using System;
using System.Collections.Generic;

namespace Logic.Symbolics2
{
	public class Scope
	{
		public Dictionary<string, Symbol> Variables { get; private set; }
		public Dictionary<string, Function> Functions { get; private set; }

		public Scope()
		{
			Variables = new Dictionary<string, Symbol>();
			Functions = new Dictionary<string, Function>();

		}

		public Symbol ResolveVariable(string name)
		{
			Symbol symbol = null;

			Variables.TryGetValue(name, out symbol);

			return symbol;
		}

		public Function ResolveFunction(string name)
		{
			Function function = null;

			Functions.TryGetValue(name, out function);

			return function;
		}
	}
}

