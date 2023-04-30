using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    [Serializable]
    public class TestClass
    {
        public string Name { get; set; }
        public List<string> Strings {  get; set; }
    }
}
