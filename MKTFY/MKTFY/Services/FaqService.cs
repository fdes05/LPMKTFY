using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services
{
    public class FaqService : IFaqService
    {
        private readonly IFaqRepository _faqRepo;

        public FaqService(IFaqRepository faqRepo)
        {
            _faqRepo = faqRepo;
        }

        public async Task<List<Faq>> GetAllFaq()
        {
            return await _faqRepo.GetAll();
        }

        public async Task<Faq> GetFaq(Guid id)
        {            
            return await _faqRepo.Get(id);
        }

        public async Task<Faq> AddFaq(Faq faq)
        {
            return await _faqRepo.Create(faq);
        }

        public async Task<Faq> EditFaq(Guid id, Faq data)
        {            
            var existingFaq = await _faqRepo.Get(id);

            existingFaq.Title = data.Title;
            existingFaq.Description = data.Description;
            var result = await _faqRepo.Edit(existingFaq);

            return result;
        }

        public async Task DeleteFaq(Guid id)
        {            
            await _faqRepo.Delete(id);
        }
        
    }
}
