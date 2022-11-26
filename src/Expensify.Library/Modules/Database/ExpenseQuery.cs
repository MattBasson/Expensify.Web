using Expensify.Database;
using Expensify.Database.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Expensify.Library.Modules.Database
{
    
    public class ExpenseQuery
    {
        private readonly ILogger<ExpenseQuery> _logger;
        private readonly ExpensifyContext _dbContext;

        public ExpenseQuery(ILogger<ExpenseQuery> logger, ExpensifyContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Expense>> ExecuteAsync(Expression<Func<Expense, bool>> predicate)
        {
            return await _dbContext.Expense.Where(predicate).ToListAsync();
        }
    }
}
