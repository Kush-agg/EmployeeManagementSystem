namespace EMS.Repository.IRepository;


public interface IUnitOfWork
{
    IEmployeeRepository Employee {get;}
    ISkillRespository Skill {get;}
    IEmployeeSkillRepository EmployeeSkill{get;}

    void Save();
}