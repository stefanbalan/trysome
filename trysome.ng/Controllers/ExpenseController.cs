using System;
using Microsoft.AspNetCore.Mvc;
using trysome.ng.Models;

namespace trysome.ng.Controllers
{
    [Route("api/[controller]")]
    public class ExpenseController : Controller
    {
        [HttpPost("[action]")]
        public Expense[] Get()
        {
            return new Expense[]
            {
                new Expense()
                {
                    Amount = 12,
                    Category = "lunch", Date = DateTime.Now,
                    Tags = "t1, t2"
                }
            };
        }

        [HttpPost("[action]")]
        public void Post([FromBody] Expense expense)
        {


        }


    }
}