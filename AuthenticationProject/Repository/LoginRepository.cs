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
            string sql = "select * from Users";
            return await _dbConnection.QueryAsync<UserLoginModel>(sql);
        }

        public async Task<UserLoginModel> AuthenticateUserAsync(string username, string password)
        {
            string sql = "select * from Users where username = @username and password = @password";
            var result = await _dbConnection.QueryFirstOrDefaultAsync<UserLoginModel>(sql, new { username = username, password = password });
            return result;
        }
    }
}
