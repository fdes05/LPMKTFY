using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services
{
    /// <summary>
    /// FAQ Service
    /// </summary>
    public class FaqService : IFaqService
    {
        private readonly IFaqRepository _faqRepo;
        /// <summary>
        /// FAQ Service Constructor takes in a IFaqRepository
        /// </summary>
        /// <param name="faqRepo"></param>
        public FaqService(IFaqRepository faqRepo)
        {
            _faqRepo = faqRepo;
        }
        /// <summary>
        /// Get All FAQs
        /// </summary>
        /// <returns></returns>
        public async Task<List<Faq>> GetAllFaq()
        {
            return await _faqRepo.GetAll();
        }
        /// <summary>
        /// Get FAQ
        /// </summary>
        /// <param name="id">Provide FaqId</param>
        /// <returns></returns>
        public async Task<Faq> GetFaq(Guid id)
        {            
            return await _faqRepo.Get(id);
        }
        /// <summary>
        /// Add FAQ
        /// </summary>
        /// <param name="faq">Requires a FAQ Entity</param>
        /// <returns></returns>
        public async Task<Faq> AddFaq(Faq faq)
        {
            return await _faqRepo.Create(faq);
        }
        /// <summary>
        /// Edit FAQ
        /// </summary>
        /// <param name="id">Requires FaqId</param>
        /// <param name="data">Requires FAQ entity with updated info</param>
        /// <returns></returns>
        public async Task<Faq> EditFaq(Guid id, Faq data)
        {            
            var existingFaq = await _faqRepo.Get(id);

            existingFaq.Title = data.Title;
            existingFaq.Description = data.Description;
            var result = await _faqRepo.Edit(existingFaq);

            return result;
        }
        /// <summary>
        /// Delete FAQ
        /// </summary>
        /// <param name="id">Requires FaqId to be deleted</param>
        /// <returns></returns>
        public async Task DeleteFaq(Guid id)
        {            
            await _faqRepo.Delete(id);
        }
        
    }
}
