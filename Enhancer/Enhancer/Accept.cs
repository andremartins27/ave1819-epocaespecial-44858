using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhancer
{
    public class Accept : EnhancerAttribute
    {
        private readonly object[] valid;

        public Accept(params object[] valid)
        {
            this.valid = valid;
        }

        public override void Check(object[] args)
        {
            bool equal = false;
            for (int i = 1; i < args.Length; i++)
            {
                foreach (string s in valid)
                {
                    if (s.Equals(args[1]))
                    {
                        equal = true;
                        break;
                    }
                }
                if (!equal) throw new ArgumentException("Not accepted parameter");
                equal = false;
            }
        }
    }
}
