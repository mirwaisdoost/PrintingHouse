using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrintingHouse.Models;
using PrintingHouse.ViewModels;

namespace PrintingHouse.Controllers
{
    public class ProductViewModelsController : Controller
    {
        private readonly PrintingHouseContext _context;

        public ProductViewModelsController(PrintingHouseContext context)
        {
            _context = context;
        }

        // GET: ProductViewModels
        public IActionResult Index()
        {
            List<ProductViewModel> lst = new List<ProductViewModel>();
            ProductViewModel pvm;
            var product = _context.Product;

            foreach(Product p in product)
            {
                pvm = new ProductViewModel();
                pvm.ProductID = p.ProductId;
                pvm.ProductName = p.Name;
                lst.Add(pvm);
            }
            return View(lst);
        }

        public IActionResult AddProduct(string ProductName, List<int> ServiceID, List<int> PropertyID, List<decimal> Price)
        {
            if (ModelState.IsValid)
            {
                var productID = _context.Product.Max(p => p.ProductId) + 1;

                Product product = new Product
                {
                    ProductId = productID,
                    Name = ProductName
                };
                _context.Product.Add(product);

                int i = 0;
                foreach (var p in PropertyID)
                {
                    ProductProperty productProperty = new ProductProperty
                    {
                        ProductId = productID,
                        ProPertyId = p,
                        Price = Price[i]
                    };
                    _context.ProductProperty.Add(productProperty);
                    i++;
                }

                foreach (var p in ServiceID)
                {
                    ProdductService productService = new ProdductService
                    {
                        ProductId = productID,
                        ServiceId = p,
                    };
                    _context.ProdductService.Add(productService);
                }

                _context.SaveChanges();
            }
            return Json("Saved!");
        }

        // GET: ProductViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // GET: ProductViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,ServiceID,ServiceName,PropertyID,PropertNyame,Price")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: ProductViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel.FindAsync(id);
            if (productViewModel == null)
            {
                return NotFound();
            }
            return View(productViewModel);
        }

        // POST: ProductViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ProductID,ProductName,ServiceID,ServiceName,PropertyID,PropertNyame,Price")] ProductViewModel productViewModel)
        {
            if (id != productViewModel.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductViewModelExists(productViewModel.ProductID))
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
            return View(productViewModel);
        }

        // GET: ProductViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // POST: ProductViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var productViewModel = await _context.ProductViewModel.FindAsync(id);
            _context.ProductViewModel.Remove(productViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductViewModelExists(int? id)
        {
            return _context.ProductViewModel.Any(e => e.ProductID == id);
        }

        public JsonResult AutoCompleteServiceSelect(string term)
        {
            var customer = (from item in _context.Service.Where(p => p.Name.Contains(term))
                            select new
                            { label = item.Name, id = item.ServiceId }).ToList();
            return Json(customer);

        }public JsonResult AutoCompletePropertySelect(string term)
        {
            var customer = (from item in _context.Property.Where(p => p.Name.Contains(term))
                            select new
                            { label = item.Name, id = item.ProPertyId}).ToList();
            return Json(customer);
        }
    }
}
