﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using AuctionHouse.Models;

namespace AuctionHouse.Controllers
{
    public class BidsController : Controller
    {
        private AuctionContext db = new AuctionContext();

        /// <summary>
        /// Action method for displaying the main page for bids
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var bids = db.Bids.Include(b => b.Buyer1).Include(b => b.Item1);
            return View(bids.ToList());
        }

        /// <summary>
        /// Get method for adding bids to the database
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Buyer = new SelectList(db.Buyers, "BuyerId", "BuyerName");
            ViewBag.Item = new SelectList(db.Items, "ItemId", "ItemName");
            return View();
        }

        /// <summary>
        /// Post action method for adding bids to the database
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BidId,Item,Buyer,Price")] Bid bid)
        {
            if (ModelState.IsValid)
            {//gets the current highest bid for the selected item.
                var highestBid = db.Bids.Where(x => x.Item1.ItemId == bid.Item).Select(x => x.Price);
                decimal? topBid = null;
                if(highestBid.FirstOrDefault() != 0)
                {
                    topBid = highestBid.Max();
                }
                else
                {
                    topBid = 0;
                }

                if(bid.Price > topBid)
                {//adds the bid to the database if it is higher then the current bid
                    bid.TimeStamp = DateTime.Now;
                    db.Bids.Add(bid);
                    db.SaveChanges();
                    return RedirectToAction("List", "Home", null);
                }
                else
                {//error if the submitted bid is not the highest bid
                    ViewBag.Buyer = new SelectList(db.Buyers, "BuyerId", "BuyerName", bid.Buyer);
                    ViewBag.Item = new SelectList(db.Items, "ItemId", "ItemName", bid.Item);
                    ModelState.AddModelError("price", "You must bid higher then the current bid of $" + topBid);
                    return View(bid);
                }
                
            }

            ViewBag.Buyer = new SelectList(db.Buyers, "BuyerId", "BuyerName", bid.Buyer);
            ViewBag.Item = new SelectList(db.Items, "ItemId", "ItemName", bid.Item);
            return View(bid);
        }

        /// <summary>
        /// Disposes of connection to the database
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
