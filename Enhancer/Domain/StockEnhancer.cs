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
                object[] args = new object[] { this, value };
                string methodname = "set_Market";
                string name = "Market";
                new test().test1(this, methodname, name, args);
                base.Market = value;
            }
        }

        public override int Price
        {
            get => base.Price;
            set
            {
                object[] args = new object[] { this, value };
                string methodname = "set_Price";
                string name = "Price";
                new test().test1(this, methodname, name, args);
                base.Price = value;
            }
        }

        public override string Trader
        {
            get => base.Trader;
            set
            {
                object[] args = new object[] { this, value };
                string methodname = "set_Trader";
                string name = "Trader";
                new test().test1(this, methodname, name, args);
                base.Trader = value;
            }
        }

        public override long Quote
        {
            get => base.Quote;
            set
            {
                object[] args = new object[] { this, value };
                string methodname = "set_Quote";
                string name = "Quote";
                new test().test1(this, methodname, name, args);
                base.Quote = value;
            }
        }

        public override double Rate
        {
            get => base.Rate;
            set
            {
                object[] args = new object[] { this, value };
                string methodname = "set_Rate";
                string name = "Rate";
                new test().test1(this, methodname, name, args);
                base.Rate = value;
            }
        }

        public override double BuildInterest(Portfolio port, Store st)
        {
            object[] argsThis = new object[] {this, port,st };
            string methodname = "BuildInterest";
            string name = "BuildInterest";
            object[] args = new object[] { port, st };
            new test().test1(this, methodname, name, argsThis);
            //double toret =(Double) GetType().BaseType.GetMethod(methodname).Invoke(this, args);
            double toret = base.BuildInterest(port, st);
            new test().test1(this, methodname, name, argsThis);
            return toret;
        }
    }
}

