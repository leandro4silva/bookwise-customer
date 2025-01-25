namespace BookWise.Customer.Infrastructure.Buckets.Abstractions;

public interface IBucketS3Service
{
    Task<string> UploadFileAsync(string filePath, string key, CancellationToken cancellationToken);
}
