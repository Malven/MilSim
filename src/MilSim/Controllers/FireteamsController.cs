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
    public class FireteamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FireteamsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Fireteams
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Fireteams.Include(f => f.Operation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Fireteams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fireteam = await _context.Fireteams.SingleOrDefaultAsync(m => m.FireteamId == id);
            if (fireteam == null)
            {
                return NotFound();
            }

            return View(fireteam);
        }

        // GET: Fireteams/Create
        public IActionResult Create()
        {
            ViewData["OperationId"] = new SelectList(_context.Operations, "OperationId", "OperationDescription");
            return View();
        }

        // POST: Fireteams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FireteamId,FireteamName,Member1,Member2,Member3,Member4,Member5,OperationId")] Fireteam fireteam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fireteam);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["OperationId"] = new SelectList(_context.Operations, "OperationId", "OperationDescription", fireteam.OperationId);
            return View(fireteam);
        }

        // GET: Fireteams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fireteam = await _context.Fireteams.SingleOrDefaultAsync(m => m.FireteamId == id);
            if (fireteam == null)
            {
                return NotFound();
            }
            ViewData["OperationId"] = new SelectList(_context.Operations, "OperationId", "OperationDescription", fireteam.OperationId);
            return View(fireteam);
        }

        // POST: Fireteams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FireteamId,FireteamName,Member1,Member2,Member3,Member4,Member5,OperationId")] Fireteam fireteam)
        {
            if (id != fireteam.FireteamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fireteam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FireteamExists(fireteam.FireteamId))
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
            ViewData["OperationId"] = new SelectList(_context.Operations, "OperationId", "OperationDescription", fireteam.OperationId);
            return View(fireteam);
        }

        // GET: Fireteams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fireteam = await _context.Fireteams.SingleOrDefaultAsync(m => m.FireteamId == id);
            if (fireteam == null)
            {
                return NotFound();
            }

            return View(fireteam);
        }

        // POST: Fireteams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fireteam = await _context.Fireteams.SingleOrDefaultAsync(m => m.FireteamId == id);
            _context.Fireteams.Remove(fireteam);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FireteamExists(int id)
        {
            return _context.Fireteams.Any(e => e.FireteamId == id);
        }
    }
}
