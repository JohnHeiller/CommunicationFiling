using CommunicationFiling.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Action = CommunicationFiling.DAL.Entities.Action;

namespace CommunicationFiling.DAL.Contracts
{
    public interface IActionRepo
    {
        Action Get(long id);
        IEnumerable<Action> Get();
        IEnumerable<Action> Get(Expression<Func<Action, bool>> predicate);
        IEnumerable<Action> GetPaging(Expression<Func<Action, bool>> predicate, int page, int size, Expression<Func<Action, bool>> filterAttribute, bool descending);
        long Create(Action entity);
        long Count();
        long Count(Expression<Func<Action, bool>> predicate);
        void Delete(Action entity);
        void Update(Action entity);
    }
}
