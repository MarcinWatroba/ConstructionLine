using ConstructionLine.CodingChallenge.Tests;
using System;

namespace SearchApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SearchEngineTests test = new SearchEngineTests();
            SearchEnginePerformanceTests perfTest = new SearchEnginePerformanceTests();
            //test.Test();

            perfTest.Setup();
            perfTest.PerformanceTest();
        }
    }
}
