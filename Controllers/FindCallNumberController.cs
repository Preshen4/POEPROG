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

        [HttpGet("CreateTree")]
        public void Get()
        {
            _treeService.CreateTree();
        }
        [HttpGet("Quiz")]
        public List<List<CallNumberModel>> GetRandomPath()
        {
            return _treeService.CreateQuiz();
        }
    }
}
