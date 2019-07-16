using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhancer
{
    public class NoEffects : EnhancerAttribute
    {
        public override void Check(object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
