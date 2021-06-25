using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BMS.AWS;
using BMS.BusinessLayer.Magazine.Models;
using static Newtonsoft.Json.JsonConvert;
namespace BMS.BusinessLayer
{
  public  class ErrorLog : IErrorLog
    {
        private const string MagazineFolder = @"magazine";
        private const string LogFile = "logFile.json";
        

        private readonly IS3Uploader _s3Bucket;

        public ErrorLog(IS3Uploader s3Bucket)
        {
            _s3Bucket = s3Bucket;
        }
        public async Task<bool> Error(string errorMsg)
        {
           
            var errorLogs = new List<string>();

            try
            {
                var magazineCategoryJson = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{LogFile}");

                if (!string.IsNullOrEmpty(magazineCategoryJson))
                {
                    errorLogs = DeserializeObject<List<string>>(magazineCategoryJson);
                }

                errorLogs.Add(errorMsg);

                var jsonString = JsonSerializer.Serialize(errorLogs);

                return await _s3Bucket.SaveFileAsync($"{MagazineFolder}/{LogFile}", jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
