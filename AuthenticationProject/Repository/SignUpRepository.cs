using Dapper;
using System.Data;

namespace AuthenticationProject.Repository
{
    public interface ISignUpRepository
    {
        Task<bool> CreateUserAccountAsync(string username, string password);
    }

    public class SignUpRepository : ISignUpRepository
    {
        private readonly IDbConnection _dbConnection;
        public SignUpRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> CreateUserAccountAsync(string username, string password)
        {
            string checkUser = "select count(1) from Users where username = @username";
            var existingUser = await _dbConnection.ExecuteScalarAsync<int>(checkUser, new { username = username });

            if (existingUser > 0)
            {
                return false;
            }

            string insertUser = "insert into Users(id, username, password, uniqueShareId, email,isDeleted,createdDate, modifiedDate) values(NewId(), @username, @password, null,null, 0, getdate(), getdate())";

            await _dbConnection.ExecuteAsync(insertUser, new
            {
                username = username,
                password
            = password
            });

            return true;
        }
    }
}
