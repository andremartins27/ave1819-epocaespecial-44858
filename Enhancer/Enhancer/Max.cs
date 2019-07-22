using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhancer
{
    public class Max : EnhancerAttribute
    {
        private readonly double value;

        public Max(double value)
        {
            this.value = value;
        }

        public override void Check(object[] args)
        {
            IConvertible convert = args[1] as IConvertible;

            if (convert.ToDouble(null) > value)
                throw new ArgumentException("Maximum value is " + value);
        }
    }
}
