using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBasketballPointsApi.Models
{
    public class Game
    {
        public int? Id { get; set; }

        public DateTime? Date { get; set; } = null;

        public int? Points { get; set; }
    }
}
