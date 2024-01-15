using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlayersMVCApplication.BusinessLayer.BusinessLogics;
using PlayersMVCApplication.BusinessLayer.Interfaces;
using PlayersMVCApplication.Data;
using PlayersMVCApplication.Models;

public class TeamsController : Controller
{
    private readonly PlayerMVCDbContext _context;


    public TeamsController(PlayerMVCDbContext context)
    {
        _context = context;
    }

    //Get RandomNumber
    [HttpPost]
    public JsonResult GetRandomNumber()
    {
        IBusinessLogics logics = new BusinessLogics();
        int teamId = logics.GetRandomNumber().Result;
        return Json(teamId);
    }

    // GET: Teams
    public async Task<IActionResult> Index()
    {
        return _context.Team != null ?
                    View(await _context.Team.ToListAsync()) :
                    Problem("Entity set 'PlayerMVCDbContext.Team'  is null.");
    }

    // GET: Teams/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null || _context.Team == null)
        {
            return NotFound();
        }

        var team = await _context.Team
            .FirstOrDefaultAsync(m => m.TeamId == id);
        if (team == null)
        {
            return NotFound();
        }

        return View(team);
    }

    // GET: Teams/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Teams/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string teamId, string teamName)
    {
        Team team = new();
        if (ModelState.IsValid)
        {
            team.TeamId = teamId;
            team.TeamName = teamName;

            _context.Add(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(team);
    }

    // GET: Teams/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null || _context.Team == null)
        {
            return NotFound();
        }

        var team = await _context.Team.FindAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        return View(team);
    }

    // POST: Teams/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("TeamId,TeamName")] Team team)
    {
        if (id != team.TeamId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(team);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(team.TeamId))
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
        return View(team);
    }

    // GET: Teams/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null || _context.Team == null)
        {
            return NotFound();
        }

        var team = await _context.Team
            .FirstOrDefaultAsync(m => m.TeamId == id);
        if (team == null)
        {
            return NotFound();
        }

        return View(team);
    }

    // POST: Teams/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (_context.Team == null)
        {
            return Problem("Entity set 'PlayerMVCDbContext.Team'  is null.");
        }
        var team = await _context.Team.FindAsync(id);
        if (team != null)
        {
            _context.Team.Remove(team);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TeamExists(string id)
    {
        return (_context.Team?.Any(e => e.TeamId == id)).GetValueOrDefault();
    }
}


