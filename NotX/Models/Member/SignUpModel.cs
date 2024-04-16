using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NotX.Models.Member
{
    public class SignUpModel
    {
        /// <summary>
        /// 樹入註冊表
        /// </summary>
        public SignUp InputSignUpDetail { get; set; }

        public class SignUp
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

        private readonly IMongoCollection<SignUp> _collection;

        public SignUpModel()
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string connectionString = $"mongodb+srv://{username}:{password}@20240411lion.ncaf5nq.mongodb.net/?retryWrites=true&w=majority&appName=20240411Lion";
            string databaseName = "20240400NotX";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<SignUp>("Member");
        }

        /// <summary>
        /// 檢查會員已經存在與否
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckMemberExist()
        {
            var filter = Builders<SignUp>.Filter.Eq("Account", InputSignUpDetail.Account);
            var MemberExist = await _collection.Find(filter).FirstOrDefaultAsync();

            if (MemberExist != null)
            {
                //存在
                return true;
            }
            else 
            {
                //不存在
                return false;
            }
        }

        /// <summary>
        /// 加入新會員資料
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddMemberDetail()
        {
            var memberId = await GetMaxMemberId();

            //加密
            var unhashPassword = InputSignUpDetail.Password;
            HashAlgorithm sha = SHA256.Create();
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(unhashPassword);
            byte[] result = sha.ComputeHash(byteArray);
            string hashPassword = System.Text.Encoding.Default.GetString(result);

            var member = new SignUp
            {
                MemberID = memberId,
                Name = InputSignUpDetail.Name,
                Account = InputSignUpDetail.Account,
                Password = hashPassword,
                PhoneNumber = InputSignUpDetail.PhoneNumber,
                Introduction = string.Empty,
                Authentication = 1,
                CreationTime = DateTime.UtcNow,
            };

            await _collection.InsertOneAsync(member);
            return true;
        }

        /// <summary>
        /// 取得最新的MemberId
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetMaxMemberId()
        {
            var maxMember = await _collection.Find(new BsonDocument())
                                              .Sort(Builders<SignUp>.Sort.Descending(a => a.MemberID))
                                              .Limit(1)
                                              .FirstOrDefaultAsync();

            if (maxMember != null)
            {
                return maxMember.MemberID + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}