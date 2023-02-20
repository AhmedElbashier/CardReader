using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardReader.Classes;
namespace CardReader.Classes
{
    class mcardsea
    {
        public TestResult result = new TestResult();
        public CardInfo info = new CardInfo();
        public void getData()
        {
           //////
        }
        
        

    }

    public class Result
    {
        public int code = 0;
        public string message = "";
    }

    public class TestResult : Result
    {
        public string[] data;
    }
    public class CardInfo
    {
        public string cardid { get; set; }
        public int cjihao { get; set; }
        public int mjihao { get; set; }
        public int status { get; set; }
        public string time { get; set; }
        public int output { get; set; }
    }

}
