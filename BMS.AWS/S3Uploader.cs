using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Syroot.Windows.IO;

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

        public async Task<string> DownloadFile(string path)
        {
            using var client =  new AmazonS3Client(_bucketRegion);
            try
            {
                GetObjectRequest request = new GetObjectRequest();
                request.BucketName = _bucketName;
                request.Key = path;

                var fileTransferUtility = new TransferUtility(client);

                var fileName = path.Split('/').Last();

                string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string pathDownload = Path.Combine(pathUser, "TheSoulJournal");

                if(! Directory.Exists(pathDownload))

                   Directory.CreateDirectory(pathDownload);

                // var downloadsPath = new KnownFolder(KnownFolderType.Downloads).Path;

                var fs = File.Create($"{pathDownload}/{fileName}");
                fs.Close();

                fileTransferUtility.Download($"{pathDownload}/{fileName}", _bucketName, path);

              //  WebClient webClient = new WebClient();
              //  await webClient.DownloadFileAsync(pathDownload, fileName);

                return pathDownload;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<bool> RemoveFilesFromS3(string key, string bucketName)
        {
            try
            {
                var delete = await _s3Client.DeleteObjectAsync(bucketName, key);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<string> ErrorLog(string errorMsg)
        {
            throw new NotImplementedException();
        }
    }
}