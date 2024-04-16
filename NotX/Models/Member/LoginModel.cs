using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NotX.Models.Member
{
    public class LoginModel
    {
        /// <summary>
        /// 輸入註冊表
        /// </summary>
        public Login InputLoginDetail { get; set; }

        public Login LoginUser { get; set; }

        public class Login
        {
            public ObjectId Id { get; set; }
            public int MemberID { get; set; }
            public string Name { get; set; }
            public string Account { get; set; }
            public string Password { get; set; }
            public string PhoneNumber { get; set; }
            public string Introduction { get; set; }
            //Visitor = 0, Member = 1, Admin = 2
            public int Authentication { get; set; }
            public DateTime CreationTime { get; set; }
        }

        private readonly IMongoCollection<Login> _collection;

        public LoginModel()
        {
            //string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            string connectionString = "mongodb+srv://Lai:20240400@20240411lion.ncaf5nq.mongodb.net/?retryWrites=true&w=majority&appName=20240411Lion";
            string databaseName = "20240400NotX";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Login>("Member");
        }

        /// <summary>
        /// 登入驗證
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckMemberDetail()
        {
            var filter = Builders<Login>.Filter.Eq("Account", InputLoginDetail.Account);
            var MemberExist = await _collection.Find(filter).FirstOrDefaultAsync();

            //加密
            var unhashPassword = InputLoginDetail.Password;
            HashAlgorithm sha = SHA256.Create();
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(unhashPassword);
            byte[] result = sha.ComputeHash(byteArray);
            string hashPassword = System.Text.Encoding.Default.GetString(result);

            if (MemberExist != null) 
            {
                if (MemberExist.Password == hashPassword)
                {
                    LoginUser = MemberExist;
                    return true;
                }
            }
            return false;
        }
    }
}