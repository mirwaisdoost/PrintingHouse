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
    public class OrderPropertiesController : Controller
    {
        private readonly PrintingHouseContext _context;

        public OrderPropertiesController(PrintingHouseContext context)
        {
            _context = context;
        }

        // GET: OrderProperties
        public async Task<IActionResult> Index()
        {
            var printingHouseContext = _context.OrderProperty.Include(o => o.Order).Include(o => o.ProductProperty);
            return View(await printingHouseContext.ToListAsync());
        }

        // GET: OrderProperties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProperty = await _context.OrderProperty
                .Include(o => o.Order)
                .Include(o => o.ProductProperty)
                .FirstOrDefaultAsync(m => m.OrderTypeId == id);
            if (orderProperty == null)
            {
                return NotFound();
            }

            return View(orderProperty);
        }

        // GET: OrderProperties/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId");
            ViewData["ProductPropertyId"] = new SelectList(_context.ProductProperty, "ProductPropertyId", "ProductPropertyId");
            return View();
        }

        // POST: OrderProperties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderTypeId,OrderId,ProductPropertyId,SupId,SupFare,Width,Height,Quantity")] OrderProperty orderProperty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderProperty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", orderProperty.OrderId);
            ViewData["ProductPropertyId"] = new SelectList(_context.ProductProperty, "ProductPropertyId", "ProductPropertyId", orderProperty.ProductPropertyId);
            return View(orderProperty);
        }

        // GET: OrderProperties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProperty = await _context.OrderProperty.FindAsync(id);
            if (orderProperty == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", orderProperty.OrderId);
            ViewData["ProductPropertyId"] = new SelectList(_context.ProductProperty, "ProductPropertyId", "ProductPropertyId", orderProperty.ProductPropertyId);
            return View(orderProperty);
        }

        // POST: OrderProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderTypeId,OrderId,ProductPropertyId,SupId,SupFare,Width,Height,Quantity")] OrderProperty orderProperty)
        {
            if (id != orderProperty.OrderTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderProperty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderPropertyExists(orderProperty.OrderTypeId))
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
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", orderProperty.OrderId);
            ViewData["ProductPropertyId"] = new SelectList(_context.ProductProperty, "ProductPropertyId", "ProductPropertyId", orderProperty.ProductPropertyId);
            return View(orderProperty);
        }

        // GET: OrderProperties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProperty = await _context.OrderProperty
                .Include(o => o.Order)
                .Include(o => o.ProductProperty)
                .FirstOrDefaultAsync(m => m.OrderTypeId == id);
            if (orderProperty == null)
            {
                return NotFound();
            }

            return View(orderProperty);
        }

        // POST: OrderProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderProperty = await _context.OrderProperty.FindAsync(id);
            _context.OrderProperty.Remove(orderProperty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderPropertyExists(int id)
        {
            return _context.OrderProperty.Any(e => e.OrderTypeId == id);
        }
    }
}
