using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Disappearwind.AppServerSolution.AuctionWeb.Business;
using Disappearwind.AppServerSolution.AuctionWeb.Models;
using Disappearwind.AppServerSolution.AuctionWeb.Utility;

namespace Disappearwind.AppServerSolution.AuctionWeb.Controllers
{
    public class ServiceController : Controller
    {
        #region Business Class
        UserInfoBusiness userInfoBusiness = new UserInfoBusiness();
        KindBusiness kindBusiness = new KindBusiness();
        ItemBusiness itemBusiness = new ItemBusiness();
        BidBusiness bidBusiness = new BidBusiness();
        #endregion

        /// <summary>
        /// 模拟客户端的行为
        /// </summary>
        /// <returns></returns>
        public ActionResult SimulateClient()
        {
            if (Request.Form.Count > 0)
            {
                string url = string.Format("{0}service/{1}", Request.Url.AbsoluteUri, Request.Form["ddlAction"]);
                url = string.Format("{0}?{1}", url, Request.Form["txtData"]);
                string str = WebAccessUtility.Request(url, string.Empty, "text/json;charset=UTF-8");
                ViewData["ServiceRequest"] = Request.Form["txtData"];
                ViewData["ServiceAction"] = Request.Form["ddlAction"];
                ViewData["ServiceResponse"] = str;
            }
            return View();
        }

        #region App Interface
        /// <summary>
        /// 用户登录接口
        /// </summary>
        /// <returns></returns>
        public JsonResult Login()
        {
            int uid = userInfoBusiness.Login(Request["username"], Request["pwd"]);
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("uid", uid);
            if (uid > 0)
            {
                Session["UID"] = uid;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加物品分类
        /// </summary>
        /// <returns></returns>
        public JsonResult AddKind()
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(Request["name"]))
            {
                Kind kind = new Kind();
                kind.Name = Request["name"];
                kind.Desc = Request["desc"];
                int result = kindBusiness.AddKind(kind);
                if (result > 0)
                {
                    msg = "物品分类添加成功！";
                }
                else
                {
                    msg = "物品分类添加失败！";
                }
            }
            else
            {
                msg = "物品分类名称不能为空！";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取所有的物品分类
        /// </summary>
        /// <returns></returns>
        public JsonResult GetKindList()
        {
            List<Kind> kindList = kindBusiness.GetKindList();
            return Json(kindList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加物品
        /// </summary>
        /// <returns></returns>
        public JsonResult AddItem()
        {
            string msg = string.Empty;
            try
            {
                Item item = new Item();
                item.Name = Request["name"];
                item.Desc = Request["desc"];
                item.Remark = Request["remark"];
                item.AvailTime = DateTime.Parse(Request["availtime"]);
                item.UserID = Convert.ToInt32(Request["uid"]);
                item.KindID = Convert.ToInt32(Request["kind"]);
                item.InitPrice = Convert.ToDecimal(Request["initprice"]);
                int result = itemBusiness.AddItem(item);
                if (result > 0)
                {
                    msg = "物品添加成功！";
                }
                else
                {
                    msg = "物品添加失败！";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 取所有的物品
        /// </summary>
        /// <returns></returns>
        public JsonResult GetHotItemList()
        {
            List<Item> itemList = itemBusiness.GetHotItemList();
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 取某个分类下所有的物品
        /// </summary>
        /// <returns></returns>
        public JsonResult GetKindItemList()
        {
            int kindid = 0;
            Int32.TryParse(Request["kindid"], out kindid);
            List<Item> itemList = itemBusiness.GetKindItemList(kindid);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 取流拍的物品
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLostItemList()
        {
            List<Item> itemList = new List<Item>();
            try
            {
                itemList = itemBusiness.GetLostItemList();
                return Json(itemList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// 取某个用户创建的所有物品
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserItemList()
        {
            int uid = 0;
            Int32.TryParse(Request["uid"], out uid);
            List<Item> itemList = itemBusiness.GetUserItemList(uid);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加竞价信息
        /// </summary>
        /// <returns></returns>
        public JsonResult AddBid()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            try
            {
                Bid bid = new Bid();
                bid.ItemID = Convert.ToInt32(Request["itemid"]);
                bid.UserID = Convert.ToInt32(Request["uid"]);
                bid.Price = Convert.ToDecimal(Request["price"]);
                int result = bidBusiness.AddBid(bid);
                data.Add("result", result.ToString());
            }
            catch (Exception ex)
            {
                data.Add("message", ex.Message);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
