using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    /// <summary>
    /// Interface FAQ Service
    /// </summary>
    public interface IFaqService
    {
        /// <summary>
        /// Get All FAQ
        /// </summary>
        /// <returns></returns>
        public Task<List<Faq>> GetAllFaq();
        /// <summary>
        /// Get FAQ by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Faq> GetFaq(Guid id);
        /// <summary>
        /// Add FAQ
        /// </summary>
        /// <param name="faq"></param>
        /// <returns></returns>
        public Task<Faq> AddFaq(Faq faq);
        /// <summary>
        /// Edit FAQ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<Faq> EditFaq(Guid id, Faq data);
        /// <summary>
        /// Delete FAQ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteFaq(Guid id);
        
    }
}
