using CommunicationFiling.DAL.Contracts;
using CommunicationFiling.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CommunicationFiling.DAL.Repositories
{
    public class RoleActionRepo : IRoleActionRepo
    {
        readonly CommFilingContext _context;

        public RoleActionRepo(CommFilingContext context)
        {
            _context = context;
        }

        public RoleAction Get(long id)
        {
            return _context.RolesActions
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<RoleAction> Get()
        {
            return _context.RolesActions
                .Where(x => x.IsValid == true)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<RoleAction> Get(Expression<Func<RoleAction, bool>> predicate)
        {
            return _context.RolesActions
                .Where(predicate)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<RoleAction> GetPaging(Expression<Func<RoleAction, bool>> predicate, int page, int size, Expression<Func<RoleAction, bool>> filterAttribute, bool descending)
        {
            var query = _context.RolesActions.AsQueryable();

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

        public long Create(RoleAction entity)
        {
            _context.RolesActions.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public long Count()
        {
            return _context.RolesActions.Count();
        }

        public long Count(Expression<Func<RoleAction, bool>> predicate)
        {
            return _context.RolesActions.Count(predicate);
        }

        public void Delete(RoleAction entity)
        {
            _context.RolesActions.Remove(entity);
        }

        public void Update(RoleAction entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
