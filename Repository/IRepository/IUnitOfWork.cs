namespace EMS.Repository.IRepository;


public interface IUnitOfWork
{
    IEmployeeRepository Employee {get;}
    ISkillRespository Skill {get;}

    void Save();
}