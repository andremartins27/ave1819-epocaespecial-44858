using Domain;
using Enhancer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace Tests
{
    [TestClass]
    public class StockTest
    {
        Stock st;

        public StockTest()
        {

            st = Enhancer.Enhancer.Build<Stock>("Apple", "Dow Jones");
            //st = new StockEnhancer("Apple", "Dow Jones");


        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StockMarketFail()
        {

            st.Market = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StockTraderFail()
        {
            st.Trader = "Andre";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StockQuoteFail()
        {
            st.Quote = 70;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StockRateFail()
        {
            st.Rate = 0.1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StockPriceFail()
        {
            st.Price = 60;
        }
    }
}
