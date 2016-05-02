using System;
using System.IO;

namespace Logic.Symbolics2
{
	public static class Module
	{
		public static Scope Scope {
			get;
			private set;
		}

		static Module()
		{
			Scope = new Scope();
			Scope.Functions.Add( "Load", Function.FromFunction<String>( Load ) );
			Scope.Functions.Add( "Save", Function.FromFunction<String, Symbol>( Save ) );

		}

		public static Symbol Load(String file, Context context)
		{
			
			return Core.Map(new Atom("Evaluate"), Symbol.Parse(File.ReadAllText( file )) as List, context);
		}

		public static Symbol Save(String file, Symbol value, Context context)
		{
			File.WriteAllText( file, value.ToString() );

			return Boolean.True;
		}


	}
}

