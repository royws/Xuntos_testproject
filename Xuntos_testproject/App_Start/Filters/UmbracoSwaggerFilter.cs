using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace Xuntos_testproject.App_Start.Filters
{
    public class UmbracoSwaggerFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            foreach(ApiDescription apiDescription in apiExplorer.ApiDescriptions)
            {
                var matchingApiRoutes = swaggerDoc.paths.Where(x => x.Key.Contains("programminglanguage"));
                var swaggerRoutes = new Dictionary<string, PathItem>();
                foreach (var route in matchingApiRoutes)
                {
                    swaggerRoutes.Add(route.Key, route.Value);
                }
                swaggerDoc.paths = swaggerRoutes;
            }
        }
    }
}