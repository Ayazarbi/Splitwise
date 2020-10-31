using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class DataserviceRepository: IDataService{
    private readonly AppdbContext context;
    private readonly IDataService service;

    public DataserviceRepository(AppdbContext context,IDataService service)
{
        this.context = context;
        this.service = service;
    }

    public async Task AddAsync<T>(T obj) where T : class
    {
        DbSet<T> table=await Setdb<T>();
        await table.AddAsync(obj);
        
    }

    public async Task<List<T>> Get<T>() where T : class
    {
        DbSet<T> table=await Setdb<T>();
        return table.ToList<T>();
    }

    public async Task Remove<T>(T obj) where T : class
    {
        DbSet<T> table=await Setdb<T>();
        table.Remove(obj);

    }

    public async Task RemoveRange<T>(List<T> obj) where T : class
    {
        DbSet<T> table=await Setdb<T>();
        table.RemoveRange(obj);
    }

    public async Task<DbSet<T>> Setdb<T>() where T : class
    {
        DbSet<T> table= await service.Setdb<T>();
        return table;
    }

    public async Task<IQueryable<T>> Where<T>(Expression<Func<T, bool>> expression) where T : class
    {
        DbSet<T> table=await Setdb<T>();
        return table.Where<T>(expression);
    }

  
}