using Orient.Base.Net.Core.Api.Core.Common.Constants;
using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models
{
    public class CommentManageModel : IValidatableObject
    {
        public CommentManageModel()
        {

        }

        [Required]
        public string Content { get; set; }

        public void SetDataToModel(Comment comment)
        {
            comment.Content = Content;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Content.Trim() == null)
            {
                yield return new ValidationResult(CommentMessageConstants.INVALID_CONTENT, new string[] { "Content" });
            }
        }

    }
}
