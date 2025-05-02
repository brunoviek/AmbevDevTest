namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DynamicFilterAttribute : Attribute
    {
        public Type EntityType { get; }

        public DynamicFilterAttribute(Type entityType)
        {
            EntityType = entityType;
        }
    }
}
