using Dapper;
using SharedModels.AuthenticationModels;
using System.Data;

namespace AuthenticationProject.Repository
{
    public interface ILoginRepository
    {
        Task<IEnumerable<UserLoginModel>> GetUsersAsync();
        Task<UserLoginModel> AuthenticateUserAsync(string username, string password);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly IDbConnection _dbConnection;
        public LoginRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<UserLoginModel>> GetUsersAsync()
        {
            string sql = "SELECT * FROM Users";
            return await _dbConnection.QueryAsync<UserLoginModel>(sql);
        }

        public async Task<UserLoginModel> AuthenticateUserAsync(string username, string password)
        {
            string sql = $"SELECT * FROM Users where Username = @Username and Password = @Password";
            var result = await _dbConnection.QueryFirstOrDefaultAsync<UserLoginModel>(sql, new { Username = username, Password = password });
            return result;
        }
    }
}
