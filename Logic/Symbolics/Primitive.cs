using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics
{
    public class Primitive<T> : Symbol
    {
        public T Value { get; private set; }

        public Primitive(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override Symbol Clone()
        {
            return new Primitive<T>(Value);
        }
    }
}
