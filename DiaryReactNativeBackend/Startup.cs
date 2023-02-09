using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using CorePush.Apple;
using CorePush.Google;
using DiaryReactNativeBackend.CronJob;
using DiaryReactNativeBackend.ExtensionMethods;
using DiaryReactNativeBackend.Model;
using DiaryReactNativeBackend.Services.Abstractions;
using DiaryReactNativeBackend.Services.Implementations;
using DiaryReactNativeBackend.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;

namespace DiaryReactNativeBackend;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        var awsOptions = Configuration.GetAWSOptions();

        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonDynamoDB>();

        services.AddScoped<IDynamoDBContext, DynamoDBContext>();

        services.AddCors();

        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionScopedJobFactory();
            var jobKey = new JobKey("ScheduleNotification");
            q.AddJob<ScheduleNotification>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("ScheduleNotification")
                .WithCronSchedule("0 40 19 * * ?"));

        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        services.Configure<FormOptions>(options =>
        {
            // Set the limit file to 1 MB
            options.MultipartBodyLengthLimit = 1000000;
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

        services.AddAWSService<IAmazonS3>();

        services.AddScoped<IStorageService, StorageService>();
        services.AddTransient<IMailService,MailService>();


        services.AddControllers();
        services.AddHttpClient<FcmSender>();
        services.AddHttpClient<ApnSender>();

        var appSettingsSection = Configuration.GetSection("FcmNotification");
        services.Configure<FcmNotificationSetting>(appSettingsSection);
        services.Configure<MailSetting>(Configuration.GetSection("MailSettings"));

        services.AddAuthorization();

        services.ConfigureRepositories();
        services.ConfigureAutoMappers();
        services.ConfigureSwagger();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Diary API V1");
        });

        app.UseHttpsRedirection();

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}