using System;
using System.Reflection;
using System.Web.Compilation;
using Surrogates.Expressions.Classes;
using Surrogates.Mappers.Entities;

namespace Surrogates.AppliedTo.Web.Mappers.Classes
{
    public class WebformMappingExpression<T> : ClassMappingExpression<T>
    {
        private static MethodInfo _typeGetter;

        static WebformMappingExpression()
        {
            _typeGetter = Type.GetType(
                "System.Web.Util.ITypedWebObjectFactory, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")
                .GetProperty("InstantiatedType", BindingFlags.Instance | BindingFlags.Public)
                .GetGetMethod();
        }

        public WebformMappingExpression(string path, MappingState State)
            : base(path, State) { }

        protected override void CreateProxy(string path, MappingState state)
        {
            var factory =
                    BuildManager.GetObjectFactory(path, false);

            if (factory == null)
            {
                throw new EntryPointNotFoundException(string.Concat(
                    "Could not resolve the provided path: '", path.ToClassName(), "' !"));
            }

            var instanceType =
                (Type)_typeGetter.Invoke(factory, null);

            try
            {
                State.TypeBuilder = this.State.ModuleBuilder.DefineType(
                    instanceType.Name, TypeAttributes.Public, instanceType);
            }
            catch (ArgumentException argEx)
            {
                throw new ProxyAlreadyMadeException(instanceType, path, argEx);
            }
        }
    }
}
