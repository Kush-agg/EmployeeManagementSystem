using EMS.Repository.IRepository;
using EMS.Data;

namespace EMS.Repository;


public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Employee = new EmployeeRepository(_context);
        Skill = new SkillRespository(_context);
        EmployeeSkill = new EmployeeSkillRepository(_context);
    }
    public IEmployeeRepository Employee {get; private set;}
    public ISkillRespository Skill {get; private set;}
    public IEmployeeSkillRepository EmployeeSkill{get; private set;}

    public void Save()
    {
        _context.SaveChanges();
    }
}