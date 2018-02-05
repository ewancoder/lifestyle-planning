namespace Lifestyle.Planning.WebApi.SelfHost
{
    using System.Web.Http;
    using global::Autofac;
    using global::Autofac.Integration.WebApi;
    using Autofac;
    using Newtonsoft.Json.Serialization;
    using Owin;
    using Swashbuckle.Application;

    /// <summary>
    /// Owin startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Owin startup configuration method.
        /// </summary>
        /// <param name="appBuilder">Owin application builder.</param>
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            SetupJson(config);
            SetupSwagger(config);

            var container = SetupContainer();

            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);
        }

        private static IContainer SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(ProjectController).Assembly);
            builder.RegisterModule<LifestylePlanningModule>();
            return builder.Build();
        }

        private static void SetupJson(HttpConfiguration config)
        {
            var json = config.Formatters.JsonFormatter;

            json.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            json.SupportedMediaTypes.Add(
                new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            json.SerializerSettings.Converters.Add(
                new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        private static void SetupSwagger(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Lifestyle Planning");
            }).EnableSwaggerUi();
        }
    }
}
