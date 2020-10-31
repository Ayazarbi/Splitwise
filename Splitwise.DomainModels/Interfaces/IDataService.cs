using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public interface IDataService
{
    
    Task<List<T>> Get<T>() where T: class;
    Task AddAsync<T>(T obj) where T : class;
    Task<IQueryable<T>> Where<T>(Expression<Func<T, bool>> expression) where T : class;
    Task Remove<T>(T obj) where T : class;
    Task RemoveRange<T>(List<T> obj) where T : class;

    Task<DbSet<T>> Setdb<T>() where T : class;

}
