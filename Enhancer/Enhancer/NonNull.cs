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
            Console.WriteLine("nonnull - "+ args[1]);
            if (args == null || args[1] == null)
            {
                throw new ArgumentException("Invalid argument");
            }
            return;
        }
    }
}
