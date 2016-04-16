using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Symbolics.Core
{
    public class Hold : Operation
    {
        public Hold() : base("Hold", "'")
        {

        }

        public override Symbol Process(Group group, Context context)
        {
            return group[1];
        }
    }
}
