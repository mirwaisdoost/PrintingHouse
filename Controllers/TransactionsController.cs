using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrintingHouse.Models;

namespace PrintingHouse.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly PrintingHouseContext _context;

        public TransactionsController(PrintingHouseContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var printingHouseContext = _context.Transaction.Include(t => t.SubAccount).Include(t => t.Suplier);
            return View(await printingHouseContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.SubAccount)
                .Include(t => t.Suplier)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId");
            ViewData["SubAccountId"] = new SelectList(_context.SubAccount, "SubAccountId", "SubAccountId");
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "SuplierId");
            return View();
        }

      
        [HttpPost]
        public IActionResult Create(DateTime date, int suplierID, int subaccountID, decimal credit, decimal debit, string description)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = new Transaction
                {
                    Date = date,
                    SuplierId = suplierID,
                    SubAccountId = subaccountID,
                    Credit = credit,
                    Debit = debit,
                    Description = description
                };
                _context.Transaction.Add(transaction);
                _context.SaveChanges();
                
            }
            return View();
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
            ViewData["SubAccountId"] = new SelectList(_context.SubAccount, "SubAccountId", "SubAccountId", transaction.SubAccountId);
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "SuplierId", transaction.SuplierId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,Date,SuplierId,CustomerId,SubAccountId,Credit,Debit,Description")] Transaction transaction)
        {
            if (id != transaction.TransactionId)
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
                    if (!TransactionExists(transaction.TransactionId))
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
            ViewData["SubAccountId"] = new SelectList(_context.SubAccount, "SubAccountId", "SubAccountId", transaction.SubAccountId);
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "SuplierId", transaction.SuplierId);
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
                .Include(t => t.SubAccount)
                .Include(t => t.Suplier)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
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
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.TransactionId == id);
        }

        public JsonResult AutoCompleteSuplierSelect(string term)
        {
            var customer = (from item in _context.Suplier.Where(p => p.Name.Contains(term))
                            select new
                            { label = item.Name, id = item.SuplierId }).ToList();
            return Json(customer);
        }

        public JsonResult AutoCompleteSubAccountSelect(string term)
        {
            var customer = (from item in _context.SubAccount.Where(p => p.Name.Contains(term))
                            select new
                            { label = item.Name, id = item.SubAccountId }).ToList();
            return Json(customer);
        }
    }
}
