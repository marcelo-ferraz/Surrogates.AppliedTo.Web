using Surrogates.AppliedTo.Web.Mappers.Classes;
using Surrogates.Expressions.Classes;
using Surrogates.Mappers;
using Surrogates.Mappers.Entities;

namespace Surrogates.AppliedTo.Web.Mappers
{
    public class WebformMapper : BaseMapper
    {
        public string Path { get; protected set; }

        public WebformMapper(MappingState state)
            :base(state) { }

        public override IMappingExpression<T> Throughout<T>(string path = null)
        {
            if (string.IsNullOrEmpty(path))
            { Path = typeof(T).Name.ToAspxPath(); }

            return (IMappingExpression<T>)
                (MapExpression = new WebformMappingExpression<T>(Path, State));
        }
    }
}
