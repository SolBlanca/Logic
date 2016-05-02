using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Logic.Symbolics2
{
	public static class Concurrency
	{
		private static ConcurrentDictionary<string, Worker> Workers;
		private static ConcurrentQueue<Worker> Pending;

		static Concurrency()
		{
			Workers = new ConcurrentDictionary<string, Worker>();
			Pending = new ConcurrentQueue<Worker>();
		}

		public static void Engine()
		{

		}

		public static Symbol Start(Symbol handler, Context context)
		{
			Worker worker = new Worker(handler);

			var atom = new Atom(Guid.NewGuid().ToString());
			if (Workers.TryAdd( atom.Name, new Worker( handler ) )) {
				return atom;
			}

			return List.Nothing;
		}

		public static Symbol Stop(Symbol target, Context context)
		{
			var atom = target as Atom;

			Worker worker = null;
			if (Workers.TryRemove( atom.Name, out worker )) {
				worker.Running = false;
				return Boolean.True;
			}

			if (Workers.ContainsKey( atom.Name )) {
				return Boolean.False;
			}

			return Boolean.True;
		}

		public static Symbol Send(Symbol target, Symbol message)
		{
			var atom = target as Atom;

			Worker worker = null;
			if (Workers.TryGetValue( atom.Name, out worker )) {
				worker.Queue.Enqueue( message );
				return Boolean.True;
			}

			return Boolean.False;
		}

		public static Symbol Listen()
		{
			return null;
		}

		public static Symbol Emit(Symbol port, Symbol message)
		{
			return null;
		}

		public static Symbol Bind(List source, List destination)
		{
			return null;
		}

		// Router, Dealer, Pipe
	}

	public class Worker
	{
		public Int32 Pending;
		public Int32 Link;

		public Context Context {
			get;
			private set;
		}

		public ConcurrentQueue<Symbol> Queue {
			get;
			private set;
		}

		public bool Running {
			get;
		 	set;
		}

		public Worker(Symbol handler)
		{
			Context = new Context();
			Context.Scopes.Add( Core.Scope );
			Context.Scopes.Add( Arithmatic.Scope );
			Context.Scopes.Add( new Scope() );

			Core.Define( new Atom( "Recieve" ), new List( new Atom( "message" ) ), handler, Context );

			Queue = new ConcurrentQueue<Symbol>();

			Running = true;
		}
	}
}

