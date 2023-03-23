using EMS.Models;

namespace EMS.Repository.IRepository;

public interface IEmployeeRepository : IRepository<Employee>
{
    void Update(Employee obj);
    
}