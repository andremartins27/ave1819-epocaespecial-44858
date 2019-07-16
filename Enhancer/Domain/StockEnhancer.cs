using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Enhancer;

namespace Domain
{
    public class StockEnhancer : Stock
    {
        public StockEnhancer(string name, string index) : base(name, index)
        {

        }

        public override string Market
        {
            get => base.Market;
            set
            {
                object[] args = new object[] { value };
                string methodname = "set_Market";
                string name = "Market";
                test.test3(this,methodname,name, args );
            }
        }

        public override int Price
        {
            get => base.Price;
            set
            {
                new Max(58).Check(new object[] { value });
                base.Price = value;
            }
        }

        public override string Trader
        {
            get => base.Trader;
            set
            {
                new Accept("Jenny", "Lily", "Valery").Check(new object[] { value });
                base.Trader = value;
            }
        }

        public override long Quote
        {
            get => base.Quote;
            set
            {
                new Min(73).Check(new object[] { value });
                base.Quote = value;
            }
        }

        public override double Rate
        {
            get => base.Rate;
            set
            {
                new Min(0.325).Check(new object[] { value });
                base.Rate = value;
            }
        }
    }
}

