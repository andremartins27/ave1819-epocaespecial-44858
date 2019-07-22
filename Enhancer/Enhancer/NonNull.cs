using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhancer
{
    public class NonNull : EnhancerAttribute
    {
        public override void Check(object[] args)
        {
            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == null)
                {
                    throw new ArgumentException("Invalid argument");
                }
            }
        }
    }
}
