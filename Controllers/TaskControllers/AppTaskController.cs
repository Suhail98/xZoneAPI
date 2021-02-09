using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Repositories.TaskRepo;

namespace xZoneAPI.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppTaskController : Controller
    {
        private ITaskRepository TaskRepo;
        private readonly IMapper mapper;

        public AppTaskController(ITaskRepository _TaskRepo, IMapper _mapper)
        {
            TaskRepo = _TaskRepo;
            mapper = _mapper;
        }

        [HttpGet] 
        public IActionResult GetAppTasks(int userId)
        {
            var taskList = TaskRepo.GetTasks(userId);
            return Ok(taskList);
        }





    }
}
