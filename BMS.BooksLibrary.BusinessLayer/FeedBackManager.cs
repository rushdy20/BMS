using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BMS.AWS;
using BMS.BusinessLayer.Models;
using static Newtonsoft.Json.JsonConvert;
namespace BMS.BusinessLayer
{
    public class FeedBackManager : IFeedBack
    {
        private const string MagazineFolder = @"magazine";
        private const string FeedbackFile = "feedBack.json";

        private readonly IS3Uploader _s3Bucket;

        public FeedBackManager(IS3Uploader s3Bucket)
        {
            _s3Bucket = s3Bucket;
        }

        public async Task<bool> CreateFeedBack(FeedbackModel feedback)
        {
            var errorLogs = new List<FeedbackModel>();

            try
            {
                var magazineCategoryJson = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{FeedbackFile}");

                if (!string.IsNullOrEmpty(magazineCategoryJson))
                {
                    errorLogs = DeserializeObject<List<FeedbackModel>>(magazineCategoryJson);
                }

                errorLogs.Add(feedback);

                var jsonString = JsonSerializer.Serialize(errorLogs);

                return await _s3Bucket.SaveFileAsync($"{MagazineFolder}/{FeedbackFile}", jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<FeedbackModel>> ReadFeedBak()
        {
            var magazineCategoryJson = await _s3Bucket.GetFileFromS3($"{MagazineFolder}/{FeedbackFile}");

            if (!string.IsNullOrEmpty(magazineCategoryJson))
            {
                return DeserializeObject<List<FeedbackModel>>(magazineCategoryJson);
            }

            return new List<FeedbackModel>();
        }
    }
}
