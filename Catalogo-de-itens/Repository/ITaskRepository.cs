namespace Catalogo_Itens.Repository
{
    public interface ITaskRepository
    {
        Task<List<Task>> GetTaskAsync();
        Task<Task> GetTaskByIdAsync(int id);
        Task CreateTaskAsync(Task task);

        Task UpdateTaskAsync(Task task);
        Task DeleteTaskAsync(int id);
    }
}
