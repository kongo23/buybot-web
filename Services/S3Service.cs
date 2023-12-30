using Amazon.CloudFront;

namespace buybot_web.Services
{
    public class S3Service : IS3Service
    {
        public string GetDownloadFileUrl(string key)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(2);
            var signedUrl = GenerateCloudFrontSignedUrl(key, expiration);

            return signedUrl;
        }

        private string GenerateCloudFrontSignedUrl(string key, DateTime expiration)
        {
            var distributionDomainFileUrl = $"https://d183q9r7wb6ukw.cloudfront.net/{key}"; // Replace with your CloudFront distribution domain
            var keyPairId = "KYGQVN396SMBA"; // Replace with your CloudFront key ID
            var privateKeyPath = "private_key.pem"; // Replace with the path to your CloudFront private key

            using (var reader = new StreamReader(privateKeyPath))
            {
                var signedUrl = AmazonCloudFrontUrlSigner.SignUrlCanned(
                    distributionDomainFileUrl,
                    keyPairId,
                    reader,
                    expiration
                );

                Console.WriteLine("Signed URL: " + signedUrl);

                return signedUrl.ToString();
            }
        }
    }
}
