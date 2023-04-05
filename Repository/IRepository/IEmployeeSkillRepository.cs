using EMS.Models;

namespace EMS.Repository.IRepository;

public interface IEmployeeSkillRepository : IRepository<EmployeeSkill>
{
    void Update(EmployeeSkill obj);
    
}