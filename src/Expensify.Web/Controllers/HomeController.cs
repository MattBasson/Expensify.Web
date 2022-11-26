using Expensify.Library.Modules.Database;
using Expensify.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Expensify.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExpenseQuery _expenseQuery;

        public HomeController(ExpenseQuery expenseQuery)
        {
            _expenseQuery = expenseQuery;
        }

        public async Task<IActionResult> Index()
        {

            return View(new HomeModel()
            {
                Expenses = await _expenseQuery.ExecuteAsync(w => w.ReceiptImage != null)
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

