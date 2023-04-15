using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ApiVersionControl;
public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "My .Net Api resful",
            Version = description.ApiVersion.ToString(),
            Description = "This is my first Api Versioning control",
            Contact = new OpenApiContact()
            {
                Email = "hilares33v@gmail.com",
                Name = "Antonio"
            }
        };
        if (description.IsDeprecated)
        {
            info.Description += "This API version has been deprecated";
        }
        return info;

    }
    public void Configure(SwaggerGenOptions options)
    {
        // Add Swagger Documentation for each API version we have
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }
    }
    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }
}
