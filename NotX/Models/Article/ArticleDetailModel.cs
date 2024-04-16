using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Configuration;

namespace NotX.Models.Article
{
    public class ArticleDetailModel
    {
        /// <summary>
        /// 文章詳情
        /// </summary>
        public Article ArticleDetail { get; set; }

        /// <summary>
        /// 選擇文章ID
        /// </summary>
        public int Choose_ArticleId { get; set; }

        public string ActionType { get; set; }
        public class Article
        {
            public ObjectId Id { get; set; }
            public int ArticleId { get; set; }
            public string Title { get; set; }
            public int AuthorID { get; set; }
            public string Author { get; set; }
            public string Content { get; set; }
            //0下架 1上架
            public int FavoriteNumber { get; set; }
            public int DisplayStatus { get; set; }
            public int ClickNumber { get; set; }
            public DateTime CreationTime { get; set; }
        }


        private readonly IMongoCollection<Article> _collection;

        public ArticleDetailModel()
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string connectionString = $"mongodb+srv://{username}:{password}@20240411lion.ncaf5nq.mongodb.net/?retryWrites=true&w=majority&appName=20240411Lion";
            string databaseName = "20240400NotX";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Article>("Article");
        }

        public async Task<bool> GetArticleDetail()
        {
            var filter = Builders<Article>.Filter.Eq("ArticleId", Choose_ArticleId);
            ArticleDetail = await _collection.Find(filter).FirstOrDefaultAsync();

            return true;
        }

        public async Task<bool> AddClickNumber()
        {
            var filter = Builders<Article>.Filter.Eq("ArticleId", Choose_ArticleId);
            ArticleDetail = await _collection.Find(filter).FirstOrDefaultAsync();

            // 增加點擊數
            if (ArticleDetail.ClickNumber == 0)
            {
                ArticleDetail.ClickNumber = 0;
            }
            if (ArticleDetail.FavoriteNumber == 0)
            {
                ArticleDetail.FavoriteNumber = 0;
            }
            ArticleDetail.ClickNumber++;

            var updateDefinition = Builders<Article>.Update.Set("ClickNumber", ArticleDetail.ClickNumber);
            await _collection.UpdateOneAsync(filter, updateDefinition);

            return true;

        }

        public async Task<bool> AddFavoriteNumber()
        {
            var filter = Builders<Article>.Filter.Eq("ArticleId", Choose_ArticleId);
            ArticleDetail = await _collection.Find(filter).FirstOrDefaultAsync();

            // 增加愛心
            ArticleDetail.FavoriteNumber++;

            var updateDefinition = Builders<Article>.Update.Set("FavoriteNumber", ArticleDetail.FavoriteNumber);
            await _collection.UpdateOneAsync(filter, updateDefinition);

            return true;

        }
    }
}