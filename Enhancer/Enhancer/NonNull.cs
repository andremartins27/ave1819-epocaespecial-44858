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
            Console.WriteLine("Check");
            if (args == null || args[0] == null)
            {
                throw new ArgumentException("Invalid argument");
            }
            return;
        }
    }
}
