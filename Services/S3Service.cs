using Amazon.S3;

namespace buybot_web.Services
{
    public class S3Service : IS3Service
    {
        private IAmazonS3 _s3Client;
        public S3Service(IAmazonS3 s3Client) {
            _s3Client = s3Client;
        }

        public async Task<Stream> DownloadFileAsync(string bucketName, string key)
        {
            var response = await _s3Client.GetObjectAsync(bucketName, key);
            return response.ResponseStream;
        }
    }
}
