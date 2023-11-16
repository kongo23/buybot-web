using Amazon.S3;
using buybot_web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAWSService<IAmazonS3>(builder.Configuration.GetAWSOptions());
builder.Services.AddSingleton<IS3Service, S3Service>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDefaultFiles();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();