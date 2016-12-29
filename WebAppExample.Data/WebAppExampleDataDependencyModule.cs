using Ninject.Modules;
using Utils.Resolution;
using WebAppExample.Data.Repositories;
using WebAppExample.Lib.Data.Repositories;

namespace WebAppExample.Data
{
    public class WebAppExampleDataDependencyModule : NinjectModule
    {
        public override void Load()
        {
            IoC.AddPrototype<IContactRepository, ContactRepository>();
            IoC.AddPrototype<IUserRepository, UserRepository>();
        }
    }
}