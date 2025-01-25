using Amazon.S3;
using Amazon.S3.Model;
using BookWise.Customer.Infrastructure.Buckets.Abstractions;
using BookWise.Customer.Infrastructure.Buckets.Dtos;
using Microsoft.Extensions.Options;

namespace BookWise.Customer.Infrastructure.Buckets.Services;

public class BucketS3Service : IBucketS3Service
{
    private readonly IAmazonS3 _amazonS3;
    private readonly AwsS3Config _amazonS3Config;

    public BucketS3Service(IOptionsMonitor<AwsS3Config> amazonS3Config, IAmazonS3 amazonS3)
    {
        _amazonS3Config = amazonS3Config.CurrentValue;
        _amazonS3 = amazonS3;
    }

    public async Task<string> UploadFileAsync(string filePath, string key, CancellationToken cancellationToken)
    {
        try
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _amazonS3Config.BucketName,
                Key = key,
                FilePath = filePath,
                ContentType = "application/octet-stream"
            };

            var response = await _amazonS3.PutObjectAsync(putRequest, cancellationToken);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK
                ? $"https://{_amazonS3Config.BucketName}.s3.amazonaws.com/{key}"
                : throw new Exception("Falha no upload");
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao fazer upload: {ex.Message}");
        }
    }
}
