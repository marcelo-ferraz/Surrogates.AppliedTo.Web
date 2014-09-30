using System;
using System.Web;
using System.Web.UI;
using Surrogates.AppliedTo.Web.Mappers;
using Surrogates.AppliedTo.Web.Model;
using Surrogates.Mappers;
using Surrogates.Mappers.Entities;

namespace Surrogates.AppliedTo.Web
{
    public class SurrogatedWebformsContainer : BaseContainer4Surrogacy<WebformEntry, WebformMapper>
    {
        protected override void AddMap(WebformMapper mappingExp, Type type)
        {
            Dictionary.Add(mappingExp.Path.ToLower(), 
                new WebformEntry { Type = type });
        }

        public SurrogatedWebformsContainer Map(Action<IMapper> mapping)
        {
            base.InternalMap(mapping);
            return this;
        }

        public IHttpHandler Handle(string path, Action<WebformEntry> onFirstAttempt)
        {
            if (string.IsNullOrEmpty(path))
            { throw new ArgumentNullException("path"); }

            var entry = 
                Dictionary[path.ToLower()];

            if (string.IsNullOrEmpty(entry.AppRelativeVirtualPath))
            {
                lock (entry)
                { onFirstAttempt(entry); }
            }

            var page = (Page)Activator.CreateInstance(
                Dictionary[path.ToLower()].Type);

            page.AppRelativeVirtualPath = 
                entry.AppRelativeVirtualPath;
            
            page.AppRelativeTemplateSourceDirectory = 
                entry.AppRelativeTemplateSourceDirectory;

            return page;
        }
    }
}
