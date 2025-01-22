using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AktivCA.Test.Base
{
    public class TestBase
    {
        protected IServiceProvider _serviceProvider { get; set; }
        protected IServiceCollection serviceCollection { get; set; }
        protected void RegisterDI()
        {
            var webApp = new Web.Program();
            serviceCollection = new ServiceCollection();
            _serviceProvider = Web.Program.TestImplementation(serviceCollection);
        }
    }
}
