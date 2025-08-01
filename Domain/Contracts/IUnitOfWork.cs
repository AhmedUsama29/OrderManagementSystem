﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {

        Task<int> SaveChanges();

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

    }
}
