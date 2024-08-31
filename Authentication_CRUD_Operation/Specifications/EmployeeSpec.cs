using Authentication_CRUD_Operation.Model;
using Authentication_CRUD_Operation.Specifications.Base;

namespace Authentication_CRUD_Operation.Specifications
{
    public class EmployeeSpec : BaseSpecification<Employee>
    {
        public EmployeeSpec(string? name, string? email, string? phoneNumber, bool isGraduated , int pageSize, int pageNumber)
        {
            // add filter to the query using aggregate root
            if (!string.IsNullOrEmpty(name))
            {
                AddCriteria(x => x.Name == name);
            }
            if (!string.IsNullOrEmpty(email))
            {
                AddCriteria(x => x.Email == email);
            }
            if (phoneNumber != null)
            {
                AddCriteria(x => x.PhoneNumber == phoneNumber);
            }
            if (isGraduated)
            {
                AddCriteria(x => x.IsGraduated == isGraduated);
            }


            // add include to the query using aggregate root


            // add order by to the query using aggregate root
            //AddOrderByAsc(x=> x.Name);

            ApplyPagination(pageSize, pageNumber);


        }

    }
}
