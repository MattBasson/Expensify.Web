using Expensify.Database.Domain;

namespace Expensify.Web.Models
{
    public class HomeModel
    {
        public IEnumerable<Expense> Expenses { get; set; }
    }
}
