using AniRateV1.DAL.Context;
using AniRateV1.DAL.Entities.Base;
using AniRateV1.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AniRateV1.DAL.Repositories
{
    class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly AniRateV1DB _db;
        private readonly DbSet<T> _Set;

        public virtual IQueryable<T> Items => _Set;

        public bool AutoSaveChanges { get; set; } = true;

        public DbRepository(AniRateV1DB db)
        {
            _db = db;
            _Set = db.Set<T>();
        }

        #region Get
        public T Get(int id)
        {
            return Items.SingleOrDefault(i => i.Id == id);
        }

        public async Task<T> GetAsync(int id, CancellationToken Cancel = default)
        {
            return await Items.SingleOrDefaultAsync(i => i.Id == id, Cancel).ConfigureAwait(false);
        }
        #endregion


        #region Add
        public T Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                _db.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken Cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;
        }
        #endregion


        #region Remove
        public void Remove(int id)
        {
            var item = _Set.Local.FirstOrDefault(i => i.Id == id) ?? new T { Id = id };
            _db.Remove(item);
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            var item = _Set.Local.FirstOrDefault(i => i.Id == id) ?? new T { Id = id };
            _db.Remove(item);
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }
        #endregion


        #region Update
        public void Update(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken Cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }
        #endregion
    }
}
