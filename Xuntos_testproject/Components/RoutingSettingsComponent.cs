using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Umbraco.Core.Composing;

namespace Xuntos_testproject.Components
{
    public class RoutingSettingsComponent : IComponent
    {
        public void Initialize()
        {
            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}