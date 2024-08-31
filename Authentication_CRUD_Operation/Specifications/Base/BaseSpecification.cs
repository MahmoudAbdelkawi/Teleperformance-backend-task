using System.Linq.Expressions;

namespace Authentication_CRUD_Operation.Specifications.Base
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criterias { get; set; } = new List<Expression<Func<T, bool>>>();
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderByAsc { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criterias.Add(criteria); // x => x.Name == "John" , x => x.Age > 20
        }
        public void AddInclude(Expression<Func<T, object>> Include)
        {
            Includes.Add(Include); // x => x.Address, x => x.Department
        }
        public void AddOrderByAsc(Expression<Func<T, object>> orderAsc)
        {
            OrderByAsc = orderAsc;  // x => x.Name
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderDesc)
        {
            OrderByDesc = orderDesc; // x => x.Age
        }
        public void ApplyPagination(int _pageSize, int _pageNumber)
        {
            IsPaginationEnabled = true;
            PageNumber = _pageNumber;
            PageSize = _pageSize;
            
        }
    }
}
