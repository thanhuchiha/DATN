using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Images
{
    public class ImageManageModel
    {
        public string Url { get; set; }

        public void SetDataToModel(Image image)
        {
            image.Url = Url;
        }
    }
}
