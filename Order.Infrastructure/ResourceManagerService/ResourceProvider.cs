using Order.Domain.Common;
using Order.Domain.Services;
using System.Resources;

namespace Order.Infrastructure.ResourceManagerService
{
    public class ResourceProvider : IResourceManager
    {
        public string GetResourceValue<T>(string resourceName, string keyName) where T : class
        {
            ResourceManager resourceManager = new ResourceManager(resourceName, typeof(T).Assembly);
            string? value = resourceManager?.GetString(keyName);
            return (value is null) ? ResourceConstConfig.ResourceManagerFileError : value;



        }
    }
}
