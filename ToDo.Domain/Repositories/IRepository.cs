﻿using ToDo.Domain.Primitives;

namespace ToDo.Domain.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<T?> GetAsync(int id, CancellationToken cancellationToken = default);

        void Add(T entity);

        void Delete(T entity);
    }
}
