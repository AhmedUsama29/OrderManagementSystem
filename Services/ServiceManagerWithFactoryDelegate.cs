using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManagerWithFactoryDelegate(Func<IAuthenticationService> AuthFactory,
                                                    Func<IOrderService> OrderFactory) : IServiceManager
    {
        public IAuthenticationService AuthenticationService => AuthFactory.Invoke();

        public IOrderService OrderService => OrderFactory.Invoke();
    }
}
