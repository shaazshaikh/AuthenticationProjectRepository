using AuthenticationProject.DTOs;
using AuthenticationProject.Models.RequestModels;
using AuthenticationProject.Models.ResponseModels;
using AutoMapper;
using Dapper;
using System.Data;

namespace AuthenticationProject.Repository
{
    public interface ILoginRepository
    {
        Task<IEnumerable<UserResponseModel>> GetUsersAsync();
        Task<UserResponseModel> AuthenticateUserAsync(UserRequestModel model);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;
        public LoginRepository(IDbConnection dbConnection, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponseModel>> GetUsersAsync()
        {
            string sql = "select * from Users";
            var userDto = await _dbConnection.QueryAsync<UserDTO>(sql);
            return _mapper.Map<IEnumerable<UserResponseModel>>(userDto);
        }

        public async Task<UserResponseModel> AuthenticateUserAsync(UserRequestModel model)
        {
            string sql = "select * from Users where username = @username and password = @password";
            var resultDto = await _dbConnection.QueryFirstOrDefaultAsync<UserDTO>(sql, new { username = model.UserName, password = model.Password });
            return _mapper.Map<UserResponseModel>(resultDto);
        }
    }
}
