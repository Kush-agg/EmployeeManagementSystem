using EMS.Data;
using Microsoft.AspNetCore.Mvc;
using EMS.Repository.IRepository;
using EMS.Models;
using EMS.Models.ViewModels;
using EMS.Model.ViewModels;

namespace EMS.Controllers;

// public class EmployeeController : Controller
// {
    public class ExpertLevelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExpertLevelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.ExpertLevel.GetAll() == null)
            {
                return NotFound();
            }

            var ExpertLevel =  _unitOfWork.ExpertLevel
                .GetFirstOrDefault(e => e.Id == id);
            if (ExpertLevel == null)
            {
                return NotFound();
            }

            return View(ExpertLevel);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpertLevel ExpertLevel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ExpertLevel.Add(ExpertLevel);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(ExpertLevel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.ExpertLevel.GetAll() == null)
            {
                return NotFound();
            }
            var ExpertLevel = _unitOfWork.ExpertLevel.GetFirstOrDefault(e => e.Id==id);
            if (ExpertLevel == null)
            {
                return NotFound();
            }
            return View(ExpertLevel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpertLevel ExpertLevel)
        {
            if (id != ExpertLevel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.ExpertLevel.Update(ExpertLevel);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(ExpertLevel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork.ExpertLevel.GetAll() == null)
            {
                return NotFound();
            }

            var ExpertLevel = _unitOfWork.ExpertLevel
                .GetFirstOrDefault(e => e.Id == id);
            if (ExpertLevel == null)
            {
                return NotFound();
            }

            return View(ExpertLevel);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork.ExpertLevel.GetAll() == null)
            {
                return Problem("Entity set 'MvcEmployeeContext.Employee'  is null.");
            }
            var ExpertLevel = _unitOfWork.ExpertLevel.GetFirstOrDefault(e => e.Id == id);
            if (ExpertLevel != null)
            {
                _unitOfWork.ExpertLevel.Remove(ExpertLevel);
            }
            
             _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

    }
