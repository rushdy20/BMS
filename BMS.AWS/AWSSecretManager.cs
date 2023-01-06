using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace BMS.AWS
{
    public class AWSSecretManager : IAWSSecretManager
    {
        //private readonly IAmazonSecretsManager _secretsManager;

        //public AWSSecretManager(IAmazonSecretsManager secretsManager)
        //{
        //    //    _secretsManager = secretsManager;

        //}

        public async Task<AWSSecretsModel> GetAWSSecrets(string key)
        {
            //var response = _secretsManager.GetSecretValueAsync(new GetSecretValueRequest
            //{
            //    SecretId = $"{key}"
            //}).Result;

            //var secretsData = JsonConvert.DeserializeObject<AWSSecretsModel>(response.SecretString);

            //return secretsData;

            return new AWSSecretsModel();
        }
    }
}