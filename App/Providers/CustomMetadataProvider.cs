using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Project.App.Providers
{
    public class CustomMetadataProvider : IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            if (context.Key.ModelType == typeof(string))
            {
                context.DisplayMetadata.ConvertEmptyStringToNull = false;
            }
        }
    }
}