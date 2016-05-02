using System;
using System.Collections.Generic;

namespace Logic.Symbolics2
{
	public class Context
	{
		public List<Scope> Scopes { get; private set; }

		public bool Numeric { get; set; }

		public Context()
		{
			Scopes = new List<Scope>();
		}

		public Symbol ResolveVariable(Symbol atom)
		{
			Symbol symbol = null;

			for (int i = Scopes.Count - 1; i >= 0; i--)
			{
				symbol = Scopes[i].ResolveVariable(((Atom)atom).Name);

				if (symbol != null)
				{
					return symbol;
				}
			}

			return atom;
		}

		public Function ResolveFunction(Symbol symbol)
		{
			if (symbol as Atom == null)
				return null;
			
			Function function = null;

			for (int i = Scopes.Count - 1; i >= 0; i--)
			{
				function = Scopes[i].ResolveFunction(((Atom)symbol).Name);

				if (function != null)
				{
					return function;
				}
			}

			return function;
		}

		public void Set(Atom atom, Symbol value)
		{
			if (Scopes.Count == 0)
			{
				Scopes.Add(new Scope());
			}

			var scope = Scopes[Scopes.Count - 1];
			scope.Variables.Add(atom.Name, value);
		}

		public void Define(Atom atom, Function value)
		{
			if (Scopes.Count == 0)
			{
				Scopes.Add(new Scope());
			}

			// should be first writable scope

			var scope = Scopes[Scopes.Count - 1];
			scope.Functions.Add(atom.Name, value);
		}


		public Symbol Evaluate(Symbol symbol)
		{
			return Core.Evaluate( symbol, this );
		}

		public Symbol Evaluate(string value)
		{
			var symbol = Symbol.Parse(value);
			return Evaluate( symbol );
		}
	}
}

