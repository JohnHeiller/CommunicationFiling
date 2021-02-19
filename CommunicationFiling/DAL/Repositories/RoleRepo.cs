using CommunicationFiling.DAL.Contracts;
using CommunicationFiling.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CommunicationRole.DAL.Repositories
{
    public class RoleRepo : IRoleRepo
    {
        readonly CommFilingContext _context;

        public RoleRepo(CommFilingContext context)
        {
            _context = context;
        }

        public Role Get(long id)
        {
            return _context.Roles
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Role> Get()
        {
            return _context.Roles
                .Where(x => x.IsValid == true)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Role> Get(Expression<Func<Role, bool>> predicate)
        {
            return _context.Roles
                .Where(predicate)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Role> GetPaging(Expression<Func<Role, bool>> predicate, int page, int size, Expression<Func<Role, bool>> filterAttribute, bool descending)
        {
            var query = _context.Roles.AsQueryable();

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

        public long Create(Role entity)
        {
            _context.Roles.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public long Count()
        {
            return _context.Roles.Count();
        }

        public long Count(Expression<Func<Role, bool>> predicate)
        {
            return _context.Roles.Count(predicate);
        }

        public void Delete(Role entity)
        {
            _context.Roles.Remove(entity);
        }

        public void Update(Role entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
