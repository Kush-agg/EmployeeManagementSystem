using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EMS.Models;
using EMS.Repository.IRepository;
using EMS.Models.ViewModels;
using ClosedXML.Excel;
using System.Data;
using System.Reflection;
using EMS.Model.ViewModels;

namespace EMS.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    static List<Employee> print;
    static List<ViewSkill> list = new List<ViewSkill>();
    static bool view = false;
    
    public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index(string name, string skillId, string Level)
    {
        HomeIndex hi ;
        hi = new HomeIndex();
        hi.skills = _unitOfWork.Skill.GetAll().OrderBy(s => s.name).ToList();
        hi.searchHomeIndex = new SearchHomeIndex();
        hi.employees = _unitOfWork.Employee.GetAll().ToList();
        var temp = hi.employees;
        ViewBag.skills = list;
        ViewBag.modal = view;
        if(name!=null)
        {
            temp = temp.Where(t=> (t.firstName.ToLower().Contains(name.ToLower())|| t.lastName.ToLower().Contains(name.ToLower()))).ToList();
            hi.employees = temp;
        }
        if(skillId!= null || Level!= null)
        {
            var employeeSkill = _unitOfWork.EmployeeSkill.GetAll().ToList();
            if(skillId!= null)
            {
                employeeSkill = employeeSkill.Where(s=> s.skillId.ToString()==skillId).ToList();
            }
            if(Level!= null)
            {
                employeeSkill = employeeSkill.Where(s=> s.level.ToLower()==Level.ToLower()).ToList();
            }
            List<Employee> e = new List<Employee>();
            foreach(var ek in employeeSkill)
            {
                var entry = temp.Where(q=> q.employeeId==ek.employeeId).FirstOrDefault();
                if(entry != null && !e.Contains(entry)){
                    e.Add(entry);
                }
            }
            hi.employees = e;
        }
        print = hi.employees;
        return View(hi);
    }

    public IActionResult ExportExcel()
    {
      
      try
      {
        var data = print;
        if(data != null & data.Count>0)
        {
          using(XLWorkbook wb = new XLWorkbook())
          {
            wb.Worksheets.Add(ToConvertDataTable(data.ToList()));
            using(MemoryStream strem = new MemoryStream())
            {
              wb.SaveAs(strem);
              string fileName = $"EmployeeList_{DateTime.Now.ToString("dd/MM/yyyy")}.xlsx";
              return File(strem.ToArray(),"application/vnd.openxmlformats-officedocuments.spreadsheetml.sheet",fileName);
            }
          }     
        }
        TempData["Error"] = "Data Not Found!";
      }

      catch(Exception ex)
      {

      }
      return RedirectToAction("Index");
    }

    public DataTable ToConvertDataTable<T>(List<T> items)
    {
       DataTable dt = new DataTable(typeof(T).Name);
     PropertyInfo[] propInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
     foreach(PropertyInfo prop in propInfo)
     {
      dt.Columns.Add(prop.Name);
     }
     foreach(T item in items)
     {
      var values = new object[propInfo.Length];
      for(int i=0; i<propInfo.Length;i++)
      {
        values[i]=propInfo[i].GetValue(item, null);
        
      }
         dt.Rows.Add(values);
     }
        return dt;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult ViewSkills(int id){
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
            list = s;
            view = true;
            return View("Index","Home");
        }

        public IActionResult turnFalse(){
            view = false;
            return View("Index","Home");
        }

       
}
