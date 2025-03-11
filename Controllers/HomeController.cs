using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SpendSmartDbContext _context;//Add the SpendSmartDbContext to the HomeController

        public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)//Add the SpendSmartDbContext to the HomeController constructor
        {
            _logger = logger;//Assign the logger to the _logger field 
            _context = context;//Assign the context to the _context field
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Expenses()
        {
            var expenses = _context.Expenses.ToList();
            //Get all the expenses from the database and store them in a list
            var totalExpenses = expenses
                .Where(x => x.Status == ExpenseStatus.Approved)//Filter the expenses that are approved
                .Sum(x => x.Value);//Calculate the total expenses
            ViewBag.TotalExpenses = totalExpenses;//Pass the total expenses to the 
            return View(expenses);//Return the list of expenses to the view
        }
        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null)
            {
                //Get the expense from the database
                var expenseInDb = _context.Expenses.SingleOrDefault(x => x.Id == id);
                return View(expenseInDb);//Return the expense to the view
            }
            return View();
        }
        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(x => x.Id == id);//Get the expense from the database
            _context.Expenses.Remove(expenseInDb);//Remove the expense from the database
            _context.SaveChanges();//Save the changes to the database
            return RedirectToAction("Expenses");//Redirect to the Expenses action
        }
        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if (model.Id == 0)
            {
                //creating
                _context.Expenses.Add(model);//Add the new expense to the database
            }
            else
            {
                //editing
                _context.Expenses.Update(model);//Update the expense in the database

            }
            
            _context.SaveChanges();//Save the changes to the database

            return RedirectToAction("Expenses");//Redirect to the Expenses action
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
