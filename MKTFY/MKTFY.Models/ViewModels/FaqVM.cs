using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class FaqVM
    {
        public FaqVM(Faq data)
        {
            Id = data.Id.ToString();
            Title = data.Title;
            Description = data.Description;
        }
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
