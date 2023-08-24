using Catalogo_Itens.Repository;
using System.Data;

namespace Catalogo_Itens.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<List<Task>> GetTaskAsync()
        {
            return await _taskRepository.GetTaskAsync();
        }
        public async Task<Task> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetTaskByIdAsync(id);
        }
        public async Task CreateTaskAsync(Task task)
        {
            await _taskRepository.CreateTaskAsync(task);
        }
        public async Task UpdateTaskAsync(Task task)
        {
            await _taskRepository.UpdateTaskAsync(task);
        }
        public async Task DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteTaskAsync(id);
        }
    }
}
