using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Symbolics2
{
	public abstract class Symbol
	{
		public abstract SymbolType Type { get; }

		public Symbol()
		{
		}


		public static Symbol Parse(string value)
		{
			Stack<List> groupings = new Stack<List>();
			var root = new List();
			groupings.Push(root);

			for (int i = 0; i < value.Length; )
			{
				switch (value[i])
				{
				case '(':
					var group = new List();
					groupings.Peek().Children.Add(group);
					groupings.Push(group);

					i++;
					break;
				case ')':
					groupings.Pop();

					i++;
					break;
				default:
					if (value[i] == ',' || Char.IsWhiteSpace(value[i]))
					{
						i++;
					}
					else if (value[i] == '"')
					{
						var end = value.IndexOfAny(new char[] { '"' }, ++i);
						var text = value.Substring(i, end - i);

						groupings.Peek().Children.Add(new String(text));

						i = end + 1;
					}
					else if (Char.IsNumber(value[i]))
					{
						var end = value.IndexOfAny(new char[] { ' ', ',', ')' }, i);

						if (end == -1)
						{
							end = value.Length;
						}

						var number = value.Substring(i, end - i);

						groupings.Peek().Children.Add(new Number(double.Parse(number)));

						i = end;
					}
					else
					{
						var end = value.IndexOfAny(new char[] { ' ', ',', ')' }, i);

						if (end == -1)
						{
							end = value.Length;
						}

						var text = value.Substring(i, end - i);

						groupings.Peek().Children.Add(new Atom(text));

						i = end;
					}
					break;
				}
			}

			if (root.Children.Count > 1)
			{
				return root;
			}
			else if (root.Children.Count == 1)
			{
				return root[0];
			}
			return List.Nothing;
		}
	}

	public class Atom : Symbol
	{
		public override SymbolType Type {
			get {
				return SymbolType.Atom;
			}
		}

		public string Name {
			get;
			private set;
		}

		public Atom(string name)
		{
			Name = name;
		}

		public override string ToString()
		{
			return Name;
		}
	}

	public class Boolean : Atom
	{
		public bool Value {
			get;
			private set;
		}

		protected Boolean(bool value) : base(value ? "True" : "False")
		{
			Value = value;
		}

		public static implicit operator bool(Boolean value)
		{
			return value.Value;
		}

		public static implicit operator Boolean(bool value)
		{
			return value ? True : False;
		}

		public static readonly Boolean True = new Boolean( true );
		public static readonly Boolean False = new Boolean( false );
	}

	public class List : Symbol
	{
		public override SymbolType Type {
			get {
				return SymbolType.List;
			}
		}

		public Symbol this [int index] {
			get { return Children [index]; }
			set { Children [index] = value; }
		}

		public int Count {
			get { return Children.Count; }
		}

		public List()
		{
			Children = new List<Symbol>();
		}

		public List(params Symbol[] symbols)
		{
			Children = new List<Symbol>( symbols );
		}
			
		public List<Symbol> Children {
			get;
			private set;
		}

		public override string ToString()
		{
			StringBuilder value = new StringBuilder();

			value.Append("( ");

			foreach (var symbol in Children)
			{
				value.Append(symbol.ToString());
				value.Append(' ');
			}

			value.Append(')');

			return value.ToString();
		}

		public static readonly List Nothing = new List();
	}

	public class Number : Symbol
	{
		public double Value {
			get;
			private set;
		}

		public override SymbolType Type {
			get {
				return SymbolType.Number;
			}
		}

		public Number(double value)
		{
			Value = value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}

	public class String: Symbol
	{
		public string Value {
			get;
			private set;
		}

		public override SymbolType Type {
			get {
				return SymbolType.String;
			}
		}

		public String(string value)
		{
			Value = value;
		}

		public override string ToString()
		{
			return "\"" + Value.ToString() + "\"";
		}

		public static implicit operator string(String value)
		{
			return value.Value;
		}

		public static implicit operator String(string value)
		{
			return new String(value);
		}
	}

	public enum SymbolType
	{
		Atom,
		List,
		Number,
		String
	}
}

