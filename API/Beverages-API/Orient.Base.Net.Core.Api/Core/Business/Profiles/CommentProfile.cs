﻿using AutoMapper;
using Orient.Base.Net.Core.Api.Core.Business.Models;
using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentManageModel>().ReverseMap();
        }
    }
}
