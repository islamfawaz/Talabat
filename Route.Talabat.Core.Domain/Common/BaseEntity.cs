﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Common
{
    public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public   TKey ? Id { get;  set; }

        
    }
}
