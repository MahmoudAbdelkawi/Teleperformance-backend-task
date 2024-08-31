using Authentication_CRUD_Operation.Data;
using Authentication_CRUD_Operation.Model;
using Authentication_CRUD_Operation.Repository.Generics;

namespace Authentication_CRUD_Operation.Repository.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
