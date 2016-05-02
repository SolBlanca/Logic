using System;
using System.Text;

namespace Logic.Symbolics2
{
	public static class Text
	{
		public static Scope Scope {
			get;
			private set;
		}

		static Text()
		{
			Scope = new Scope();

			Scope.Functions.Add( "Concatinate", new Function( Concatinate ) );
			Scope.Functions.Add( ".", Scope.Functions ["Concatinate"] );
			Scope.Functions.Add( "Parse", Function.FromFunction<String>( Parse ) );

		}

		public static Symbol Concatinate(List list, Context context)
		{
			StringBuilder value = new StringBuilder();

			for (int i = 1; i < list.Count; i++) {
				if (list [i].Type == SymbolType.String) {
					value.Append( ((String)list [i]).Value );
				} else {
					value.Append( list [i].ToString() );
				}
			}

			return new String( value.ToString() );
		}

		public static Symbol Parse(String value, Context context) {
			return Symbol.Parse( value );
		}
	}
}

