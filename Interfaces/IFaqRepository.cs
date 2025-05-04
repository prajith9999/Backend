using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;

namespace LandWind.Interfaces
{
    public interface IFaqHandler
    {
        Task<List<Faq>> GetAllFaqs();
        Task<Faq> GetFaqById(int id);
        Task<Faq> CreateFaq(Faq faq);
        Task<Faq> UpdateFaq(int id, Faq faq);
        Task<bool> DeleteFaq(int id);
    }
}
