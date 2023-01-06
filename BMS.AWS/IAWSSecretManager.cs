using System.Threading.Tasks;

namespace BMS.AWS
{
    public interface IAWSSecretManager
    {
        Task<AWSSecretsModel> GetAWSSecrets(string key);
    }
}