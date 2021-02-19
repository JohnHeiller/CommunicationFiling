using CommunicationFiling.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Contracts
{
    public interface IRoleActionRepo
    {
        RoleAction Get(long id);
        IEnumerable<RoleAction> Get();
        IEnumerable<RoleAction> Get(Expression<Func<RoleAction, bool>> predicate);
        IEnumerable<RoleAction> GetPaging(Expression<Func<RoleAction, bool>> predicate, int page, int size, Expression<Func<RoleAction, bool>> filterAttribute, bool descending);
        long Create(RoleAction entity);
        long Count();
        long Count(Expression<Func<RoleAction, bool>> predicate);
        void Delete(RoleAction entity);
        void Update(RoleAction entity);
    }
}
