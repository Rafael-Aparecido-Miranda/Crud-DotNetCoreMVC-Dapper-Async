using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace Catalogo_Itens.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Task>> GetTaskAsync()
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            return (await dbConnection.QueryAsync<Task>("SELECT * FROM Tasks")).ToList();
        }
        public async Task<Task> GetTaskByIdAsync(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<Task>("SELECT * FROM Tasks WHERE Id = @Id", new { Id = id });
        }
        public async Task CreateTaskAsync(Task task)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            const string query = "INSERT INTO Tasks (Description, IsCompleted) VALUES (@Description, @IsCompleted)";
            await dbConnection.ExecuteAsync(query, task);
        }
        public async Task UpdateTaskAsync(Task task)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            const string query = "UPDATE Task SET Description = @Description, IsCompleted WHERE Id = @Id";
            await dbConnection.ExecuteAsync(query, task);
        }
        public async Task DeleteTaskAsync(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            const string query = "DELETE FROM Tasks WHERE Id = @Id";
            await dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}
