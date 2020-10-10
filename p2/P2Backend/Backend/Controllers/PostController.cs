using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Repo.Repo.IRepo;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
  [Route("api/post")]
  [ApiController]
  public class PostController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public PostController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    [Produces("application/json")]
    [HttpGet("find")]
    public async Task<ActionResult<Post>> Find()
    {
      return await _unitOfWork.Post.GetAsync(1);
    }

    [Produces("application/json")]
    [HttpGet("findall")]
    public async Task<ActionResult<List<Post>>> FindAll()
    {
      return await _unitOfWork.Post.GetAllAsync();
    }

  }
}
