using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IBaseRepository<TPrimaryKey, TEntity>
    {
        public Task<TEntity> Create(TEntity src);
        public Task<TEntity> Edit(TEntity data);
        public Task<TEntity> Get(TPrimaryKey id);
        public Task<List<TEntity>> GetAll();
        public Task Delete(TPrimaryKey id);

    }
}
