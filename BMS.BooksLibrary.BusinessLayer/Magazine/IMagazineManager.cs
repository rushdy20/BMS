using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BMS.BusinessLayer.Magazine.Models;

namespace BMS.BusinessLayer.Magazine
{
    public interface IMagazineManager
    {
        Task<List<MagazineCategory>> GetAllMagazineCategories();

        Task<bool> SaveMagazineCategories(List<MagazineCategory> magazineCategories);

        Task<bool> SaveImageFile(Stream contentStream, string folder, string fileName);

        Task<bool> CreateMagazine(Models.Magazine magazine);

        Task<Models.Magazine> GetCurrentEdition();

        Task<Models.Magazine> GetMagazine(string magazineId);

        Task<bool> RemoveContentsFromCurrentEdition(string[] contentIds);

        Task<bool> AddMagazineContent(MagazineContent content);

        Task<bool> UpdateMagazineStatus(bool status, string magazineId);

        Task<string> DownloadFile(string path);
    }
}