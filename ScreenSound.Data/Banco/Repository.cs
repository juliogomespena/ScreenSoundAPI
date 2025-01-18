using Microsoft.EntityFrameworkCore;
using ScreenSound.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ScreenSoundEfContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ScreenSoundEfContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public T? FindByParameter(Func<T, bool> predicate) => _dbSet.FirstOrDefault(predicate);

    public IEnumerable<T> ListAll() => _dbSet.ToList();

    public void Add(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }
}
