using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Disappearwind.AppServerSolution.AuctionWeb.Models;

namespace Disappearwind.AppServerSolution.AuctionWeb.Business
{
    public class BidBusiness : BaseBusiness
    {
        /// <summary>
        /// 添加竞价信息，若当前竞价的价格小于历史竞价价格，不入库，并返回-1
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        public int AddBid(Bid bid)
        {
            var b = from p in DBContext.Bid
                    where p.ItemID == bid.ItemID
                    orderby p.Price descending
                    select p;
            if (b != null && b.Count() > 0)
            {
                if (bid.Price < b.First().Price)
                {
                    return -1;
                }
            }
            var c = DBContext.Bid.OrderByDescending(p => p.ID).FirstOrDefault();
            if (c == null)
            {
                bid.ID = 1;
            }
            else
            {
                bid.ID = c.ID + 1;
            }
            bid.CreateDate = DateTime.Now;
            DBContext.Bid.AddObject(bid);
            DBContext.SaveChanges();
            //更新物品的最高价
            ItemBusiness itemBusiness = new ItemBusiness();
            itemBusiness.UpdateItemBid(bid.ItemID, bid.Price);
            return 1;
        }
    }
}