using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MilSim.Data;
using MilSim.Models;

namespace MilSim.Controllers
{
    public class OperationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OperationsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Operations
        public async Task<IActionResult> Index() {
            var db = _context.Operations.Include( f => f.Fireteams );
            return View(await db.ToListAsync());
        }

        // GET: Operations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations.SingleOrDefaultAsync(m => m.OperationId == id);
            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // GET: Operations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Operations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OperationId,OperationDescription,OperationTitle")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(operation);
        }

        // GET: Operations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations.SingleOrDefaultAsync(m => m.OperationId == id);
            if (operation == null)
            {
                return NotFound();
            }
            return View(operation);
        }

        // POST: Operations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OperationId,OperationDescription,OperationTitle")] Operation operation)
        {
            if (id != operation.OperationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationExists(operation.OperationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(operation);
        }

        // GET: Operations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations.SingleOrDefaultAsync(m => m.OperationId == id);
            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operation = await _context.Operations.SingleOrDefaultAsync(m => m.OperationId == id);
            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OperationExists(int id)
        {
            return _context.Operations.Any(e => e.OperationId == id);
        }
    }
}
