using System.IO;
using System.Threading.Tasks;

namespace BMS.AWS
{
    public interface IS3Uploader
    {
        Task<bool> UploadFileAsync(string fileName, Stream storageStream);
        Task<bool> SaveFileAsync(string fileName, string storageStream);

        Task<string> GetFileFromS3(string filename);

        string ReadFile(string fullName);
        string SaveToFile(string fullName, string content);

        Task<string> DownloadFile(string path);

        Task<bool> RemoveFilesFromS3(string fileName, string bucketName);

    }
}