using System;
using System.Web;
using System.Web.Mvc;

namespace NotX.Filters
{
    /// <summary>
    /// 角色定義
    /// </summary>
    public enum UserRole
    {
        Visitor = 0, Member = 1, Admin = 2
    }

    public class AuthorizeFilter : AuthorizeAttribute
    {
        public UserRole CheckUserRole { get; set; } //要檢查的角色權限

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="check"></param>
        public AuthorizeFilter(UserRole checkRole)
        {
            CheckUserRole = checkRole;
        }

        /// <summary>
        /// 自訂權限檢查
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            bool CheckResult = false; //false 表示不符合權限

            // 取得用戶角色
            UserRole userRole = UserRole.Visitor; //預設角色
            if (httpContext.Session["UserRole"] != null)
            {
                // 從 Session 取得角色
                userRole = (UserRole)Enum.Parse(typeof(UserRole), httpContext.Session["UserRole"].ToString(), true);
            }

            // 如果使用者是管理員角色直接通過
            if (userRole == UserRole.Admin)
            {
                return true;
            }

            // 檢查一般會員權限
            if (CheckUserRole == UserRole.Member)
            {
                if (userRole == UserRole.Member)
                {
                    CheckResult = true;
                }
            }
            return CheckResult;
        }

        /// <summary>
        /// 權限檢查失敗時動作
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // 當權限檢查失敗時，跳頁至登入頁
            filterContext.Result = new RedirectResult("~/Home/Index?ErrorMsg=unauthorized");
        }
    }
}