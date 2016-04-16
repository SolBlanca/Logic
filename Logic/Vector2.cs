using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2()
        {

        }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2 Add(Vector2 value)
        {
            X += value.X;
            Y += value.Y;

            return this;
        }

        public Vector2 Subtract(Vector2 value)
        {
            X -= value.X;
            Y -= value.Y;

            return this;
        }

        public static double Dot(Vector2 left, Vector2 right)
        {
            return (left.X * right.X) + (left.Y * right.Y);
        }


        public override bool Equals(object obj)
        {
            Vector2 other = obj as Vector2;

            if (other == null)
            {
                return false;
            }

            return (X == other.X) && (Y == other.Y);
        }

        public override string ToString()
        {
            StringBuilder value = new StringBuilder();

            value.Append("( ");
            value.Append(X.ToString());
            value.Append(", ");
            value.Append(Y.ToString());
            value.Append(" )");

            return value.ToString();
        }
    }
}
