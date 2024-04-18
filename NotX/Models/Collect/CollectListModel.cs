using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace NotX.Models.User
{
    public class CollectListModel
    {
      //  /// <summary>
      //  /// 收藏清單
      //  /// </summary>
      //  public List<Collect> CollectList { get; set; }

        /// <summary>
        /// 選擇收藏行為
        /// </summary>
        public string ActionType { get; set; }

        public class Collect
        {
            public ObjectId Id { get; set; }
            public int CollectID { get; set; }
            public int MemberID { get; set; }
            public int ArticleId { get; set; }
            public DateTime CreationTime { get; set; }
        }

        /// <summary>
        /// 選擇收藏ID
        /// </summary>
        public int Choose_CollectID { get; set; }

        /// <summary>
        /// 選擇要收藏的會員ID
        /// </summary>
        public int Choose_CollectMemberID { get; set; }

        /// <summary>
        /// 選擇收藏的文章ID
        /// </summary>
        public int Choose_CollectArticleId { get; set; }

        

        private readonly IMongoCollection<Collect> _collection;

        public CollectListModel()
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string connectionString = $"mongodb+srv://{username}:{password}@20240411lion.ncaf5nq.mongodb.net/?retryWrites=true&w=majority&appName=20240411Lion";
            string databaseName = "20240400NotX";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Collect>("Collect");
        }

      //  /// <summary>
      //  /// 取得收藏列表
      //  /// </summary>
      //  /// <returns></returns>
      //  public async Task<bool> GetCollectList()
      //  {
      //      var filter = Builders<Collect>.Filter.Eq("MemberID", Choose_CollectMemberID);
      //      CollectList = await _collection.Find(filter).ToListAsync();
      //      //CollectList = await _collection.Find(filter).FirstOrDefaultAsync();
      //
      //      return true;
      //  }

        /// <summary>
        /// 新增收藏列表
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddCollectList()
        {
            var New_CollectId = await GetMaxCollectId();

            var collect = new Collect
            {
                CollectID = New_CollectId,
                MemberID = Choose_CollectMemberID,
                ArticleId = Choose_CollectArticleId,
                CreationTime = DateTime.UtcNow,
            };

            await _collection.InsertOneAsync(collect);

            return true;
        }

        /// <summary>
        /// 移除收藏列表
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateCollectList()
        {
            var filter = Builders<Collect>.Filter.Eq(r => r.CollectID, Choose_CollectID);

            await _collection.DeleteOneAsync(filter);

            return true;
        }

        /// <summary>
        /// 取得最新的CollectId
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetMaxCollectId()
        {
            var maxCollect = await _collection.Find(new BsonDocument())
                                              .Sort(Builders<Collect>.Sort.Descending(a => a.CollectID))
                                              .Limit(1)
                                              .FirstOrDefaultAsync();

            if (maxCollect != null)
            {
                return maxCollect.CollectID + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}