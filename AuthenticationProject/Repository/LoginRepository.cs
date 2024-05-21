using Dapper;
using SharedModels.AuthenticationModels;
using System.Data;

namespace AuthenticationProject.Repository
{
    public interface ILoginRepository
    {
        Task<IEnumerable<UserLoginModel>> GetUsersAsync();
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
    }
}
