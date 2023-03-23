using EMS.Repository.IRepository;
using EMS.Models;
using EMS.Data;

namespace EMS.Repository;

public class SkillRespository : Repository<Skill>, ISkillRespository
{
    private ApplicationDbContext _context;

    public SkillRespository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }


    public void Update(Skill obj)
    {
        _context.Skills.Update(obj);
    }
}
