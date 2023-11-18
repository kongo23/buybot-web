namespace buybot_web.Services
{
    public interface IS3Service
    {
        string GetDownloadFileUrl(string bucketName, string key);
    }
}
