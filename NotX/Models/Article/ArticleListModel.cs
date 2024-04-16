using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace NotX.Models.Article
{
    public class ArticleListModel
    {
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
        /// 選擇排列方式
        /// </summary>
        public string ChooseOrderCondition { get; set; }

        /// <summary>
        /// 選擇排列方式字典
        /// </summary>

        public Dictionary<string, string> ChooseOrderConditionDict = new Dictionary<string, string>();

        public void InitDict()
        {
            ChooseOrderConditionDict = new Dictionary<string, string>();
            ChooseOrderConditionDict.Add("CreationTime", "建立時間" );
            ChooseOrderConditionDict.Add("FavoriteNumber", "最愛數" );
            ChooseOrderConditionDict.Add("ClickNumber", "瀏覽量");
            ChooseOrderConditionDict.Add("Title", "標題名稱" );
        }
       

        private readonly IMongoCollection<Article> _collection;

        public ArticleListModel()
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string connectionString = $"mongodb+srv://{username}:{password}@20240411lion.ncaf5nq.mongodb.net/?retryWrites=true&w=majority&appName=20240411Lion";
            string databaseName = "20240400NotX";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Article>("Article");
        }

        public async Task<bool> GetArticleList()
        {
            ArticleList = await _collection.Find(new BsonDocument()).ToListAsync();

            switch (ChooseOrderCondition)
            {
                case "CreationTime":
                    ArticleList = ArticleList.OrderByDescending(article => article.CreationTime).ToList();
                    break;
                case "FavoriteNumber":
                    ArticleList = ArticleList.OrderByDescending(article => article.FavoriteNumber).ToList();
                    break;
                case "ClickNumber":
                    ArticleList = ArticleList.OrderByDescending(article => article.ClickNumber).ToList();
                    break;
                case "Title":
                    ArticleList = ArticleList.OrderByDescending(article => article.ClickNumber).ToList();
                    break;
                default:
                    ArticleList = ArticleList.OrderByDescending(article => article.CreationTime).ToList();
                    break;
            }

            return true;
        }
    }
}