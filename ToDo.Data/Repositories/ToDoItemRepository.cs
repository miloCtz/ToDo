using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories;

namespace ToDo.Data.Repositories;

internal sealed class ToDoItemRepository : IToDoItemRepository
{
    private readonly DatabaseContext _dbContext;

    public ToDoItemRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    public void Add(ToDoItem entity) =>
        _dbContext.Set<ToDoItem>().Add(entity);

    public void Delete(ToDoItem entity) =>
        _dbContext.Set<ToDoItem>().Remove(entity);

    public async Task<List<ToDoItem>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ToDoItem>().AsNoTracking().ToListAsync(cancellationToken);

    public async Task<ToDoItem?> GetAsync(int id, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ToDoItem>().FindAsync(id);
}

