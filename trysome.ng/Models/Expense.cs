using System;

namespace trysome.ng.Models
{
    public class Expense
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; }
    }

}
