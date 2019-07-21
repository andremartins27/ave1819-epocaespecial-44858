using Domain;
using Enhancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApp
{
    class Program
    {
        static void Main(string[] args)
        {

            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            Console.ReadKey();
            Stock st = Enhancer.Enhancer.Build<Stock>("Apple", "Dow Jones");
            //Stock st = new StockEnhancer("Apple", "Dow Jones");
            //st.Quote = 90;
            Console.WriteLine(st.Quote);
            st.Market = "test";
            st.BuildInterest(new Portfolio(), new Store());


            Console.WriteLine("End");

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
