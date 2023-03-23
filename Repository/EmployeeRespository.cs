using EMS.Repository.IRepository;
using EMS.Models;
using EMS.Data;

namespace EMS.Repository;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    private ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }


    public void Update(Employee obj)
    {
        _context.Employees.Update(obj);
    }
}
