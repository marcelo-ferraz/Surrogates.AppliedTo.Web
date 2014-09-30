
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surrogates.AppliedTo.Web.Model
{
    public class WebformEntry
    {
        public Type OriginalType { get; set; }
        public Type Type { get; set; }
        public string AppRelativeVirtualPath { get; set; }
        public string AppRelativeTemplateSourceDirectory { get; set; } 
    }
}
