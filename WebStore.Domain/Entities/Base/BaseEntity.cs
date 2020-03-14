﻿using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities.Base
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
