using Microsoft.AspNetCore.Mvc;
using trysome.ng.Models;

namespace trysome.ng.Controllers
{
    [Route("api/[controller]")]
    public class ExpenseController : Controller
    {

        [HttpPost("[action]")]
        public void Post([FromBody] Expense expense)
        {


        }


    }
}