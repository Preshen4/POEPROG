using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovelNestLibraryAPI.Models;
using NovelNestLibraryAPI.Services;
using NovelNestLibraryAPI.Tree;

namespace NovelNestLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindCallNumberController : ControllerBase
    {
        private readonly FindCallNumberService _treeService;

        public FindCallNumberController(FindCallNumberService treeService)
        {
            _treeService = treeService;
        }

        [HttpGet]
        public ActionResult<Tree<CallNumberModel>> Get()
        {
            return _treeService.CreateTree();
        }

        [HttpGet("{callNumber}")]
        public ActionResult<List<int>> Get(int callNumber)
        {
            return _treeService.FindCallNumber(callNumber);
        }
    }
}
