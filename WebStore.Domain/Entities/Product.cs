﻿using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public virtual Section Section { get; set; }

        public int? BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }


        public string ImageUrl{ get; set; }

        [Column(TypeName ="decimal(18,2)")]// 18 знаков, из них 2 после запятой
        public decimal Price{ get; set; }

        public string Description { get; set; }

        [NotMapped]
        public int NotMappedProperty { get; set; } // Свойство не сохраняемое в БД
    }
}
