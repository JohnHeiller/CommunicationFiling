using CommunicationFiling.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Contracts
{
    public interface IAuditRepo
    {
        Audit Get(long id);
        IEnumerable<Audit> Get();
        IEnumerable<Audit> Get(Expression<Func<Audit, bool>> predicate);
        IEnumerable<Audit> GetPaging(Expression<Func<Audit, bool>> predicate, int page, int size, Expression<Func<Audit, bool>> filterAttribute, bool descending);
        long Create(Audit entity);
        long Count();
        long Count(Expression<Func<Audit, bool>> predicate);
        void Delete(Audit entity);
        void Update(Audit entity);
    }
}
