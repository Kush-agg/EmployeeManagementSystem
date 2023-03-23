using EMS.Models;

namespace EMS.Repository.IRepository;

public interface ISkillRespository : IRepository<Skill>
{
    void Update(Skill obj);
    
}