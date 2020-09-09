using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace BMS.AWS
{
    public class S3Uploader : IS3Uploader
    {
        private const string _bucketName = "bmsweb";
        private static readonly RegionEndpoint _bucketRegion = RegionEndpoint.USEast2;
        private static IAmazonS3 _s3Client;

        public S3Uploader()
        {
           _s3Client = new AmazonS3Client(_bucketRegion);
        }

        public async Task<bool> UploadFileAsync(string fileName, Stream storageStream)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("File name must be specified.");

            try
            {
                using (var client = new AmazonS3Client(_bucketRegion))
                {
                    if (storageStream.Length > 0)
                        if (storageStream.CanSeek)
                            storageStream.Seek(0, SeekOrigin.Begin);

                    var request = new PutObjectRequest
                    {
                        AutoCloseStream = true,
                        BucketName = _bucketName,
                        InputStream = storageStream,
                        Key = fileName
                    };
                    var response = await client.PutObjectAsync(request).ConfigureAwait(false);
                    return response.HttpStatusCode == HttpStatusCode.OK;
                }
            }
            catch (AmazonS3Exception ex)
            {
                return false;
                //ignore
            }
        }

        public async Task<bool> SaveFileAsync(string fileName, string storageStream)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("File name must be specified.");

            try
            {
                using (var client = new AmazonS3Client(_bucketRegion))
                {
                    var request = new PutObjectRequest
                    {
                        AutoCloseStream = true,
                        BucketName = _bucketName,
                        ContentBody = storageStream,
                        Key = fileName
                    };
                    var response = await client.PutObjectAsync(request).ConfigureAwait(false);
                    return response.HttpStatusCode == HttpStatusCode.OK;
                }
            }
            catch (AmazonS3Exception ex)
            {
                //ignore
                return false;
            }
        }

        public async Task<string> GetFileFromS3(string filename)
        {
            try
            {
                var requestObject = new GetObjectRequest
                {
                    BucketName = _bucketName,
                    Key = filename
                };

                using (var response = await _s3Client.GetObjectAsync(requestObject))
                using (var responseStream = response.ResponseStream)
                using (var reader = new StreamReader(responseStream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (AmazonS3Exception ex)
            {
                //ignore
            }

            return string.Empty;
        }

        public string ReadFile(string fullName)
        {
            var path = Path.Combine("wwwroot", "supportFiles", fullName);
            var jsonString = File.ReadAllText(path);
            return jsonString;
        }

        public string SaveToFile(string fullName, string content)
        {
            var path = Path.Combine("wwwroot", "supportFiles", fullName);
            File.WriteAllText(path, content);
            return string.Empty;
        }
    }
}