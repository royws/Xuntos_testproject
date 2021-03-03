using Umbraco.Core;
using Umbraco.Core.Composing;
using Xuntos_testproject.Components;
using Xuntos_testproject.Database;
using Xuntos_testproject.Models;

namespace Xuntos_testproject.Composer
{
    public class UmbracoComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<RoutingSettingsComponent>();
            composition.Register(typeof(ApplicationDbContext));
        }
    }
}