using DiaryReactNativeBackend.Mappers;

namespace DiaryReactNativeBackend.ExtensionMethods;

public static class AutoMapperConfigurations
{
    public static void ConfigureAutoMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserMapperProfile).Assembly);
        services.AddAutoMapper(typeof(TopicMapperProfile).Assembly);
        services.AddAutoMapper(typeof(DiaryMapperProfile).Assembly);
    }
}
