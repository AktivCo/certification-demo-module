using Microsoft.Extensions.DependencyInjection;

namespace AktivCA.Domain.Shared.Module
{
    public class ApplicationModuleBase : IApplicationModuleBase
    {
        public virtual void Init() { }
        public virtual void Init(IServiceCollection serviceCollection) { }
    }
}
