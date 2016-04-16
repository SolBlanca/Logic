﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics.Core
{
    public class Numeric : Operation
    {
        public Numeric() : base("Numeric", "N")
        {

        }

        public override Symbol Process(Group group, Context context)
        {
            context.Numeric = true;

            var value = context.Process(group[1]);

            context.Numeric = false;

            return value;
        }
    }
}