﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Contract
{
    public interface IStoreContextInitializer
    {
        Task InitializerAsync();
        Task SeedAsnc();

    }
}