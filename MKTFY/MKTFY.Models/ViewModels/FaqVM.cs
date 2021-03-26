using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// FAQ View Model
    /// </summary>
    public class FaqVM
    {
        /// <summary>
        /// FAQ Constructor which takes in a FAQ Entity with 
        /// </summary>
        /// <param name="data"></param>
        public FaqVM(Faq data)
        {
            Id = data.Id.ToString();
            Title = data.Title;
            Description = data.Description;
        }
        /// <summary>
        /// FaqId Field
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Title Field
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Description Field
        /// </summary>
        public string Description { get; set; }
    }
}
