using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Comments
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {

        }

        public CommentViewModel(Comment comment) : this()
        {
            Id = comment.Id;
            Content = comment.Content;
            Username = comment.User.Name;
            UserId = comment.UserId;
            DateComment = comment.CreatedOn;
        }

        public Guid Id { get; set; }

        public string Content { get; set; }

        public Guid UserId { get; set; }

        public string Username { get; set; }

        public DateTime? DateComment { get; set; }
    }
}
