using System.Linq.Expressions;

namespace Authentication_CRUD_Operation.Specifications.Base
{
    public interface ISpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criterias { get; set; } // select 
        public List<Expression<Func<T, object>>> Includes { get; set; } // include 
        public Expression<Func<T, object>> OrderByAsc { get; set; } // order ascending
        public Expression<Func<T, object>> OrderByDesc { get; set; } // order descending
        public int PageSize { get; set; } // PageSize
        public int PageNumber { get; set; } // PageNumber
        public bool IsPaginationEnabled { get; set; } // pagination
    }
}
