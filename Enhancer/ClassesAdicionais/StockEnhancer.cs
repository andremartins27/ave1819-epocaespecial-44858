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
                test.test1(this, methodname, name, args);
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
                test.test1(this, methodname, name, args);
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
                test.test1(this, methodname, name, args);
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
                test.test1(this, methodname, name, args);
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
                test.test1(this, methodname, name, args);
                base.Rate = value;
            }
        }

        public override double BuildInterest(Portfolio port, Store st)
        {
            string methodname = "BuildInterest";
            string name = "BuildInterest";
            object[] args = new object[] {this, port, st };
            test.test1(this, methodname, name, args);            //double toret =(Double) GetType().BaseType.GetMethod(methodname).Invoke(this, args);
            double toret = base.BuildInterest(port, st);
            test.test1(this, methodname, name, args);
            return toret;
        }
    }
}

