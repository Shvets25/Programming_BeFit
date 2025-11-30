using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;

namespace BeFit.Controllers
{
    public class TrainingExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrainingExercises
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TrainingExercises
                .Include(t => t.ExerciseType)
                .Include(t => t.TrainingSession);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TrainingExercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingExercise = await _context.TrainingExercises
                .Include(t => t.ExerciseType)
                .Include(t => t.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingExercise == null)
            {
                return NotFound();
            }

            return View(trainingExercise);
        }

        
        public IActionResult Create()
        {
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name");
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions, "Id", "StartTime");
            return View();
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrainingSessionId,ExerciseTypeId,Load,Sets,Reps")] TrainingExercise trainingExercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingExercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ExerciseTypeId"] = new SelectList(
                _context.ExerciseTypes,
                "Id",
                "Name",
                trainingExercise.ExerciseTypeId
            );

            ViewData["TrainingSessionId"] = new SelectList(
                _context.TrainingSessions,
                "Id",
                "StartTime",
                trainingExercise.TrainingSessionId
            );

            return View(trainingExercise);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingExercise = await _context.TrainingExercises.FindAsync(id);
            if (trainingExercise == null)
            {
                return NotFound();
            }

            ViewData["ExerciseTypeId"] = new SelectList(
                _context.ExerciseTypes,
                "Id",
                "Name",
                trainingExercise.ExerciseTypeId
            );

            ViewData["TrainingSessionId"] = new SelectList(
                _context.TrainingSessions,
                "Id",
                "StartTime",
                trainingExercise.TrainingSessionId
            );

            return View(trainingExercise);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrainingSessionId,ExerciseTypeId,Load,Sets,Reps")] TrainingExercise trainingExercise)
        {
            if (id != trainingExercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingExercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExerciseExists(trainingExercise.Id))
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

            ViewData["ExerciseTypeId"] = new SelectList(
                _context.ExerciseTypes,
                "Id",
                "Name",
                trainingExercise.ExerciseTypeId
            );

            ViewData["TrainingSessionId"] = new SelectList(
                _context.TrainingSessions,
                "Id",
                "StartTime",
                trainingExercise.TrainingSessionId
            );

            return View(trainingExercise);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingExercise = await _context.TrainingExercises
                .Include(t => t.ExerciseType)
                .Include(t => t.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingExercise == null)
            {
                return NotFound();
            }

            return View(trainingExercise);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingExercise = await _context.TrainingExercises.FindAsync(id);
            if (trainingExercise != null)
            {
                _context.TrainingExercises.Remove(trainingExercise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExerciseExists(int id)
        {
            return _context.TrainingExercises.Any(e => e.Id == id);
        }
    }
}
