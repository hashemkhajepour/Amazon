﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Generic_Repo_Amazon<TEntity> where TEntity:class
    {
        private MyAmazon_Context _context;
        private DbSet<TEntity> _dbset;
        public Generic_Repo_Amazon(MyAmazon_Context Context)
        {
            _context = Context;
            _dbset = Context.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity,bool>> where = null, Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderby=null , string includes = "")
        {
            IQueryable<TEntity> query = _dbset;
            if(where != null)
            {
                query = query.Where(where);
            }
            if(orderby != null)
            {
                query = orderby(query);
            }
            if(includes != "")
            {
                foreach(string include in includes.Split(','))
                {
                    query = query.Include(include);
                }
                
            }
            return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbset.Find(id);
        }
        public virtual void Insert(TEntity entity)
        {
            _dbset.Add(entity);
        }
        public virtual void Update(TEntity entity)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbset.Attach(entity);
            }
            _dbset.Remove(entity);
        }
        public virtual void Delete(object id)
        {
            var entity = GetById(id);
            Delete(entity);
        }

    }
}
