using CommunicationFiling.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Contracts
{
    public interface IFilingRepo
    {
        Filing Get(long id);
        IEnumerable<Filing> Get();
        IEnumerable<Filing> Get(Expression<Func<Filing, bool>> predicate);
        IEnumerable<Filing> GetPaging(Expression<Func<Filing, bool>> predicate, int page, int size, Expression<Func<Filing, bool>> filterAttribute, bool descending);
        long Create(Filing entity);
        long Count();
        long Count(Expression<Func<Filing, bool>> predicate);
        void Delete(Filing entity);
        void Update(Filing entity);
    }
}
