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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StockBuildInterestFail()
        {
            st.BuildInterest(new Portfolio(), new Store());
        }

        [TestMethod]
        public void StockMarketSuccess()
        {
            st.Market = "isel";
            Assert.AreEqual(st.Market, "isel");
        }

        [TestMethod]
        public void StockTraderSuccess()
        {
            st.Trader = "Lily";
            Assert.AreEqual(st.Trader, "Lily");
        }

        [TestMethod]
        public void StockQuoteSuccess()
        {
            st.Quote = 80;
            Assert.AreEqual(st.Quote, 80);
        }

        [TestMethod]
        public void StockRateSuccess()
        {
            st.Rate = 1.02;
            Assert.AreEqual(st.Rate, 1.02);
        }
        [TestMethod]
        public void StockPriceSuccess()
        {
            st.Price = 40;
            Assert.AreEqual(st.Price, 40);
        }

        [TestMethod]
        public void StockBuildInterestSuccess()
        {
            st.BuildInterest(null, null);
            Assert.AreEqual(st, st);
        }

    }
}
