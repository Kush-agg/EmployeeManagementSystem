using EMS.Data;
using Microsoft.AspNetCore.Mvc;
using EMS.Repository.IRepository;
using EMS.Models;
using EMS.Models.ViewModels;
using EMS.Model.ViewModels;

namespace EMS.Controllers;

// public class EmployeeController : Controller
// {
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        static List<ViewSkill> sk = new List<ViewSkill>();
        static bool modal = false;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            EmployeeIndex ei = new EmployeeIndex();
            ei.Employees = _unitOfWork.Employee.GetAll().ToList();
            ei.Skills = _unitOfWork.Skill.GetAll().ToList();
            ei.employeeSkill = new EmployeeSkill();
            ei.employeeSkills = _unitOfWork.EmployeeSkill.GetAll().ToList();
            ViewBag.skills = sk;
            ViewBag.modal = modal;
            return View(ei);
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
        // public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Employee employee)
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.employeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Employee.Update(employee);
                _unitOfWork.Save();
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

        public List<Employee> GetEmployeesByName(string name)
        {
            if(name!=null){
                var temp = _unitOfWork.Employee.GetAll().ToList();
                temp = temp.Where(t=> (t.firstName.Contains(name)|| t.lastName.Contains(name))).ToList();
                return temp;
            }
            return new List<Employee>();
        }

        // public IActionResult ShowpopUp(int id)
        // {
        //     var employeeSkill = _unitOfWork.EmployeeSkill.GetAll().ToList();
        //     employeeSkill = employeeSkill.Where(es=> es.employeeId==id).ToList();
        //     List<Skill> s = new List<Skill>();
        //     var skills = _unitOfWork.Skill.GetAll().ToList();
        //     foreach(var item in employeeSkill)
        //     {
        //         var q = skills.Where(s=> s.skillId==item.skillId).FirstOrDefault();
        //         if(q!= null)
        //         {
        //             s.Add(q);
        //         }
        //     }
        //     ViewBag.EmployeeId = id;
        //     // var venue = _context.Venues.FirstOrDefault(x => x.Id == id);
        //     //specify the name or path of the partial view
        //     return PartialView("_ViewSkill",s);
        // }
        
        public IActionResult AssignViewBag(int id){
            var employeeSkill = _unitOfWork.EmployeeSkill.GetAll().ToList();
            employeeSkill = employeeSkill.Where(es=> es.employeeId==id).ToList();
            List<ViewSkill> s = new List<ViewSkill>();
            var skills = _unitOfWork.Skill.GetAll().ToList();
            foreach(var item in employeeSkill)
            {
                var q = skills.Where(s=> s.skillId==item.skillId).FirstOrDefault();
                if(q!= null)
                {
                    ViewSkill a = new ViewSkill();
                a.employeeSkillId = item.employeeSkillId;
                a.skillName = q.name;
                a.level = item.level;
                a.experience = item.experience;
                    s.Add(a);
                }
            }
            sk = s;
            modal = true;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult turnFalse(){
            modal = false;
            return RedirectToAction(nameof(Index));
        }
    }
