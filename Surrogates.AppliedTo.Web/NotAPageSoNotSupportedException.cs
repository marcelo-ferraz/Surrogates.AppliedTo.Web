using System;

namespace Surrogates.AppliedTo.Web
{
    public class NotAPageSoNotSupportedException: Exception
    {
        public NotAPageSoNotSupportedException(Type type)
            :base(string.Format("The given type: '{0}' is not a webform page, therefore not supported for this Container", type)) { }
    }
}
