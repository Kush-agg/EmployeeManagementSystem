using EMS.Models;

namespace EMS.Repository.IRepository;

public interface IExpertLevelRepository : IRepository<ExpertLevel>
{
    void Update(ExpertLevel obj);
    
}