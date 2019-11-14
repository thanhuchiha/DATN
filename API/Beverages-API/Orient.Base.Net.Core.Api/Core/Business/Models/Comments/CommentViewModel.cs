﻿using Orient.Base.Net.Core.Api.Core.Entities;
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
        }

        public Guid Id { get; set; }

        public string Content { get; set; }
    }
}
