using FantasyOfSango.Constants;
using FantasyOfSango.Services;
using SangoCommon.Classs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MongoDBService mongoDBService = new MongoDBService();
            mongoDBService.InitService();
            string collectionName = "TestAvaterInfos";
            TestClass testClass = new TestClass
            {
                Name = "test",
                Strings = new List<string>
                {
                    "a","b"
                }
            };
            MongoDBService.Instance.AddOneData<TestClass>(testClass, collectionName);
        }
    }
}
