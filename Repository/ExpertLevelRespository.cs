using EMS.Repository.IRepository;
using EMS.Models;
using EMS.Data;

namespace EMS.Repository;

public class ExpertLevelRepository : Repository<ExpertLevel>, IExpertLevelRepository
{
    private ApplicationDbContext _context;

    public ExpertLevelRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }


    public void Update(ExpertLevel obj)
    {
        _context.ExpertLevels.Update(obj);
    }
}
