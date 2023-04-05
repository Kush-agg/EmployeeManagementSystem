using EMS.Data;
using Microsoft.AspNetCore.Mvc;
using EMS.Repository.IRepository;
using EMS.Models;
using EMS.Models.ViewModels;

namespace EMS.Controllers;

public class SkillController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public SkillController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult AssignSkill()
    {
            return View( _unitOfWork.Skill.GetAll().OrderBy(s => s.name));
    }

    public IActionResult Index()
    {
        SkillIndex si = new SkillIndex();
        si.Skills = _unitOfWork.Skill.GetAll().OrderBy(s => s.name).ToList();
        si.Skill = new Skill();
        return View(si);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _unitOfWork.Skill.GetAll() == null)
        {
            return NotFound();
        }

        var Skill =  _unitOfWork.Skill
            .GetFirstOrDefault(s => s.skillId == id);
        if (Skill == null)
        {
            return NotFound();
        }

        return View(Skill);
    }

    public IActionResult Create()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("name")] Skill Skill)
    {
        if (ModelState.IsValid)
        {
            var data = _unitOfWork.Skill.GetAll().ToList();
            var check = data.Where(s=> s.name.ToLower().Contains(Skill.name.ToLower())).FirstOrDefault();
            if(check==null)
            {
                _unitOfWork.Skill.Add(Skill);
                _unitOfWork.Save();
                TempData["success"]="Skill created successfully";
                return RedirectToAction(nameof(Index));
            }else{
                TempData["success"]="Skill already exists";

            }
            
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _unitOfWork.Skill.GetAll() == null)
        {
            return NotFound();
        }
        var Skill = _unitOfWork.Skill.GetFirstOrDefault(s => s.skillId==id);
        if (Skill == null)
        {
            return NotFound();
        }
        return View(Skill);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Skill skill)
    public async Task<IActionResult> Edit(int id, Skill skill)
    {
        if (id != skill.skillId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(skill);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _unitOfWork.Skill.GetAll() == null)
        {
            return NotFound();
        }

        var Skill = _unitOfWork.Skill
            .GetFirstOrDefault(s => s.skillId == id);
        if (Skill == null)
        {
            return NotFound();
        }

        return View(Skill);
    }

    // POST: Employees/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_unitOfWork.Skill.GetAll() == null)
        {
            return Problem("Entity set 'MvcEmployeeContext.Skill'  is null.");
        }
        var Skill = _unitOfWork.Skill.GetFirstOrDefault(s => s.skillId == id);
        if (Skill != null)
        {
            _unitOfWork.Skill.Remove(Skill);
        }
        
            _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }
}
