namespace Catalogo_Itens.Services
{
    public interface ITaskService
    {
        Task<List<Task>> GetTaskAsync();
        Task<Task> GetTaskByIdAsync(int id);
        Task CreateTaskAsync(Task task);
        Task UpdateTaskAsync(Task task);
        Task DeleteTaskAsync(int id);
    }
}
