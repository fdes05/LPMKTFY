using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    public interface IFaqService
    {
        public Task<List<Faq>> GetAllFaq();

        public Task<Faq> GetFaq(Guid id);

        public Task<Faq> AddFaq(Faq faq);

        public Task<Faq> EditFaq(Guid id, Faq data);

        public Task DeleteFaq(Guid id);
        
    }
}
