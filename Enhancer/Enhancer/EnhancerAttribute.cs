using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhancer
{
    public abstract class EnhancerAttribute : Attribute
    {
        public abstract void Check(object[] args);
    }
}