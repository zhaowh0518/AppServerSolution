using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Disappearwind.AppServerSolution.AuctionWeb.Models;

namespace Disappearwind.AppServerSolution.AuctionWeb.Business
{
    /// <summary>
    /// 物品分类的业务逻辑
    /// </summary>
    public class KindBusiness : BaseBusiness
    {
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public int AddKind(Kind kind)
        {
            var c = DBContext.Kind.OrderByDescending(p => p.ID).FirstOrDefault();
            if (c == null)
            {
                kind.ID = 1;
            }
            else
            {
                kind.ID = c.ID + 1;
            }
            DBContext.Kind.AddObject(kind);
            return DBContext.SaveChanges();
        }
        /// <summary>
        /// 获取分类的列表
        /// </summary>
        /// <returns></returns>
        public List<Kind> GetKindList()
        {
            var c = from p in DBContext.Kind
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.ToList();
            }
            else
            {
                return new List<Kind>();
            }
        }
    }
}