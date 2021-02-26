using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BMS.AWS;
using BMS.BooksLibrary.BusinessLayer;
using BMS.BusinessLayer.Magazine.Models;
using static Newtonsoft.Json.JsonConvert;

namespace BMS.BusinessLayer.Magazine
{
  public  class MagazineManager : IMagazineManager
    {
        private const string MagazineFolder = @"magazine";
        private const string MagazineCategoryFile = "magazineCategories.json";
        private const string MagazineContents = "magazine.json";
        private const string MagazineCategoryCacheKey = "MagazineCategories";
        private const string CurrentEditionMagazineCacheKey = "CurrentEditionMagazineCacheKey";

        private readonly IS3Uploader _s3Bucket;
        private readonly ICacheManager _cacheManager;


        public MagazineManager(IS3Uploader s3Bucket, ICacheManager cacheManager)
        {
            _s3Bucket = s3Bucket;
            _cacheManager = cacheManager;
        }
        public async Task<List<MagazineCategory>> GetAllMagazineCategories()
        {
            var cachedMagazineCategories = _cacheManager.Get<List<MagazineCategory>>(MagazineCategoryCacheKey);

            if (cachedMagazineCategories != null && cachedMagazineCategories.Any()) return cachedMagazineCategories;

            var magazineCategoryJson = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{MagazineCategoryFile}");
            
            if (string.IsNullOrEmpty(magazineCategoryJson))
                return new List<MagazineCategory>();

            var categoriesFromS3 = DeserializeObject<List<MagazineCategory>>(magazineCategoryJson);

            _cacheManager.Set(MagazineCategoryCacheKey, categoriesFromS3);

            return categoriesFromS3 ?? new List<MagazineCategory>();

        }

