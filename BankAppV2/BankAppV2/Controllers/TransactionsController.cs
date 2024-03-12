/*
 * Gaspard TORTERAT stu-74536
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankAppV2.Data;
using BankAppV2.Models;
using Microsoft.AspNetCore.Authorization;

namespace BankAppV2.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static int startup = 1;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            if(startup == 0)
            {
                startup = 1;
                List<Transaction> transas = new List<Transaction>
                {
                    new Transaction
                    {
                        // Id = 1,
                        ACCreceiver = "js-7-90-78",
                        Amount = 90,
                        Date = DateTime.Now,
                        Action = "Deposit"
                    },
                    new Transaction
                    {
                        //Id = 2,
                        ACCsender = "me-9-67-09",
                        ACCreceiver = "js-7-90-78",
                        Amount = 70,
                        Date = DateTime.Now,
                        Action = "Transfer"
                    },
                    new Transaction
                    {
                        //Id = 35,
                        ACCsender = "lu-4-34-89",
                        ACCreceiver = "js-7-90-78",
                        Amount = 40,
                        Date = DateTime.Now,
                        Action = "Transfer"
                    },
                    new Transaction
                    {
                        //Id = 24,
                        ACCreceiver = "js-7-90-78",
                        Amount = -40,
                        Date = DateTime.Now,
                        Action = "Withdraw"
                    }
                };
                
                foreach (var item in transas)
                {
                    if (!_context.Transaction.Any(m => m.Id == item.Id))
                    { await Create(item); }
                }

            }
            //ViewData["TransactionData"] = transas;
            var names = new List<String>();
            foreach (var acc_name in _context.Account.Where(x => x.Holder == User.Identity.Name))
            {
                names.Add(acc_name.AccountName);
            }
            ViewData["Acc"] = names;
            return View(await _context.Transaction.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            var names = new List<String>();
            foreach (var acc_name in _context.Account.Where(x => x.Holder == User.Identity.Name))
            {
                names.Add(acc_name.AccountName);
            }
            ViewData["Acc"] = names;
            var accs = new List<(String,String)>();
            foreach (var acc_name in _context.Account)
            {
                var d = (acc_name.AccountName, acc_name.AccType.ToString());
                accs.Add(d);
            }
            ViewData["All"] = accs;
            return View();
        }
        public IActionResult Error_balance()
        {
            return View("Error_balance");
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ACCsender,ACCreceiver,Amount,Date,Action")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                //var am = transaction.Amount;
                //var accR = transaction.ACCreceiver;
                //var customer = await _context.Account
                //.FirstOrDefaultAsync(m => m.AccountName == accR);
                //Account.Balance += am;
                if(transaction.ACCsender != null)
                { 
                    var dif = _context.Account.Single(m => m.AccountName == transaction.ACCsender).Balance - transaction.Amount;
                    if (dif >= 0)
                    { 
                        _context.Account.Single(m => m.AccountName == transaction.ACCreceiver).Balance += transaction.Amount;
                        _context.Account.Single(m => m.AccountName == transaction.ACCsender).Balance -= transaction.Amount;
                    }
                    else
                    {
                        return RedirectToAction("Error_balance");
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }
//=====================================
        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ACCsender,ACCreceiver,Amount,Date,Action")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction != null)
            {
                _context.Transaction.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.Id == id);
        }

    }
}
