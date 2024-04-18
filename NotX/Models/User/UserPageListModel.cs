using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace NotX.Models.User
{
    public class UserPageListModel
    {
        public int InputMemberID { get; set; }
        public User UserData { get; set; }

        public class User
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

        /// <summary>
        /// 文章清單
        /// </summary>
        public List<Article> ArticleList { get; set; }

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
            public int ClickNumber { get; set; }
            public int DisplayStatus { get; set; }
            public DateTime CreationTime { get; set; }
        }

        /// <summary>
        /// 收藏文章清單
        /// </summary>
        public List<Article> CollectArticleList { get; set; }

        /// <summary>
        /// 收藏清單
        /// </summary>
        public List<Collect> CollectList { get; set; }

        public class Collect
        {
            public ObjectId Id { get; set; }
            public int CollectID { get; set; }
            public int MemberID { get; set; }
            public int ArticleId { get; set; }
            public DateTime CreationTime { get; set; }
        }

        private readonly IMongoCollection<User> _collection;
        private readonly IMongoCollection<Article> _collectionArticle;
        private readonly IMongoCollection<Collect> _collectionCollect;

        public UserPageListModel()
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string connectionString = $"mongodb+srv://{username}:{password}@20240411lion.ncaf5nq.mongodb.net/?retryWrites=true&w=majority&appName=20240411Lion";
            string databaseName = "20240400NotX";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<User>("Member");
            _collectionArticle = database.GetCollection<Article>("Article");
            _collectionCollect = database.GetCollection<Collect>("Collect");
        
        }

        /// <summary>
        /// 取得使用者資料
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetUserDetail() 
        {
            var filter = Builders<User>.Filter.Eq("MemberID", InputMemberID);
            UserData = await _collection.Find(filter).FirstOrDefaultAsync();

            return true;
        }

        /// <summary>
        /// 取得文章列表
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetArticleList()
        {
            var filter = Builders<Article>.Filter.Eq("AuthorID", InputMemberID);
            ArticleList = await _collectionArticle.Find(filter).ToListAsync();
            ArticleList = ArticleList.OrderByDescending(article => article.CreationTime).ToList();

            return true;
        }

        /// <summary>
        /// 取得收藏列表
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetUserCollectList()
        {
            var filter = Builders<Collect>.Filter.Eq("MemberID", InputMemberID);
            CollectList = await _collectionCollect.Find(filter).ToListAsync();
            CollectList = CollectList.OrderByDescending(article => article.CreationTime).ToList();

            await GetUserCollectArticleList();

            return true;
        }

        /// <summary>
        /// 使用收藏列表取得文章列表
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetUserCollectArticleList()
        {
            var articleids = CollectList.Select(item => item.ArticleId).ToList();
            var filter = Builders<Article>.Filter.In("ArticleId", articleids);
            CollectArticleList = await _collectionArticle.Find(filter).ToListAsync();
            CollectArticleList = CollectArticleList.OrderByDescending(article => article.CreationTime).ToList();

            return true;
        }
    }
}