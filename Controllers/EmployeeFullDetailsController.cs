using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MIS4200Team2.DAL;
using MIS4200Team2.Models;

namespace MIS4200Team2.Controllers
{
    public class EmployeeFullDetailsController : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        // GET: EmployeeFullDetails
        public ActionResult Index(string searchString)
        {
            var testusers = from u in db.EmployeeFullDetails select u;
            if (!String.IsNullOrEmpty(searchString))
            {
                testusers = testusers.Where(u =>
                u.lastName.Contains(searchString)
                || u.firstName.Contains(searchString));
                return View(testusers.ToList());
            }
            return View(db.EmployeeFullDetails.ToList());
        }




        

        // GET: EmployeeFullDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeFullDetail employeeFullDetail = db.EmployeeFullDetails.Find(id);
            if (employeeFullDetail == null)
            {
                return HttpNotFound();
            }
            return View(employeeFullDetail);
        }

        // GET: EmployeeFullDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeFullDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employeeFullDetailID,firstName,lastName,businessUnit,hireDate,title")] EmployeeFullDetail employeeFullDetail)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeFullDetails.Add(employeeFullDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeFullDetail);
        }

        // GET: EmployeeFullDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeFullDetail employeeFullDetail = db.EmployeeFullDetails.Find(id);
            if (employeeFullDetail == null)
            {
                return HttpNotFound();
            }
            return View(employeeFullDetail);
        }

        // POST: EmployeeFullDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "employeeFullDetailID,firstName,lastName,businessUnit,hireDate,title")] EmployeeFullDetail employeeFullDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeFullDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeFullDetail);
        }

        // GET: EmployeeFullDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeFullDetail employeeFullDetail = db.EmployeeFullDetails.Find(id);
            if (employeeFullDetail == null)
            {
                return HttpNotFound();
            }
            return View(employeeFullDetail);
        }

        // POST: EmployeeFullDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeFullDetail employeeFullDetail = db.EmployeeFullDetails.Find(id);
            db.EmployeeFullDetails.Remove(employeeFullDetail);
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
