using System;

namespace Logic.Symbolics2
{
	public static class Patterns
	{
		public static Scope Scope {
			get;
			private set;
		}

		static Patterns()
		{
			Scope = new Scope();

			// Type
			Scope.Functions.Add( "IsAtom", Function.FromFunction<Symbol>( IsAtom ) );
			Scope.Functions.Add( "IsList", Function.FromFunction<Symbol>( IsList ) );
			Scope.Functions.Add( "IsNumber", Function.FromFunction<Symbol>( IsNumber ) );

			// Conditionals
			Scope.Functions.Add( "If", Function.FromFunction<Symbol, Symbol, Symbol>( If ) );
			Scope.Functions.Add( "Condition", new Function( Condition ) );
			Scope.Functions.Add( "Match", Function.FromFunction<Symbol, Symbol>( Match ) );

		}

		public static Symbol IsNumber(Symbol symbol, Context context)
		{
			return (Boolean)(symbol.Type == SymbolType.Number);
		}

		public static Symbol IsAtom(Symbol symbol, Context context)
		{
			return (Boolean)(symbol.Type == SymbolType.Atom);
		}

		public static Symbol IsList(Symbol symbol, Context context)
		{
			return (Boolean)(symbol.Type == SymbolType.List);
		}

		public static Symbol If(Symbol symbol, Symbol t, Symbol f, Context context)
		{
			if (Core.Equals(symbol, Boolean.True))
				return t;
			return f;
		}

		public static Symbol Condition(List list, Context context)
		{
			for (int i = 0; i < list.Count; i++) {
				var pair = list [i] as List;
				if (pair != null && pair.Count == 2) {
					var condition = Core.Evaluate( pair [0], context );
					if (Core.Equals(condition, Boolean.True)) {
						return Core.Evaluate( pair [1], context );
					}
				}
			}

			return List.Nothing;
		}

		public static Boolean Match(Symbol value, Symbol pattern, Context context)
		{
			if (Core.Equals( value, pattern )) {
				return true;
			}

			if (Core.Equals( pattern, Blank )) {
				return true;
			}

			if (pattern.Type == SymbolType.List) {
				var list = pattern as List;

				if (list.Count > 0) {
					if (Core.Equals( list [0], Alternatives )) {
						for (int i = 1; i < list.Count; i++) {
							if (Match( value, list [i], context )) {
								return true;
							}
						}
					}
				}
			}

			return false;
		}

		private static Symbol MatchInternal(Symbol value, Symbol pattern, Context context)
		{
			return Match( value, pattern, context );
		}

		public static readonly Atom Blank = new Atom("_");
		public static readonly Atom BlankSequence = new Atom("__");
		public static readonly Atom Alternatives = new Atom("|");
	}
}

