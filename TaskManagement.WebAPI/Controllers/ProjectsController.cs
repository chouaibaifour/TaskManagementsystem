using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Projects.Contracts;
using TaskManagement.Application.Projects.UseCase;

namespace TaskManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(ProjectService projectService) : ControllerBase
    {
        private readonly ProjectService _projectService = projectService;

        // GET: api/<ProjectsController>
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var result = await _projectService.GetAllProjectsAsync();
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        
        // GET: api/<ProjectsController>
        [HttpGet("{OwnerId :guid}/ProjectsPerOwner")]
        public async Task<IActionResult> GetProjectsByOwnerId(Guid ownerId)
        {
            var result = await _projectService.GetProjectsByOwnerIdAsync(ownerId);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
