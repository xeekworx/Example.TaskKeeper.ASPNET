using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskKeeper.Domain;
using TaskKeeper.Persistence.Repositories;
using TaskKeeper.Web.Models;

namespace TaskKeeper.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ILogger<TaskItemController> _logger;
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;

        public TaskItemController(
            ILogger<TaskItemController> logger,
            ITaskItemRepository taskItemRepository,
            IMapper mapper
        )
        {
            _logger = logger;
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskItem>> Search(
            [FromQuery] string? containsTitle = default,
            [FromQuery] bool? completed = default
        )
        {
            List<TaskItem> items;

            items = await _taskItemRepository.SearchAsync(
                containsTitle, exactTitle: false,
                isCompleted: completed
            );

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetById(Guid id)
        {
            var taskItem = await _taskItemRepository.GetByIdAsync(id);

            return taskItem != null ? taskItem : NotFound();
        }

        [HttpPost]
        public async Task<TaskItem> Create(CreateTaskItemRequest request)
        {
            TaskItem taskItem = _mapper.Map<TaskItem>(request);
            await _taskItemRepository.AddAsync(taskItem);
            return taskItem;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskItem>> DeleteById(Guid id)
        {
            var item = await _taskItemRepository.DeleteAsync(id);

            return item != null ? Ok(item) : NotFound();
        }

        [HttpPatch("{id}/complete")]
        public async Task<ActionResult<TaskItem>> CompleteTask(Guid id)
        {
            var item = await _taskItemRepository.CompleteTaskAsync(id);

            return item != null ? Ok(item) : NotFound();
        }

        [HttpPatch("{id}/incomplete")]
        public async Task<ActionResult<TaskItem>> ResetTask(Guid id)
        {
            var item = await _taskItemRepository.ResetTaskAsync(id);

            return item != null ? Ok(item) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItem>> UpdateTask(Guid id, UpdateTaskItemRequest request)
        {
            TaskItem taskItem = _mapper.Map<TaskItem>(request);
            taskItem.Id = id;

            var success = await _taskItemRepository.UpdateAsync(taskItem);

            return success ? Ok(taskItem) : NotFound();
        }
    }
}