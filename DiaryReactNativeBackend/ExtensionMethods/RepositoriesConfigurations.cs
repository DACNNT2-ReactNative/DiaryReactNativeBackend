using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Logics.Implementations;
using DiaryReactNativeBackend.Repositories.Abstractions;
using DiaryReactNativeBackend.Repositories.Implementations;

namespace DiaryReactNativeBackend.ExtensionMethods;

public static class RepositoriesConfigurations
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITopicRepository, TopicRepository>();
        services.AddScoped<IDiaryRepository, DiaryRepository>();
        services.AddScoped<IDeviceRepository, DeviceRepository>();

        services.AddScoped<IUserLogic, UserLogic>(); 
        services.AddScoped<ITopicLogic, TopicLogic>();
        services.AddScoped<IDiaryLogic, DiaryLogic>();
        services.AddScoped<IDeviceLogic, DeviceLogic>();
    }
}
