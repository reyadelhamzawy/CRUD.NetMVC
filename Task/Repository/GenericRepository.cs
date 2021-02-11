using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Task.Models;

namespace Task.Repository
{
    public class GenericRepository<Tbl_Entity> : IRepository<Tbl_Entity> where Tbl_Entity : class
    {
        private TaskEntities _DBEntity;
        DbSet<Tbl_Entity> _DbSet;
        public GenericRepository(TaskEntities DBEntity)
        {
            _DBEntity = DBEntity;
            _DbSet = DBEntity.Set<Tbl_Entity>();
        }
        public void Add(Tbl_Entity entity)
        {
            _DbSet.Add(entity);
            _DBEntity.SaveChanges();
        }

        public IEnumerable<Tbl_Entity> GetAllRecorders()
        {
            return _DbSet.ToList();
        }

        public Tbl_Entity GetById(int recordId)
        {
            return _DbSet.Find(recordId);
        }

        public void Remove(Tbl_Entity entity)
        {
            if (_DBEntity.Entry(entity).State == EntityState.Detached)
            {
                _DbSet.Attach(entity);
            }
            _DbSet.Remove(entity);
        }

        public void Update(Tbl_Entity entity)
        {
            _DbSet.Attach(entity);
            _DBEntity.Entry(entity).State = EntityState.Modified;
            _DBEntity.SaveChanges();
        }
    }
}