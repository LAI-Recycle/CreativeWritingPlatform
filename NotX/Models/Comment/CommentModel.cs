using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NotX.Models.Comment
{
    public class CommentModel
    {
        /// <summary>
        /// 評論詳情
        /// </summary>
        public Comment CommentDetail { get; set; }

        /// <summary>
        /// 選擇留言行為
        /// </summary>
        public string ActionType { get; set; }

        public class Comment
        {
            public ObjectId Id { get; set; }
            public int CommentID { get; set; }
            public int ArticleId { get; set; }
            public int MemberID { get; set; }
            public string MemberName { get; set; }
            public string CommentContent { get; set; }
            public int FavoriteNumber { get; set; }
            public int DisplayStatus { get; set; }
            public DateTime CreationTime { get; set; }
        }

        public int Choose_CommentId { get; set; }

        public Comment InputComment { get; set; }

        private readonly IMongoCollection<Comment> _collection;

        public CommentModel()
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string connectionString = $"mongodb+srv://{username}:{password}@20240411lion.ncaf5nq.mongodb.net/?retryWrites=true&w=majority&appName=20240411Lion";
            string databaseName = "20240400NotX";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Comment>("Comment");
        }

        /// <summary>
        /// 新增評論列表
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddCommentList()
        {
            var New_CommentId = await GetMaxCommentId();

            var comment = new Comment
            {
                CommentID = New_CommentId,
                ArticleId = InputComment.ArticleId,
                MemberID = InputComment.MemberID,
                MemberName = InputComment.MemberName,
                CommentContent = InputComment.CommentContent,
                FavoriteNumber = 0,
                DisplayStatus = 1,
                CreationTime = DateTime.UtcNow,
            };

            await _collection.InsertOneAsync(comment);

            return true;
        }

        //讀取評論

        /// <summary>
        /// 增評論喜愛數
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddFavoriteNumber()
        {
            var filter = Builders<Comment>.Filter.Eq("CommentId", Choose_CommentId);
            CommentDetail = await _collection.Find(filter).FirstOrDefaultAsync();

            // 增加愛心
            CommentDetail.FavoriteNumber++;

            var updateDefinition = Builders<Comment>.Update.Set("FavoriteNumber", CommentDetail.FavoriteNumber);
            await _collection.UpdateOneAsync(filter, updateDefinition);

            return true;
        }


        /// <summary>
        /// 取得最新的CommentId
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetMaxCommentId()
        {
            var maxCollect = await _collection.Find(new BsonDocument())
                                              .Sort(Builders<Comment>.Sort.Descending(a => a.CommentID))
                                              .Limit(1)
                                              .FirstOrDefaultAsync();

            if (maxCollect != null)
            {
                return maxCollect.CommentID + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}