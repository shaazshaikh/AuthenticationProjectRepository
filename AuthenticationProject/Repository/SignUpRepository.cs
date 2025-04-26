using AuthenticationProject.Helpers;
using AuthenticationProject.Models.RequestModels;
using AuthenticationProject.Models.ResponseModels;
using Dapper;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AuthenticationProject.Repository
{
    public interface ISignUpRepository
    {
        Task<bool> CreateUserAccountAsync(UserRequestModel model);
    }

    public class SignUpRepository : ISignUpRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly AuthenticationHelper _authenticationHelper;
        public SignUpRepository(IDbConnection dbConnection, AuthenticationHelper authenticationHelper)
        {
            _dbConnection = dbConnection;
            _authenticationHelper = authenticationHelper;
        }

        public async Task<bool> CreateUserAccountAsync(UserRequestModel model)
        {
            string checkUser = "select count(1) from Users where username = @username";
            var existingUser = await _dbConnection.ExecuteScalarAsync<int>(checkUser, new { username = model.UserName });

            if (existingUser > 0)
            {
                return false;
            }

            string insertUser = "insert into Users(id, username, password, uniqueShareId, email,isDeleted,createdDate, modifiedDate) values(@id, @username, @password, null,null, 0, getdate(), getdate())";
            Guid id = Guid.NewGuid();
            var rowsAffected = await _dbConnection.ExecuteAsync(insertUser, new
            {
                id = id,
                username = model.UserName,
                password
            = model.Password
            });

            if (rowsAffected > 0)
            {
                using (var httpClient = new HttpClient())
                {
                    var folderObject = new
                    {
                        ParentFolderId = (string)null,
                        FolderPath = "home",
                        FolderName = "home"
                    };

                    var userObject = new UserResponseModel()
                    {
                        UserName = model.UserName,
                        Password = model.Password,
                        Id = id
                    };

                    var token = await _authenticationHelper.GenerateToken(userObject);
                    string jsonContent = JsonSerializer.Serialize(folderObject);
                    var payload = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    string homeFolderCreateUrl = "https://localhost:7082/api/folder/createFolder";

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await httpClient.PostAsync(homeFolderCreateUrl, payload);
                }
                return true;
            }
            else
            {
                return false;
            }       
        }
    }
}
