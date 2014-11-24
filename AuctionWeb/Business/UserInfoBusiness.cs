using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Disappearwind.AppServerSolution.AuctionWeb.Business
{
    public class UserInfoBusiness : BaseBusiness
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public int Login(string name, string pwd)
        {
            int uid = 0;
            var c = from p in DBContext.UserInfo
                    where p.Name == name && p.Pwd == pwd
                    select p;
            if (c != null && c.Count() > 0)
            {
                uid = c.FirstOrDefault().ID;
            }
            return uid;
        }
    }
}