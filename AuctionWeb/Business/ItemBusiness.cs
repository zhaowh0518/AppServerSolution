using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Disappearwind.AppServerSolution.AuctionWeb.Models;

namespace Disappearwind.AppServerSolution.AuctionWeb.Business
{
    /// <summary>
    /// 物品
    /// </summary>
    public class ItemBusiness : BaseBusiness
    {
        /// <summary>
        /// 添加物品
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int AddItem(Item item)
        {
            var c = DBContext.Item.OrderByDescending(p => p.ID).FirstOrDefault();
            if (c == null)
            {
                item.ID = 1;
            }
            else
            {
                item.ID = c.ID + 1;
            }
            item.CreateDate = DateTime.Now;
            item.BidCount = 0;
            DBContext.Item.AddObject(item);
            return DBContext.SaveChanges();
        }
        /// <summary>
        /// 获取指定ID的物品
        /// </summary>
        /// <returns></returns>
        public Item GetItem(int id)
        {
            var c = from p in DBContext.Item
                    where p.ID == id
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.SingleOrDefault();
            }
            else
            {
                return new Item();
            }
        }
        /// <summary>
        /// 获取所有的物品
        /// </summary>
        /// <returns></returns>
        public List<Item> GetAllItemList()
        {
            var c = from p in DBContext.Item
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.ToList();
            }
            else
            {
                return new List<Item>();
            }
        }
        /// <summary>
        /// 根据分类获取物品列表
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public List<Item> GetKindItemList(int kindID)
        {
            var c = from p in DBContext.Item
                    where p.KindID == kindID
                    orderby p.CreateDate descending
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.ToList();
            }
            else
            {
                return new List<Item>();
            }
        }
        /// <summary>
        /// 获取最热门拍卖的物品
        /// </summary>
        /// <returns></returns>
        public List<Item> GetHotItemList()
        {
            var c = from p in DBContext.Item
                    where p.AvailTime >= DateTime.Now
                    orderby p.BidCount descending
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.ToList();
            }
            else
            {
                return new List<Item>();
            }
        }
        /// <summary>
        /// 获取流拍的物品
        /// </summary>
        /// <returns></returns>
        public List<Item> GetLostItemList()
        {
            var c = from p in DBContext.Item
                    where p.AvailTime < DateTime.Now
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.ToList();
            }
            else
            {
                return new List<Item>();
            }
        }
        /// <summary>
        /// 获取某个用户的所有物品
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Item> GetUserItemList(int userID)
        {
            var c = from p in DBContext.Item
                    where p.UserID == userID
                    select p;
            if (c != null && c.Count() > 0)
            {
                return c.ToList();
            }
            else
            {
                return new List<Item>();
            }
        }
        /// <summary>
        /// 更新物品的最高价
        /// </summary>
        /// <returns></returns>
        public int UpdateItemBid(int itemid, decimal price)
        {
            var item = DBContext.Item.Where(p => p.ID == itemid).SingleOrDefault();
            if (item != null)
            {
                item.MaxPrice = price;
                item.BidCount = item.BidCount + 1;
            }
            return DBContext.SaveChanges();
        }
    }
}