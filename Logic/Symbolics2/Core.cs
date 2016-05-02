using System;

namespace Logic.Symbolics2
{
	public static class Core
	{
		public static Scope Scope {
			get;
			private set;
		}

		static Core()
		{
			Scope = new Scope();

			// Execution
			Scope.Functions.Add( "Evaluate", Function.FromFunction<Symbol>( Evaluate ) );
			Scope.Functions.Add( "Apply", Function.FromFunction<Symbol, List>( Apply ) );
			Scope.Functions.Add( "Hold", new Function( Hold ) );

			// Scope
			Scope.Functions.Add( "Set", Function.FromFunction<Atom, Symbol>( Set ) );
			Scope.Functions.Add( "=", Scope.Functions ["Set"] );
			Scope.Functions.Add( "Define", Function.FromFunction<Atom, List, Symbol>( Define ) );
			Scope.Functions.Add( ":=", Scope.Functions ["Define"] );
			Scope.Functions.Add( "Equal", Function.FromFunction<Symbol, Symbol>( Equals ) );
			Scope.Functions.Add( "==", Scope.Functions ["Equal"] );

			// List
			Scope.Functions.Add( "First", Function.FromFunction<List>( First ) );
			Scope.Functions.Add( "Rest", Function.FromFunction<List>( Rest ) );
			Scope.Functions.Add( "Prepend", Function.FromFunction<Symbol, List>( Prepend ) );
			Scope.Functions.Add( "Append", Function.FromFunction<List, Symbol>( Append ) );
			Scope.Functions.Add( "Map", Function.FromFunction<Symbol, List>( Map ) );
			Scope.Functions.Add( "Length", Function.FromFunction<List>( Length ) );

		}

		public static Symbol Evaluate(Symbol symbol, Context context)
		{
			switch (symbol.Type) {

			case SymbolType.List:
				var list = symbol as List;

				var head = First( list, context );
				var function = context.ResolveFunction( head );

				if (function == null) {
					var eval = new List();

					for (int i = 0; i < list.Count; i++) {
						eval.Children.Add( Evaluate( list [i], context ) );
					}

					return eval;
				}

				return function.Apply( list, context );
			
			case SymbolType.Atom:
				var atom = symbol as Atom;

				var variable = context.ResolveVariable( atom );

				return variable;

			default:
				return symbol;
			}
		}

		public static Symbol Apply(Symbol head, List parameters, Context context)
		{
			var function = context.ResolveFunction( head );

			if (function != null) {
				return function.Apply( Prepend(head, parameters) as List, context );
			}

			return List.Nothing;
		}

		public static Symbol Hold(List list, Context context)
		{
			if (list.Count > 1) {
				return list [1];
			}

			return List.Nothing;
		}

		public static Symbol Set(Atom name, Symbol value, Context context)
		{
			context.Set( name, value );

			return value;
		}

		public static Symbol Define(Atom name, List parameters, Symbol replacement, Context context )
		{
			context.Define( name as Atom, Function.FromDefine( parameters as List, replacement ) );

			return name;
		}

		public static void Enter(Context context)
		{
			context.Scopes.Add( new Scope() );
		}

		public static void Exit(Context context)
		{
			context.Scopes.RemoveAt( context.Scopes.Count - 1 );
		}

		public static Symbol First(List symbol, Context context = null)
		{
			if (symbol.Count > 0) {
				return symbol [0];
			}

			return List.Nothing;
		}

		public static Symbol Second(List symbol, Context context = null)
		{
			if (symbol.Count > 1) {
				return symbol [1];
			}

			return List.Nothing;
		}

		public static List Rest(List symbol, Context context = null)
		{
			if (symbol.Count > 1) {
				var list = new List();

				for (int i = 1; i < symbol.Count; i++) {
					list.Children.Add( symbol [i] );
				}

				return list;
			}

			return List.Nothing;
		}

		public static Symbol Prepend(Symbol symbol, List list, Context context = null)
		{
			List value = new List();

			value.Children.Add(symbol);

			for (int i = 0; i < list.Count; i++) {
				value.Children.Add( list [i] );
			}

			return value;
		}

		public static Symbol Append(List list, Symbol symbol, Context context = null)
		{
			List value = new List();

			for (int i = 0; i < list.Count; i++) {
				value.Children.Add( list [i] );
			}

			value.Children.Add(symbol);

			return value;
		}

		public static Symbol Map(Symbol symbol, List list, Context context)
		{
			List mapped = new List();

			for (int i = 0; i < list.Count; i++) {

				if (list [i].Type == SymbolType.List) {
					mapped.Children.Add( Prepend( symbol, list[i] as List ) );
				} else {
					var pair = new List();
					pair.Children.Add( symbol );
					pair.Children.Add( list [i] );
					mapped.Children.Add( pair );
				}
			}

			return Core.Evaluate(mapped, context);
		}

		public static Symbol Length(List symbol, Context context = null)
		{
			return new Number( symbol.Count );
		}

		public static Boolean Equals(Symbol left, Symbol right)
		{
			if (left.Type == right.Type) {
				switch (left.Type) {

				case SymbolType.List:
					var l = left as List;
					var r = right as List;

					if (l.Count == r.Count) {
						for (int i = 0; i < l.Count; i++) {
							if (!Equals( l [i], r [i] )) {
								return false;
							}
						}

						return true;
					}

					return false;

				case SymbolType.Atom:
					return ((Atom)left).Name == ((Atom)right).Name;

				case SymbolType.Number:
					return ((Number)left).Value == ((Number)right).Value;

				}
			}

			return false;
		}

		public static Symbol Equals(Symbol left, Symbol right, Context context)
		{
			return Equals( left, right );
		}
	}
}

