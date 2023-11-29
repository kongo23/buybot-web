using Amazon.S3;

namespace buybot_web.Services
{
    public class S3Service : IS3Service
    {
        private IAmazonS3 _s3Client;

        public S3Service(IAmazonS3 s3Client) {
            _s3Client = s3Client;
        }

        public string GetDownloadFileUrl(string bucketName, string key)
        {
            DateTime expiration = DateTime.UtcNow.AddHours(1);

            return _s3Client.GeneratePreSignedURL(bucketName, key, expiration, new Dictionary<string,object>());
        }
    }
}
