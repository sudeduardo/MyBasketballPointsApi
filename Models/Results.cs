using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBasketballPointsApi.Models
{
    public class Results
    {
        public int  AmountGame {get;set;}

        public int CountAllPoints {get;set;}

        public double AvgPointPerGame {get;set;}

        public int  MaxPointInGames {get;set;}

        public int  MinPointInGames {get;set;}
        public int AmountRecordBreaking {get;set;}
        public DateTime DataStart {get;set;}
        public DateTime DataEnd {get;set;}

    }
}