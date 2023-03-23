using EMS.Data;
using Microsoft.AspNetCore.Mvc;
using EMS.Repository.IRepository;
using EMS.Models;

namespace EMS.Controllers;

// public class EmployeeController : Controller
// {
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
              return View( _unitOfWork.Employee.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.Employee.GetAll() == null)
            {
                return NotFound();
            }

            var Employee =  _unitOfWork.Employee
                .GetFirstOrDefault(e => e.employeeId == id);
            if (Employee == null)
            {
                return NotFound();
            }

            return View(Employee);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee Employee)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Employee.Add(Employee);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.Employee.GetAll() == null)
            {
                return NotFound();
            }
            var Employee = _unitOfWork.Employee.GetFirstOrDefault(e => e.employeeId==id);
            if (Employee == null)
            {
                return NotFound();
            }
            return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Employee employee)
        {
            if (id != employee.employeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork.Employee.GetAll() == null)
            {
                return NotFound();
            }

            var Employee = _unitOfWork.Employee
                .GetFirstOrDefault(e => e.employeeId == id);
            if (Employee == null)
            {
                return NotFound();
            }

            return View(Employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork.Employee.GetAll() == null)
            {
                return Problem("Entity set 'MvcEmployeeContext.Employee'  is null.");
            }
            var Employee = _unitOfWork.Employee.GetFirstOrDefault(e => e.employeeId == id);
            if (Employee != null)
            {
                _unitOfWork.Employee.Remove(Employee);
            }
            
             _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
