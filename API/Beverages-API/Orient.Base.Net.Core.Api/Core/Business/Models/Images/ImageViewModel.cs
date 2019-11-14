using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Images
{
    public class ImageViewModel
    {
        public ImageViewModel()
        {

        }
        
        public ImageViewModel(Image image): this()
        {
            Id = image.Id;
            Url = image.Url;
        }

        public Guid Id { get; set; }

        public string Url { get; set; }
    }
}
