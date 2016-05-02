using System;

namespace Logic.Symbolics2
{
	public static class Arithmatic
	{
		public static Scope Scope {
			get;
			private set;
		}

		static Arithmatic()
		{
			Scope = new Scope();
			Scope.Functions.Add( "Add", Function.FromFunction<Number, Number>( Add ) );
			Scope.Functions.Add( "-", Function.FromFunction<Number, Number>( Subtract ) );
			Scope.Functions.Add( "+", Scope.Functions["Add"] );

			Scope.Functions.Add( "And", new Function( And ) );
			Scope.Functions.Add( "Or", new Function( Or ) );

		}

		public static Symbol Add(Number a, Number b, Context c)
		{
			return new Number( a.Value + b.Value );
		}

		public static Symbol Subtract(Number a, Number b, Context c)
		{
			return new Number( a.Value - b.Value );
		}

		public static Boolean IsTrue(Symbol symbol)
		{
			if (Core.Equals( symbol, Boolean.False ) || Core.Equals( symbol, List.Nothing )) {
				return Boolean.False;
			}

			return Boolean.True;
		}

		public static Symbol And(List list, Context c)
		{
			for (int i = 1; i < list.Count; i++) {
				if (!IsTrue( Core.Evaluate(list [i], c) )) {
					return (Boolean)false;
				}
			}

			return (Boolean)true;
		}

		public static Symbol Or(List list, Context c)
		{
			for (int i = 1; i < list.Count; i++) {
				if (IsTrue( Core.Evaluate(list [i], c) )) {
					return (Boolean)true;
				}
			}

			return (Boolean)false;
		}
	}
}

