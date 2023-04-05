using EMS.Data;
using Microsoft.AspNetCore.Mvc;
using EMS.Repository.IRepository;
using EMS.Models;
using EMS.Models.ViewModels;

namespace EMS.Controllers;

    public class EmployeeSkillController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeSkillController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _unitOfWork.EmployeeSkill.GetAll() == null)
            {
                return NotFound();
            }

            var EmployeeSkill =  _unitOfWork.EmployeeSkill
                .GetFirstOrDefault(e => e.employeeSkillId == id);
            if (EmployeeSkill == null)
            {
                return NotFound();
            }

            return View(EmployeeSkill);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeSkill EmployeeSkill)
        {
            if (ModelState.IsValid)
            {
                var data = _unitOfWork.EmployeeSkill.GetAll().ToList();
                var check = data.Where(s=> s.employeeSkillId.Contains(EmployeeSkill.employeeSkillId)).FirstOrDefault();
                if(check==null)
                {
                    _unitOfWork.EmployeeSkill.Add(EmployeeSkill);
                    _unitOfWork.Save();
                    return View("Index","Employee");
                }
            }
            return View(EmployeeSkill);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _unitOfWork.EmployeeSkill.GetAll() == null)
            {
                return NotFound();
            }
            var EmployeeSkill = _unitOfWork.EmployeeSkill.GetFirstOrDefault(e => e.employeeSkillId==id);
            if (EmployeeSkill == null)
            {
                return NotFound();
            }
            return View(EmployeeSkill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EmployeeSkill employeeSkill)
        {
            if (id != employeeSkill.employeeSkillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(employeeSkill);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _unitOfWork.EmployeeSkill.GetAll() == null)
            {
                return NotFound();
            }

            var EmployeeSkill = _unitOfWork.EmployeeSkill
                .GetFirstOrDefault(e => e.employeeSkillId == id);
            if (EmployeeSkill == null)
            {
                return NotFound();
            }else{
                _unitOfWork.EmployeeSkill.Remove(EmployeeSkill);
            }
            _unitOfWork.Save();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_unitOfWork.EmployeeSkill.GetAll() == null)
            {
                return Problem("Entity set 'MvcEmployeeContext.EmployeeSkill'  is null.");
            }
            var EmployeeSkill = _unitOfWork.EmployeeSkill.GetFirstOrDefault(e => e.employeeSkillId == id);
            if (EmployeeSkill != null)
            {
                _unitOfWork.EmployeeSkill.Remove(EmployeeSkill);
            }
            
             _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
