using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orient.Base.Net.Core.Api.Core.Business.Filters;
using Orient.Base.Net.Core.Api.Core.Business.IoC;
using Orient.Base.Net.Core.Api.Core.Business.Models;
using Orient.Base.Net.Core.Api.Core.Business.Models.Comments;
using Orient.Base.Net.Core.Api.Core.Business.Services;
using Orient.Base.Net.Core.Api.Core.DataAccess.Repositories.Base;
using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Controllers
{
    [Route("api/comments")]
    [EnableCors("CorsPolicy")]
    [ValidateModel]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly IRepository<Comment> _commentRepository;

        public CommentController(ICommentService commentService, IRepository<Comment> commentRepository)
        {
            _commentService = commentService;
            _commentRepository = commentRepository;
        }

        [HttpGet("{shopId}")]
        public async Task<IActionResult> GetComment(Guid shopId, CommentRequestListViewModel commentRequestListViewModel)
        {
            var list = await _commentService.GetAllCommentByShop(shopId, commentRequestListViewModel);
            return Ok(list);
        }

        [HttpPost("{shopId}")]
        [CustomAuthorize]
        public async Task<IActionResult> PostComment(Guid shopId, [FromBody] CommentManageModel commentManageModel)
        {
            var responseModel = await _commentService.CreateCommentAsync(CurrentUserId, shopId, commentManageModel);
            if (responseModel.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(responseModel.Message);
            }
            else
            {
                return BadRequest(new { Message = responseModel.Message });
            }
        }

        [HttpPut("{shopId}/{commentId}")]
        [CustomAuthorize]
        public async Task<IActionResult> UpdateComment(Guid shopId, Guid commentId, [FromBody] CommentManageModel commentManageModel)
        {
            var commentRepository = IoCHelper.GetInstance<IRepository<Comment>>();
            var comment = commentRepository.GetAll().Where(x => x.Id == commentId && x.UserId == CurrentUserId).FirstOrDefault();
            if (comment == null)
            {
                return StatusCode(403, new { Message = "Forbidden" });
            }

            var responseModel = await _commentService.UpdateCommentAsync(CurrentUserId, shopId, commentId, commentManageModel);
            if (responseModel.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(responseModel.Message);
            }
            else
            {
                return BadRequest(new { Message = responseModel.Message });
            }
        }

        [HttpDelete("{shopId}/{commentId}")]
        [CustomAuthorize]
        public async Task<IActionResult> DeleteComment(Guid shopId, Guid commentId)
        {
            var comment = _commentRepository.GetAll().Where(x => x.Id == commentId && x.UserId == CurrentUserId).FirstOrDefault();
            if (comment == null)
            {
                return StatusCode(403, new { Message = "Forbidden" });
            }

            var responseModel = await _commentService.DeleteCommentAsync(CurrentUserId, shopId, commentId);
            if (responseModel.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(responseModel.Message);
            }
            else
            {
                return BadRequest(new { Message = responseModel.Message });
            }
        }

    }
}
