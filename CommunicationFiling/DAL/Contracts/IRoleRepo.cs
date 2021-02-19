using CommunicationFiling.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Contracts
{
    public interface IRoleRepo
    {
        Role Get(long id);
        IEnumerable<Role> Get();
        IEnumerable<Role> Get(Expression<Func<Role, bool>> predicate);
        IEnumerable<Role> GetPaging(Expression<Func<Role, bool>> predicate, int page, int size, Expression<Func<Role, bool>> filterAttribute, bool descending);
        long Create(Role entity);
        long Count();
        long Count(Expression<Func<Role, bool>> predicate);
        void Delete(Role entity);
        void Update(Role entity);
    }
}
