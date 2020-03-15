using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Interfaces
{
    /// <summary>
    /// Каталог товаров
    /// </summary>
    public interface IProductData
    {
        /// <summary>
        /// Получить все секции
        /// </summary>
        /// <returns></returns>
        IEnumerable<Section> GetSections();
        /// <summary>
        /// Получить все бренды
        /// </summary>
        /// <returns></returns>
        IEnumerable<Brand> GetBrands();
    }
}
