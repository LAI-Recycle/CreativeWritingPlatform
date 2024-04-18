using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Configuration;

namespace NotX.Models.Article
{
    public class ArticleUpdateDetailModel
    {
        /// <summary>
        /// 文章詳情
        /// </summary>
        public Article ArticleDetail { get; set; }

        /// <summary>
        /// 選擇編輯文章ID
        /// </summary>
        public int Choose_ArticleId { get; set; }
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
        public string ActionType { get; set; }
        public Article InputArticle { get; set; }
        public int New_ArticleId { get; set; }

        private readonly IMongoCollection<Article> _collection;

        public ArticleUpdateDetailModel()
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string connectionString = $"mongodb+srv://{username}:{password}@20240411lion.ncaf5nq.mongodb.net/?retryWrites=true&w=majority&appName=20240411Lion";
            string databaseName = "20240400NotX";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Article>("Article");
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddArticleDetail()
        {
            New_ArticleId = await GetMaxArticleId();
           
            var article = new Article
            {
                ArticleId = New_ArticleId,
                Title = InputArticle.Title,
                AuthorID = InputArticle.AuthorID,
                Author = InputArticle.Author,
                Content = InputArticle.Content,
                ClickNumber = 0,
                FavoriteNumber = 0,
                DisplayStatus = 0,          
                CreationTime = DateTime.UtcNow,
            };

            await _collection.InsertOneAsync(article);

            return true;
        }

        /// <summary>
        /// 抓取文章資訊
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetArticleDetail()
        {
            var filter = Builders<Article>.Filter.Eq("ArticleId", Choose_ArticleId);
            ArticleDetail = await _collection.Find(filter).FirstOrDefaultAsync();

            return true;
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateArticleDetail()
        {
            var filter = Builders<Article>.Filter.Eq("ArticleId", Choose_ArticleId);
            var update = Builders<Article>.Update
                .Set("Title", InputArticle.Title)
                .Set("Content", InputArticle.Content);

            await _collection.UpdateOneAsync(filter, update);

            return true;
        }

        /// <summary>
        /// 取得最新的ArticleId
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetMaxArticleId()
        {
            var maxArticle = await _collection.Find(new BsonDocument())
                                              .Sort(Builders<Article>.Sort.Descending(a => a.ArticleId))
                                              .Limit(1)
                                              .FirstOrDefaultAsync();

            if (maxArticle != null)
            {
                return maxArticle.ArticleId + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}