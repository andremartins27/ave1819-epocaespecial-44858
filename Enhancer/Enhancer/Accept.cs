using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhancer
{
    public class Accept : EnhancerAttribute
    {
        private readonly string[] valid;

        public Accept(params string[] valid)
        {
            this.valid = valid;
        }

        public override void Check(object[] args)
        {
            foreach(string s in valid)
            {
                if(!s.Equals(args[0]))throw new ArgumentException("Not accepted parameter");
            }
            
        }

    }
}
