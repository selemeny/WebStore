using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB db;

        public WebStoreDBInitializer(WebStoreDB _dB) => db = _dB;

        public void Initialize() => InitializeAsync().Wait();

        public async Task InitializeAsync()
        {
            var database = db.Database;

            //// Создание БД в ручную (без миграций)
            //if (await database.EnsureDeletedAsync().ConfigureAwait(false))
            //    if (!await database.EnsureCreatedAsync().ConfigureAwait(false))
            //        throw new InvalidOperationException("Не удалось создать БД");

            await database.MigrateAsync().ConfigureAwait(false);

            if (await db.Products.AnyAsync())
                return;

            using(var Transaction = await database.BeginTransactionAsync().ConfigureAwait(false))
            {
                await db.Sections.AddRangeAsync(TestData.Sections).ConfigureAwait(false);

                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");// Разрешаем базе вставлять свой первичный кюч ID и внешний ключ ~ParentId, т.к. в TestData у нас прописаны эти данные
                await db.SaveChangesAsync().ConfigureAwait(false);
                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                await Transaction.CommitAsync().ConfigureAwait(false);
            }

            using (var Transaction = await database.BeginTransactionAsync().ConfigureAwait(false))
            {
                await db.Brands.AddRangeAsync(TestData.Brands).ConfigureAwait(false);

                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");// Разрешаем базе вставлять свой первичный кюч ID и внешний ключ ~ParentId, т.к. в TestData у нас прописаны эти данные
                await db.SaveChangesAsync().ConfigureAwait(false);
                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                await Transaction.CommitAsync().ConfigureAwait(false);
            }

            using (var Transaction = await database.BeginTransactionAsync().ConfigureAwait(false))
            {
                await db.Products.AddRangeAsync(TestData.Products).ConfigureAwait(false);

                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");// Разрешаем базе вставлять свой первичный кюч ID и внешний ключ ~ParentId, т.к. в TestData у нас прописаны эти данные
                await db.SaveChangesAsync().ConfigureAwait(false);
                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

                await Transaction.CommitAsync().ConfigureAwait(false);
            }

            using (var Transaction = await database.BeginTransactionAsync().ConfigureAwait(false))
            {
                await db.Employees.AddRangeAsync(TestData.Employees).ConfigureAwait(false);

                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employees] ON");// Разрешаем базе вставлять свой первичный кюч ID и внешний ключ ~ParentId, т.к. в TestData у нас прописаны эти данные
                await db.SaveChangesAsync().ConfigureAwait(false);
                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employees] OFF");

                await Transaction.CommitAsync().ConfigureAwait(false);
            }

        }
    }
}
