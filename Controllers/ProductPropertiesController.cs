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
    public class ProductPropertiesController : Controller
    {
        private readonly PrintingHouseContext _context;

        public ProductPropertiesController(PrintingHouseContext context)
        {
            _context = context;
        }

        // GET: ProductProperties
        public async Task<IActionResult> Index()
        {
            var printingHouseContext = _context.ProductProperty.Include(p => p.ProPerty).Include(p => p.Product);
            return View(await printingHouseContext.ToListAsync());
        }

        // GET: ProductProperties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productProperty = await _context.ProductProperty
                .Include(p => p.ProPerty)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductPropertyId == id);
            if (productProperty == null)
            {
                return NotFound();
            }

            return View(productProperty);
        }

        // GET: ProductProperties/Create
        public IActionResult Create()
        {
            ViewData["ProPertyId"] = new SelectList(_context.Property, "ProPertyId", "ProPertyId");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId");
            return View();
        }

        // POST: ProductProperties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductPropertyId,ProductId,ProPertyId,Width,Height,Price")] ProductProperty productProperty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productProperty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProPertyId"] = new SelectList(_context.Property, "ProPertyId", "ProPertyId", productProperty.ProPertyId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", productProperty.ProductId);
            return View(productProperty);
        }

        // GET: ProductProperties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productProperty = await _context.ProductProperty.FindAsync(id);
            if (productProperty == null)
            {
                return NotFound();
            }
            ViewData["ProPertyId"] = new SelectList(_context.Property, "ProPertyId", "ProPertyId", productProperty.ProPertyId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", productProperty.ProductId);
            return View(productProperty);
        }

        // POST: ProductProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductPropertyId,ProductId,ProPertyId,Width,Height,Price")] ProductProperty productProperty)
        {
            if (id != productProperty.ProductPropertyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productProperty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPropertyExists(productProperty.ProductPropertyId))
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
            ViewData["ProPertyId"] = new SelectList(_context.Property, "ProPertyId", "ProPertyId", productProperty.ProPertyId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", productProperty.ProductId);
            return View(productProperty);
        }

        // GET: ProductProperties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productProperty = await _context.ProductProperty
                .Include(p => p.ProPerty)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductPropertyId == id);
            if (productProperty == null)
            {
                return NotFound();
            }

            return View(productProperty);
        }

        // POST: ProductProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productProperty = await _context.ProductProperty.FindAsync(id);
            _context.ProductProperty.Remove(productProperty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPropertyExists(int id)
        {
            return _context.ProductProperty.Any(e => e.ProductPropertyId == id);
        }
    }
}
