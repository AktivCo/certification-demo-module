using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AktivCA.Domain.Shared.Module
{
    public interface IApplicationModuleBase
    {
        public void Init();
        public void Init(IServiceCollection serviceCollection);
    }
}
