using CommunicationFiling.DAL.Contracts;
using CommunicationFiling.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Action = CommunicationFiling.DAL.Entities.Action;

namespace CommunicationFiling.DAL.Repositories
{
    public class ActionRepo : IActionRepo
    {
        protected CommFilingContext _context;

        public ActionRepo(CommFilingContext context)
        {
            _context = context;
        }

        public Action Get(long id)
        {
            return _context.Actions
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Action> Get()
        {
            return _context.Actions
                .Where(x => x.IsValid == true)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Action> Get(Expression<Func<Action, bool>> predicate)
        {
            return _context.Actions
                .Where(predicate)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Action> GetPaging(Expression<Func<Action, bool>> predicate, int page, int size, Expression<Func<Action, bool>> filterAttribute, bool descending)
        {
            var query = _context.Actions.AsQueryable();

            query = query.Where(predicate);

            if (descending)
            {
                query = query.OrderByDescending(filterAttribute);
            }
            else
            {
                query = query.OrderBy(filterAttribute);
            }

            return query.Skip((page - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToList();
        }

        public long Create(Action entity)
        {
            _context.Actions.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public long Count()
        {
            return _context.Actions.Count();
        }

        public long Count(Expression<Func<Action, bool>> predicate)
        {
            return _context.Actions.Count(predicate);
        }

        public void Delete(Action entity)
        {
            _context.Actions.Remove(entity);
        }

        public void Update(Action entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
