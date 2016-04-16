﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics
{
    public abstract class Operation : Symbol
    {
        public Atom Atom { get; private set; }
        public IList<string> Aliases { get; private set; }

        public Operation(string name, params string[] aliases )
        {
            Atom = new Atom(name);
            Aliases = new List<string>(aliases);
        }

        public virtual Symbol Process(Group group, Context context)
        {
            Evaluate(group, context);
            return group;
        }

        public override string ToString()
        {
            return Atom.Name;
        }

        public override Symbol Clone()
        {
            return this;
        }

        public static void Evaluate(Group group, Context context)
        {
            for (int i = 1; i < group.Elements.Count; i++)
            {
                group[i] = context.Process(group[i]);
            }
        }
    }
}
