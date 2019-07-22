using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhancer
{
    public class Min : EnhancerAttribute
    {
        private readonly double value;

        public Min(double value)
        {
            this.value = value;
        }

        public override void Check(object[] args)
        {
            IConvertible convert = args[1] as IConvertible;

            if (convert.ToDouble(null) < value)
                throw new ArgumentException("Minimum value is " + value);
        }
    }
}
