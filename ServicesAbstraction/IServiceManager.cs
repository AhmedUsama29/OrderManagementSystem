﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IServiceManager
    {
        public IAuthenticationService AuthenticationService { get; }

        public IOrderService OrderService { get; }

    }
}
