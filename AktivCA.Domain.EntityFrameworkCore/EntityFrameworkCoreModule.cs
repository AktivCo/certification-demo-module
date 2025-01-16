using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AktivCA.Domain.Shared.Module;
using Microsoft.Extensions.DependencyInjection;

namespace AktivCA.Domain.EntityFrameworkCore
{
    public class EntityFrameworkCoreModule:ApplicationModuleBase, IApplicationModuleBase
    {
        protected IServiceProvider _services;
        public EntityFrameworkCoreModule(IServiceProvider services)
        {
            _services = services;
        }
        public override void Init()
        {
        }
    }
}
