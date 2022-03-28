using AniRateV1.DAL.Context;
using AniRateV1.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRateV1.Data
{
    public class DbInitializer
    {
        private readonly AniRateV1DB _db;
        private readonly ILogger<DbInitializer> _Logger;

        public Random Rnd { get; set; }

        public DbInitializer(AniRateV1DB db, ILogger<DbInitializer> Logger)
        {
            _db = db;
            _Logger = Logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();

            _Logger.LogInformation("Инициализация БД...");

            //_Logger.LogInformation("Удаление существующей БД...");
            ////асинхронно удаляем бд
            //await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            //_Logger.LogInformation("Удаление существующей БД выполнено за {0} мс", timer.ElapsedMilliseconds);

            //Создание бд (не продвинутое)
            //_db.Database.EnsureCreated();

            _Logger.LogInformation("Миграция БД...");
            //Создание бд + накатить на нее все миграции
            await _db.Database.MigrateAsync().ConfigureAwait(false);
            _Logger.LogInformation("Миграция БД выполнена за {0} мс", timer.ElapsedMilliseconds);

            //если бд проинициализированм - выходим
            if (await _db.AnimeTitles.AnyAsync()) return;

            await InitializeTitles();
            await InitializeCollections();

            _Logger.LogInformation("Инициализация БД выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }

        private const int __TitlesCount = 25;
        private AnimeTitle[] _Titles;
        private async Task InitializeTitles()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация тайтлов...");

            _Titles = new AnimeTitle[__TitlesCount];
            for (var i = 0; i < __TitlesCount; i++)
            {
                _Titles[i] = new AnimeTitle
                {
                    Name = $"Тайтл {Rnd.Next(1000)}",
                    Description = $"Description {i}",
                    Rating = i,
                };
            }


            await _db.AnimeTitles.AddRangeAsync(_Titles);
            await _db.SaveChangesAsync();

            _Logger.LogInformation("Инициализация тайтлов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int __CollectionsCount = 15;
        private AnimeCollection[] _Collections;
        private async Task InitializeCollections()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация коллекций...");

            _Collections = new AnimeCollection[__CollectionsCount];
            for (var i = 0; i < __CollectionsCount; i++)
            {
                _Collections[i] = new AnimeCollection
                {
                    Name = $"Тайтл {Rnd.Next(1000)}",
                    AnimeTitles = _Titles
                };
            }


            await _db.AnimeCollections.AddRangeAsync(_Collections);
            await _db.SaveChangesAsync();

            _Logger.LogInformation("Инициализация коллекций выполнена за {0} мс", timer.ElapsedMilliseconds);
        }
    }
}
