using CommunicationFiling.DAL.Contracts;
using CommunicationFiling.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CommunicationFiling.DAL.Repositories
{
    public class UserRoleRepo : IUserRoleRepo
    {
        readonly CommFilingContext _context;

        public UserRoleRepo(CommFilingContext context)
        {
            _context = context;
        }

        public UserRole Get(long id)
        {
            return _context.UsersRoles
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<UserRole> Get()
        {
            return _context.UsersRoles
                .Where(x => x.IsValid == true)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<UserRole> Get(Expression<Func<UserRole, bool>> predicate)
        {
            return _context.UsersRoles
                .Where(predicate)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<UserRole> GetPaging(Expression<Func<UserRole, bool>> predicate, int page, int size, Expression<Func<UserRole, bool>> filterAttribute, bool descending)
        {
            var query = _context.UsersRoles.AsQueryable();

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

        public long Create(UserRole entity)
        {
            _context.UsersRoles.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public long Count()
        {
            return _context.UsersRoles.Count();
        }

        public long Count(Expression<Func<UserRole, bool>> predicate)
        {
            return _context.UsersRoles.Count(predicate);
        }

        public void Delete(UserRole entity)
        {
            _context.UsersRoles.Remove(entity);
        }

        public void Update(UserRole entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
