namespace buybot_web.Services
{
    public interface IS3Service
    {
        Task<Stream> DownloadFileAsync(string bucketName, string key);
    }
}
