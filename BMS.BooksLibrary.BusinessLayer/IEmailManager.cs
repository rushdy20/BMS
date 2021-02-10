using System.Threading.Tasks;

namespace BMS.BusinessLayer
{
    public interface IEmailManager
    {
        Task<bool> SendEmail(string to, string message, string subject);
        Task<bool> SendEmail(string to, string from, string message, string subject);
    }
}