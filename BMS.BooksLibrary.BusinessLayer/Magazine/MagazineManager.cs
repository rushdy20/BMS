using BMS.AWS;
using BMS.BooksLibrary.BusinessLayer;
using BMS.BusinessLayer.Magazine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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
        private readonly IErrorLog _errorLog;
        private readonly IFeedBack _feedBack;


        public MagazineManager(IS3Uploader s3Bucket, ICacheManager cacheManager, IErrorLog errorLog, IFeedBack feedBack)
        {
            _s3Bucket = s3Bucket;
            _cacheManager = cacheManager;
            _errorLog = errorLog;
            _feedBack = feedBack;
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
               await _errorLog.Error($"SaveMagazineCategories: {e.Message}");
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
                await _errorLog.Error($"CreateMagazine: {e.Message}");

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
            var  magazineFromS3 = magazineListFromS3.Where(w => w.IsLive).OrderByDescending(o => o.DateCreated).FirstOrDefault();
           

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
            var cachedMagazine = _cacheManager.Get<Models.Magazine>($"{CurrentEditionMagazineCacheKey}_{magazineId}");

            if (cachedMagazine != null) return cachedMagazine;

            var magazineJson = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{MagazineContents}");

            if (string.IsNullOrEmpty(magazineJson))
                return new Models.Magazine();

            var magazineListFromS3 = DeserializeObject<List<Models.Magazine>>(magazineJson);
            var magazineFromS3 = magazineListFromS3.FirstOrDefault(m => m.MagazineId == magazineId);

            var currentEditionContent = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{magazineFromS3?.FolderName}/{MagazineContents}");

            if (!string.IsNullOrWhiteSpace(currentEditionContent))
            {
                magazineFromS3.Contents = DeserializeObject<Models.Magazine>(currentEditionContent).Contents;
            }

            _cacheManager.Set($"{CurrentEditionMagazineCacheKey}_{magazineFromS3.MagazineId}", magazineFromS3);
            return magazineFromS3;
        }

        public async Task<bool> RemoveContentsFromCurrentEdition(string magazineId ,string[] contentIds)
        {
            var currentEdition = await GetMagazine(magazineId);

            var newContents = currentEdition.Contents.Where(c => !contentIds.Contains(c.ContentId)).ToList();
            var removeItems = currentEdition.Contents.Where(c => contentIds.Contains(c.ContentId)).ToList();

            RemoveFilesFromS3(removeItems);
            currentEdition.Contents = newContents;

            var jsonString = JsonSerializer.Serialize(currentEdition);
            
             _cacheManager.Set($"{CurrentEditionMagazineCacheKey}_{currentEdition.MagazineId}", currentEdition);

            return await _s3Bucket.SaveFileAsync($"{MagazineFolder}/{currentEdition.FolderName}/{MagazineContents}", jsonString);
        }

        public async Task<bool> AddMagazineContent(MagazineContent content)
        {
            var getCategories = await GetAllMagazineCategories();
            content.Category = getCategories.FirstOrDefault(c => c.CategoryId == content.Category.CategoryId);

            var currentEdition = await GetMagazine(content.MagazineId); //await GetCurrentEdition();

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
                _cacheManager.Set($"{CurrentEditionMagazineCacheKey}_{magazineFromS3.MagazineId}", magazineFromS3);

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

            var currentMagazine =  await GetCurrentEdition();
            if (currentMagazine.MagazineId == magazineId)
            {
                currentMagazine.IsLive = status;
                _cacheManager.Set(CurrentEditionMagazineCacheKey, currentMagazine);

                var jsonString = JsonSerializer.Serialize(currentMagazine);

                await _s3Bucket.SaveFileAsync($"{MagazineFolder}/{currentMagazine.FolderName}/{MagazineContents}", jsonString);
            }

            var magazineJson = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{MagazineContents}");

            if (string.IsNullOrEmpty(magazineJson))
                return false;

            var magazineListFromS3 = DeserializeObject<List<Models.Magazine>>(magazineJson);
            var magazineToUpdate = magazineListFromS3.SingleOrDefault(m => m.MagazineId == magazineId);
            if (magazineToUpdate != null)
            {
                magazineToUpdate.IsLive = status;
            }

            var nonUpdatedMagazine = magazineListFromS3.Where(m => m.MagazineId != magazineId).ToList();
            nonUpdatedMagazine.Add(magazineToUpdate);

            var jsonStringMagazines = JsonSerializer.Serialize(nonUpdatedMagazine);
            return await _s3Bucket.SaveFileAsync($"{MagazineFolder}/{MagazineContents}", jsonStringMagazines);
        }

        public async Task<string> DownloadFile(string path)
        {
          return await _s3Bucket.DownloadFile($"{MagazineFolder}/{path}");
        }

        public async Task<List<Models.Magazine>> GetAllMagazines()
        {
            var magazineJson = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{MagazineContents}");

            if (string.IsNullOrEmpty(magazineJson))
                return new List<Models.Magazine>();

           return DeserializeObject<List<Models.Magazine>>(magazineJson);
        }

        private async void RemoveFilesFromS3(List<MagazineContent> removedContents)
        {
            foreach (var content in removedContents)
            {
              await _s3Bucket.RemoveFilesFromS3($"{content.FolderName}/{content.MainImage}", MagazineFolder);

              if (!string.IsNullOrWhiteSpace(content.SubImage1))
              {
                  await _s3Bucket.RemoveFilesFromS3($"{content.FolderName}/{content.SubImage1}", MagazineFolder);

              }
              if (!string.IsNullOrWhiteSpace(content.SubImage2))
              {
                  await _s3Bucket.RemoveFilesFromS3($"{content.FolderName}/{content.SubImage2}", MagazineFolder);
              }

              if (!string.IsNullOrWhiteSpace(content.SubImage3))
              {
                  await _s3Bucket.RemoveFilesFromS3($"{content.FolderName}/{content.SubImage3}", MagazineFolder);

              }
              if (!string.IsNullOrWhiteSpace(content.SubImage4))
              {
                  await _s3Bucket.RemoveFilesFromS3($"{content.FolderName}/{content.SubImage4}", MagazineFolder);
              }

              if (!string.IsNullOrWhiteSpace(content.SubImage5))
              {
                  await _s3Bucket.RemoveFilesFromS3($"{content.FolderName}/{content.SubImage5}", MagazineFolder);
              }
            }
        }
    }
}
