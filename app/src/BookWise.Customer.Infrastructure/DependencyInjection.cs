using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using Amazon.SQS;
using BookWise.Customer.Domain.Repositories;
using BookWise.Customer.Infrastructure.Buckets.Abstractions;
using BookWise.Customer.Infrastructure.Buckets.Dtos;
using BookWise.Customer.Infrastructure.Buckets.Services;
using BookWise.Customer.Infrastructure.Configurations;
using BookWise.Customer.Infrastructure.LogAudit.Abstractions;
using BookWise.Customer.Infrastructure.LogAudit.Dtos;
using BookWise.Customer.Infrastructure.LogAudit.Services;
using BookWise.Customer.Infrastructure.MessageBus.Abstraction;
using BookWise.Customer.Infrastructure.MessageBus.Clients;
using BookWise.Customer.Infrastructure.MessageBus.Event;
using BookWise.Customer.Infrastructure.Notifications;
using BookWise.Customer.Infrastructure.Notifications.Abstraction;
using BookWise.Customer.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookWise.Customer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services, AppConfiguration appConfiguration)
    {
        AddDynamoDb(services);
        AddS3Bucket(services);

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
    public static IServiceCollection AddAwsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAWSService<IAmazonSQS>();
        services.AddAWSService<IAmazonS3>();

        services.AddSingleton<IPublisher, AwsSQSClient>();
        services.AddScoped<IBucketS3Service, BucketS3Service>();
        services.AddScoped<IEventProcessor, EventProcessor>();
        services.AddScoped<ILogAuditService, LogAuditService>();

        services.Configure<CreateCustomerSqsConfig>(configuration.GetSection(nameof(CreateCustomerSqsConfig)));
        services.Configure<AuditoriaConfig>(configuration.GetSection(nameof(AuditoriaConfig)));
        services.Configure<UserImageConfig>(configuration.GetSection(nameof(UserImageConfig)));
        services.Configure<AwsS3Config>(configuration.GetSection(nameof(AwsS3Config)));

        return services;
    }

    private static IServiceCollection AddDynamoDb(this IServiceCollection services)
    {
        services.AddScoped<IAmazonDynamoDB, AmazonDynamoDBClient>();
        services.AddScoped<IDynamoDBContext, DynamoDBContext>();

        return services;
    }

    private static IServiceCollection AddS3Bucket(this IServiceCollection services)
    {
        services.AddScoped<IAmazonS3, AmazonS3Client>();

        return services;
    }
}
