using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Repository
{
    public interface IRepository<Tbl_Entity> where Tbl_Entity : class
    {
        IEnumerable<Tbl_Entity> GetAllRecorders();
        //GetFirstorDefault
        Tbl_Entity GetById(int recordId);
        void Add(Tbl_Entity entity);
        void Update(Tbl_Entity entity);
        void Remove(Tbl_Entity entity);
    }
}