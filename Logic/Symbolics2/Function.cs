using System;

namespace Logic.Symbolics2
{
	public class Function
	{
		public delegate Symbol Application(List list, Context context);

		public delegate Symbol Functor<T>(T t, Context context)
			where T:Symbol;

		public delegate Symbol Functor<T, U>(T t, U u, Context context)
			where T:Symbol
			where U:Symbol;

		public delegate Symbol Functor<T, U, V>(T t, U u, V v, Context context)
			where T:Symbol
			where U:Symbol
			where V:Symbol;

		public Application Apply {
			get;
			private set;
		}

		public Function(Application application)
		{
			Apply = application;
		}

		public static Function FromDefine(List variables, Symbol formula)
		{
			return new Function( (l, c) => {

				Scope scope = new Scope();

				for (int i = 0; i < variables.Count; i++) {

					var name = variables[i] as Atom;
					var value = (l.Count > i) ? Core.Evaluate(l[i + 1], c) : List.Nothing;

					scope.Variables.Add(name.Name, value);
				}

				c.Scopes.Add(scope);
				var evaluated = Core.Evaluate(formula, c);
				c.Scopes.Remove(scope);

				return evaluated;
			} );
		}

		public static Function FromFunction<T>(Functor<T> functor)
			where T:Symbol
		{
			return new Function( (l, c) => {
				var list = l as List;

				if (list != null && list.Count > 1) {

					var t = Core.Evaluate(list[1], c) as T;
					if (t != null) {
						return functor(t, c);
					}
				}

				return l;
			} );
		}

		public static Function FromFunction<T, U>(Functor<T, U> functor)
			where T:Symbol
			where U:Symbol
		{
			return new Function( (l, c) => {
				var list = l as List;

				if (list != null && list.Count > 2) {

					var t = Core.Evaluate(list[1], c) as T;
					var u = Core.Evaluate(list[2], c) as U;
					if (t != null && u != null) {
						return functor(t, u, c);
					}
				}

				return l;
			} );
		}

		public static Function FromFunction<T, U, V>(Functor<T, U, V> functor)
			where T:Symbol
			where U:Symbol
			where V:Symbol
		{
			return new Function( (l, c) => {
				var list = l as List;

				if (list != null && list.Count > 2) {

					var t = Core.Evaluate(list[1], c) as T;
					var u = Core.Evaluate(list[2], c) as U;
					var v = Core.Evaluate(list[3], c) as V;
					if (t != null && u != null && v != null) {
						return functor(t, u, v, c);
					}
				}

				return l;
			} );
		}
	}
}

