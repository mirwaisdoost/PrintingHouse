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
    public class SubAccountsController : Controller
    {
        private readonly PrintingHouseContext _context;

        public SubAccountsController(PrintingHouseContext context)
        {
            _context = context;
        }

        // GET: SubAccounts
        public async Task<IActionResult> Index()
        {
            var printingHouseContext = _context.SubAccount.Include(s => s.Account);
            return View(await printingHouseContext.ToListAsync());
        }

        // GET: SubAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subAccount = await _context.SubAccount
                .Include(s => s.Account)
                .FirstOrDefaultAsync(m => m.SubAccountId == id);
            if (subAccount == null)
            {
                return NotFound();
            }

            return View(subAccount);
        }

        // GET: SubAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int AccountID, string name)
        {
            if (ModelState.IsValid)
            {
                SubAccount subAccount = new SubAccount
                {
                    AccountId = AccountID,
                    Name = name,
                    Balance = 0
                };
                _context.SubAccount.Add(subAccount);
                _context.SaveChanges();
                return Json(subAccount);
            }
          
            return View();
        }

        // GET: SubAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subAccount = await _context.SubAccount.FindAsync(id);
            if (subAccount == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "AccountId", "AccountId", subAccount.AccountId);
            return View(subAccount);
        }

           [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("SubAccountId,AccountId,Name,Balance")] SubAccount subAccount)
        {
            if (id != subAccount.SubAccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubAccountExists(subAccount.SubAccountId))
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
            ViewData["AccountId"] = new SelectList(_context.Account, "AccountId", "AccountId", subAccount.AccountId);
            return View(subAccount);
        }

        // GET: SubAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subAccount = await _context.SubAccount
                .Include(s => s.Account)
                .FirstOrDefaultAsync(m => m.SubAccountId == id);
            if (subAccount == null)
            {
                return NotFound();
            }

            return View(subAccount);
        }

        // POST: SubAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subAccount = await _context.SubAccount.FindAsync(id);
            _context.SubAccount.Remove(subAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubAccountExists(int id)
        {
            return _context.SubAccount.Any(e => e.SubAccountId == id);
        }

        public JsonResult AutoCompleteAccountSelect(string term)
        {
            var customer = (from item in _context.Account.Where(p => p.Name.Contains(term))
                            select new
                            { label = item.Name, id = item.AccountId }).ToList();
            return Json(customer);
        }
    }
}
