using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using Surrogates.Mappers;

namespace Surrogates.AppliedTo.Web
{
    public class WebformHandlerFactory : PageHandlerFactory
    {
        private static SurrogatedWebformsContainer _container;

        static WebformHandlerFactory()
        {
            _container = new 
                SurrogatedWebformsContainer();
        }

        public static SurrogatedWebformsContainer Map(Action<IMapper> mapping)
        {
            return _container.Map(mapping);
        }

        public override IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
        {
            if (!_container.Has(key: virtualPath.ToLower()))
            { return base.GetHandler(context, requestType, virtualPath, path); }

            return _container
                    .Handle(virtualPath, entry =>
                    {
                        var p = base
                            .GetHandler(context, requestType, virtualPath, path) as Page;

                        if (p == null)
                        { throw new NotAPageSoNotSupportedException(p.GetType()); }

                        entry.AppRelativeVirtualPath = p.AppRelativeVirtualPath;
                        entry.AppRelativeTemplateSourceDirectory = p.AppRelativeTemplateSourceDirectory;
                    });
        }
    }
}