        public async Task<bool> SaveMagazineCategories(List<MagazineCategory> magazineCategories)
        {
            var jsonString = JsonSerializer.Serialize(magazineCategories);
            try
            {
                _cacheManager.Set(MagazineCategoryCacheKey, magazineCategories);

                return await _s3Bucket.SaveFileAsync($"{MagazineFolder}/{MagazineCategoryFile}", jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> SaveImageFile(Stream contentStream, string folder, string fileName)
        {
            var result = await _s3Bucket.UploadFileAsync($@"{MagazineFolder}/{folder}/{fileName}", contentStream)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<bool> CreateMagazine(Models.Magazine magazine)
        {
            var magazineJson = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{MagazineContents}");

            var magazineFromS3 = new List<Models.Magazine>();

            if (!string.IsNullOrEmpty(magazineJson))
            {
                magazineFromS3 = DeserializeObject<List<Models.Magazine>>(magazineJson);
            }

            magazineFromS3.Add(magazine);


            var jsonString = JsonSerializer.Serialize(magazineFromS3);

            try
            {
                _cacheManager.Set(CurrentEditionMagazineCacheKey, magazine);

                return await _s3Bucket.SaveFileAsync($"{MagazineFolder}/{MagazineContents}", jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Models.Magazine> GetCurrentEdition()
        {
            var cachedMagazine = _cacheManager.Get<Models.Magazine>(CurrentEditionMagazineCacheKey);

            if (cachedMagazine != null ) return cachedMagazine;

            var magazineJson = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{MagazineContents}");

            if (string.IsNullOrEmpty(magazineJson))
                return new Models.Magazine();

            var magazineListFromS3 = DeserializeObject<List<Models.Magazine>>(magazineJson);
            var  magazineFromS3 = magazineListFromS3.OrderByDescending(o => o.DateCreated).FirstOrDefault();

            if (magazineFromS3 == null) 
                return new Models.Magazine();

            var currentEditionContent = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{magazineFromS3?.FolderName}/{MagazineContents}");

            if (!string.IsNullOrWhiteSpace(currentEditionContent))
            {
                magazineFromS3.Contents = DeserializeObject<Models.Magazine>(currentEditionContent).Contents;
            }

            _cacheManager.Set(CurrentEditionMagazineCacheKey, magazineFromS3);

            return magazineFromS3 ?? new Models.Magazine();
        }

        public async Task<Models.Magazine> GetMagazine(string magazineId)
        {
            var magazineJson = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{MagazineContents}");

            if (string.IsNullOrEmpty(magazineJson))
                return new Models.Magazine();

            var magazineListFromS3 = DeserializeObject<List<Models.Magazine>>(magazineJson);
            return magazineListFromS3.FirstOrDefault(m => m.MagazineId == magazineId);
        }

        public async Task<bool> RemoveContentsFromCurrentEdition(string[] contentIds)
        {
            var currentEdition = await GetCurrentEdition();

            var newContents = currentEdition.Contents.Where(c => !contentIds.Contains(c.ContentId)).ToList();
            currentEdition.Contents = newContents;

            var jsonString = JsonSerializer.Serialize(currentEdition);
            
             _cacheManager.Set(CurrentEditionMagazineCacheKey, currentEdition);

            return await _s3Bucket.SaveFileAsync($"{MagazineFolder}/{currentEdition.FolderName}/{MagazineContents}", jsonString);
        }

        public async Task<bool> AddMagazineContent(MagazineContent content)
        {
            var getCategories = await GetAllMagazineCategories();
            content.Category = getCategories.FirstOrDefault(c => c.CategoryId == content.Category.CategoryId);

            var currentEdition = await GetCurrentEdition();

            var magazineJson = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{content.FolderName}/{MagazineContents}");

            var magazineFromS3 = new Models.Magazine();
            
            if (!string.IsNullOrEmpty(magazineJson))
            {
                magazineFromS3 = DeserializeObject<Models.Magazine>(magazineJson);
            }

            if (magazineFromS3.Contents == null)
            {
                magazineFromS3.Contents = new List<MagazineContent>();
            }

            magazineFromS3.Contents.Add(content);
            magazineFromS3.Name = currentEdition.Name;
            magazineFromS3.FolderName = currentEdition.FolderName;
            magazineFromS3.Image = currentEdition.Image;
            magazineFromS3.MagazineId = currentEdition.MagazineId;


            var jsonString = JsonSerializer.Serialize(magazineFromS3);

            try
            {
                _cacheManager.Set(CurrentEditionMagazineCacheKey, magazineFromS3);

                return await _s3Bucket.SaveFileAsync($"{MagazineFolder}/{content.FolderName}/{MagazineContents}", jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> UpdateMagazineStatus(bool status, string magazineId)
        {
            var currentMagazine = await GetCurrentEdition();
            if (currentMagazine.MagazineId == magazineId)
            {
                currentMagazine.IsLive = status;
                _cacheManager.Set(CurrentEditionMagazineCacheKey, currentMagazine);

                var jsonString = JsonSerializer.Serialize(currentMagazine);

               return await _s3Bucket.SaveFileAsync($"{MagazineFolder}/{currentMagazine.FolderName}/{MagazineContents}", jsonString);
            }

            var magazineJson = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{MagazineContents}");

            if (string.IsNullOrEmpty(magazineJson))
                return false;

            var magazineListFromS3 = DeserializeObject<List<Models.Magazine>>(magazineJson);
            var magazineFromS3 = magazineListFromS3.SingleOrDefault(m => m.MagazineId == magazineId);
            if (magazineFromS3 != null)
            {
                magazineFromS3.IsLive = status;
            }

            var jsonStringMagazines = JsonSerializer.Serialize(currentMagazine);
            return await _s3Bucket.SaveFileAsync($"{MagazineFolder}/{MagazineContents}", jsonStringMagazines);
        }

        public async Task<string> DownloadFile(string path)
        {
          return await _s3Bucket.DownloadFile($"{MagazineFolder}/{path}");
        }
    }
}
