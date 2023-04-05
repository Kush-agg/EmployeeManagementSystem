using EMS.Repository.IRepository;
using EMS.Models;
using EMS.Data;

namespace EMS.Repository;

public class EmployeeSkillRepository : Repository<EmployeeSkill>, IEmployeeSkillRepository
{
    private ApplicationDbContext _context;

    public EmployeeSkillRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }


    public void Update(EmployeeSkill obj)
    {
        _context.EmployeeSkills.Update(obj);
    }
}
