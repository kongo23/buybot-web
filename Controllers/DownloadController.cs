using buybot_web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace buybot_web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadController : ControllerBase
    {
        private IConfiguration _configs;
        private IS3Service _s3Service;

        public DownloadController(
            IConfiguration configs,
            IS3Service s3Service)
        {
            _configs = configs;
            _s3Service = s3Service;
        }

        [HttpGet]
        public async Task<IActionResult> Download(string id)
        {
            //move to secret config
            string secretKey = _configs.GetValue<string>("captcha_secret");

            bool isRecaptchaValid = await VerifyRecaptchaAsync(secretKey, id);

            if (isRecaptchaValid)
            {
                var url = _s3Service.GetDownloadFileUrl("moonbot-free.zip");

                return Ok(new { success = true, url });
            }
            else
            {
                return BadRequest(new { success = false });
            }
        }

        private async Task<bool> VerifyRecaptchaAsync(string secretKey, string userResponse)
        {
            using (var client = new HttpClient())
            {
                var requestData = new Dictionary<string, string>
                {
                    { "secret", secretKey },
                    { "response", userResponse }
                };

                var content = new FormUrlEncodedContent(requestData);

                HttpResponseMessage response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<RecaptchaVerificationResponse>(responseContent, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    return result!.Success;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public class RecaptchaVerificationResponse
    {
        public bool Success { get; set; }
    }
}
