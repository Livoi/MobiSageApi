using Dapper;
using Microsoft.Extensions.Configuration;
using MobiSageApi.Common;
using MobiSageApi.IRepository;
using MobiSageApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MobiSageApi.Repository
{
    public class UserRepository : IUserRepository
    {
        string _connectionString = "";
        User _oUser = new User();
        List<User> _oUsers = new List<User>();

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MobiSageDB");
        }
        public async Task<string> Delete(User obj)
        {
            string message = "";
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString)){
                    if (con.State == ConnectionState.Closed) con.Open();
                    var Users = await con.QueryAsync<User>("SP_User", 
                        this.SetParameters(obj, (int)OperationType.Delete), 
                        commandType: CommandType.StoredProcedure);
                    message="Deleted"; 
                }
            }catch(Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public async Task<User> Get(int objId)
        {
            _oUser = new User();
            using(IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var Users = await con.QueryAsync<User>(string.Format(@"SELECT * FROM [User] WHERE UserId={0}", objId));
                if (Users !=null && Users.Count() > 0)
                {
                    _oUser = Users.SingleOrDefault();
                }
            }

            return _oUser;
        }

        public async Task<User> GetByUsernamePassword(User user)
        {
            _oUser = new User();
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var Users = await con.QueryAsync<User>(string.Format(@"SELECT * FROM [User] WHERE Username='{0}' AND Password='{1}'", user.Username,user.Password));
                if (Users != null && Users.Count() > 0)
                {
                    _oUser = Users.SingleOrDefault();
                }
            }

            return _oUser;
        }

        public async Task<List<User>> Gets()
        {
            _oUsers = new List<User>();
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var Users = await con.QueryAsync<User>("SELECT * FROM [User]");
                if (Users != null && Users.Count() > 0)
                {
                    _oUsers = Users.ToList();
                }
            }

            return _oUsers;
        }

        public async Task<User> Save(User obj)
        {
            _oUser = new User();
            try
            {
                int operationType = Convert.ToInt32(obj.UserId == 0 ? OperationType.Insert : OperationType.Update);
                using(IDbConnection con = new SqlConnection(_connectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    var Users = await con.QueryAsync<User>("SP_User",
                        this.SetParameters(obj, operationType),
                        commandType: CommandType.StoredProcedure);
                    if (Users != null && Users.Count() > 0) _oUser = Users.FirstOrDefault();
                }
            }catch(Exception ex)
            {
                _oUser = new User();
                _oUser.Message = ex.Message;
            }

            return _oUser;
        }

        private DynamicParameters SetParameters(User oUser, int nOpertaionType)
        {
            DynamicParameters paramters = new DynamicParameters();
            paramters.Add("@UserId", oUser.UserId);
            paramters.Add("@Username", oUser.Username);
            paramters.Add("@Email", oUser.Email);
            paramters.Add("@Password", oUser.Password);
            paramters.Add("@Phone", oUser.Phone);
            paramters.Add("@OperationType", nOpertaionType);
            return paramters;
        }
    }
}
