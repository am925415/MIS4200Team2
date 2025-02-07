﻿using Microsoft.AspNet.Identity;
using MIS4200Team2.DAL;
using MIS4200Team2.Models;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MIS4200Team2.Controllers
{
    public class CoreValueLeaderboardController : Controller
    {
        private Context2 db = new Context2();

        public ActionResult Index(string sortOrder)
        {

            ViewBag.URL = HttpContext.Request.Url.AbsolutePath;

            //https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application

            //ViewBag.TotalSortParm = String.IsNullOrEmpty(sortOrder) ? "total_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.StewardshipSortParm = sortOrder == "Stewardship" ? "stewardship_desc" : "Stewardship";
            ViewBag.CultureSortParm = sortOrder == "Culture" ? "culture_desc" : "Culture";
            ViewBag.DeliverySortParm = sortOrder == "Delivery Excellence" ? "delivery_excellence_desc" : "Delivery Excellence";
            ViewBag.InnovationSortParm = sortOrder == "Innovation" ? "innovation_desc" : "Innovation";
            ViewBag.GreaterSortParm = sortOrder == "Greater Good" ? "greater_good_desc" : "Greater Good";
            ViewBag.IntegritySortParm = sortOrder == "Integrity And Openness" ? "integrity_desc" : "Integrity And Openness";
            ViewBag.BalanceSortParm = sortOrder == "Balance" ? "balance_desc" : "Balance";
            //ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";




            var leaderboard = from s in db.Users
                              select s;

            return View(leaderboard.ToList());
        }

        // GET: CoreValueLeaderboard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoreValueLeaderboard coreValueLeaderboard = db.Users.Find(id);
            if (coreValueLeaderboard == null)
            {
                return HttpNotFound();
            }
            return View(coreValueLeaderboard);
        }

        // GET: CoreValueLeaderboard/Create
        public ActionResult Create()
        {

            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName");
            return View();
        }

        // POST: CoreValueLeaderboard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "leaderboardID,Stewardship,Culture,Delivery_Excellence,Innovation,Greater_Good,Integrity_And_Openness,Balance,TotalPoints,ID")] CoreValueLeaderboard coreValueLeaderboard)
        {
            Guid userID;
            Guid.TryParse(User.Identity.GetUserId(), out userID);
            if (ModelState.IsValid)
            {
                coreValueLeaderboard.Stewardship = 0;
                coreValueLeaderboard.Culture = 0;
                coreValueLeaderboard.Delivery_Excellence = 0;
                coreValueLeaderboard.Innovation = 0;
                coreValueLeaderboard.Greater_Good = 0;
                coreValueLeaderboard.Integrity_And_Openness = 0;
                coreValueLeaderboard.Balance = 0;
                coreValueLeaderboard.ID = userID;
                db.Users.Add(coreValueLeaderboard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);
            return View(coreValueLeaderboard);
        }

        // GET: CoreValueLeaderboard/Edit/5
        public ActionResult Edit(int? id)
        {
            // get information needed to update database correctly

            int leaderboardID = Convert.ToInt32(HttpContext.Request.Url.AbsolutePath.Split('/').Last());

            var coreValues = db.Users.Where(r => r.leaderboardID == leaderboardID).FirstOrDefault();

            TempData["leaderboardID"] = leaderboardID;
            TempData["updateMemberID"] = coreValues.ID; // ID of the person you're viewing
            TempData["Stewardship"] = coreValues.Stewardship;

            //begin authentication process
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CoreValueLeaderboard coreValueLeaderboard = db.Users.Find(id);
            if (coreValueLeaderboard == null)
            {
                return HttpNotFound();
            }

            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);

            //USER MUST BE LOGGED IN TO ENDORSE
            if (Convert.ToString(memberID) == "00000000-0000-0000-0000-000000000000")
            {
                return View("MustLogin");
            }

            //USER CANNOT ENDORSE SELF
            else if (coreValueLeaderboard.ID == memberID)
            {
                return View("SelfEndorse");
            }

            //IF LOGGED IN AND NOT THE USER GO TO ENDORSE PAGE
            else
            {
                // find the user's record
                var currentUser = db.UserDetails.Find(memberID);
                // save the current photo into TempData

                ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);
                TempData["id"] = memberID; // replace with person being edited

                return View(coreValueLeaderboard);
            }


        }

        // POST: CoreValueLeaderboard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Stewardship([Bind(Include = "leaderboardID,Stewardship,Culture,Delivery_Excellence,Innovation,Greater_Good,Integrity_And_Openness,Balance,TotalPoints")] CoreValueLeaderboard coreValueLeaderboard)
        {
            if (ModelState.IsValid)
            {
                int LBID = Convert.ToInt32(TempData["leaderboardID"]);
                int LBID2 = LBID;

                var coreValues = db.Users.Where(r => r.leaderboardID == LBID2).FirstOrDefault();

                coreValues.ID = (Guid)TempData["updateMemberID"];
                coreValues.Stewardship = coreValues.Stewardship + 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);


            return View(coreValueLeaderboard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Culture([Bind(Include = "leaderboardID,Stewardship,Culture,Delivery_Excellence,Innovation,Greater_Good,Integrity_And_Openness,Balance,TotalPoints")] CoreValueLeaderboard coreValueLeaderboard)
        {
            if (ModelState.IsValid)
            {
                int LBID = Convert.ToInt32(TempData["leaderboardID"]);
                int LBID2 = LBID;

                var coreValues = db.Users.Where(r => r.leaderboardID == LBID2).FirstOrDefault();

                coreValues.ID = (Guid)TempData["updateMemberID"];
                coreValues.Culture = coreValues.Culture + 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);


            return View(coreValueLeaderboard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delivery([Bind(Include = "leaderboardID,Stewardship,Culture,Delivery_Excellence,Innovation,Greater_Good,Integrity_And_Openness,Balance,TotalPoints")] CoreValueLeaderboard coreValueLeaderboard)
        {
            if (ModelState.IsValid)
            {
                int LBID = Convert.ToInt32(TempData["leaderboardID"]);
                int LBID2 = LBID;

                var coreValues = db.Users.Where(r => r.leaderboardID == LBID2).FirstOrDefault();

                coreValues.ID = (Guid)TempData["updateMemberID"];
                coreValues.Delivery_Excellence = coreValues.Delivery_Excellence + 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);


            return View(coreValueLeaderboard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Innovation([Bind(Include = "leaderboardID,Stewardship,Culture,Delivery_Excellence,Innovation,Greater_Good,Integrity_And_Openness,Balance,TotalPoints")] CoreValueLeaderboard coreValueLeaderboard)
        {
            if (ModelState.IsValid)
            {
                int LBID = Convert.ToInt32(TempData["leaderboardID"]);
                int LBID2 = LBID;

                var coreValues = db.Users.Where(r => r.leaderboardID == LBID2).FirstOrDefault();

                coreValues.ID = (Guid)TempData["updateMemberID"];
                coreValues.Innovation = coreValues.Innovation + 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);


            return View(coreValueLeaderboard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Greater([Bind(Include = "leaderboardID,Stewardship,Culture,Delivery_Excellence,Innovation,Greater_Good,Integrity_And_Openness,Balance,TotalPoints")] CoreValueLeaderboard coreValueLeaderboard)
        {
            if (ModelState.IsValid)
            {
                int LBID = Convert.ToInt32(TempData["leaderboardID"]);
                int LBID2 = LBID;

                var coreValues = db.Users.Where(r => r.leaderboardID == LBID2).FirstOrDefault();

                coreValues.ID = (Guid)TempData["updateMemberID"];
                coreValues.Greater_Good = coreValues.Greater_Good + 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);


            return View(coreValueLeaderboard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Integrity([Bind(Include = "leaderboardID,Stewardship,Culture,Delivery_Excellence,Innovation,Greater_Good,Integrity_And_Openness,Balance,TotalPoints")] CoreValueLeaderboard coreValueLeaderboard)
        {
            if (ModelState.IsValid)
            {
                int LBID = Convert.ToInt32(TempData["leaderboardID"]);
                int LBID2 = LBID;

                var coreValues = db.Users.Where(r => r.leaderboardID == LBID2).FirstOrDefault();

                coreValues.ID = (Guid)TempData["updateMemberID"];
                coreValues.Integrity_And_Openness = coreValues.Integrity_And_Openness + 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);


            return View(coreValueLeaderboard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Balance([Bind(Include = "leaderboardID,Stewardship,Culture,Delivery_Excellence,Innovation,Greater_Good,Integrity_And_Openness,Balance,TotalPoints")] CoreValueLeaderboard coreValueLeaderboard)
        {
            if (ModelState.IsValid)
            {
                int LBID = Convert.ToInt32(TempData["leaderboardID"]);
                int LBID2 = LBID;

                var coreValues = db.Users.Where(r => r.leaderboardID == LBID2).FirstOrDefault();

                coreValues.ID = (Guid)TempData["updateMemberID"];
                coreValues.Balance = coreValues.Balance + 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);


            return View(coreValueLeaderboard);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "leaderboardID,Stewardship,Culture,Delivery_Excellence,Innovation,Greater_Good,Integrity_And_Openness,Balance,TotalPoints")] CoreValueLeaderboard coreValueLeaderboard)
        //{
        //    //line 164

        //    //if (ModelState.IsValid)
        //    //{
        //    //    coreValueLeaderboard.Stewardship = 0;
        //    //    coreValueLeaderboard.Culture = 0;
        //    //    coreValueLeaderboard.Delivery_Excellence = 0;
        //    //    coreValueLeaderboard.Innovation = 0;
        //    //    coreValueLeaderboard.Greater_Good = 0;
        //    //    coreValueLeaderboard.Integrity_And_Openness = 0;
        //    //    coreValueLeaderboard.Balance = 0;
        //    //    coreValueLeaderboard.ID = userID;
        //    //    db.Users.Add(coreValueLeaderboard);
        //    //    db.SaveChanges();
        //    //    return RedirectToAction("Index");
        //    //}

        //    if (ModelState.IsValid)
        //    {
        //        coreValueLeaderboard.ID = (Guid)TempData["updateMemberID"];
        //        db.Entry(coreValueLeaderboard).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);


        //    return View(coreValueLeaderboard);


        //}

        // GET: CoreValueLeaderboard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoreValueLeaderboard coreValueLeaderboard = db.Users.Find(id);
            if (coreValueLeaderboard == null)
            {
                return HttpNotFound();
            }
            return View(coreValueLeaderboard);
        }

        // POST: CoreValueLeaderboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoreValueLeaderboard coreValueLeaderboard = db.Users.Find(id);
            db.Users.Remove(coreValueLeaderboard);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
