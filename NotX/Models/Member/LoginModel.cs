using MongoDB.Driver;
using System.Threading.Tasks;

namespace NotX.Models.Member
{
    public class LoginModel
    {
        /// <summary>
        /// 樹入註冊表
        /// </summary>
        public Login InputLoginDetail { get; set; }

        public class Login
        {
            public string Account { get; set; }
            public string Password { get; set; }
            public int Authentication { get; set; }
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

            if (MemberExist == null) 
            {
                return false;
            }
            //密碼比對
            if (MemberExist.Password == InputLoginDetail.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}