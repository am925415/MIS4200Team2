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
    public class EmployeeRecognitionNominationsController : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        // GET: EmployeeRecognitionNominations
        public ActionResult Index()
        {
            return View(db.employeeRecognitionNominations.ToList());
        }

        // GET: EmployeeRecognitionNominations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeRecognitionNomination employeeRecognitionNomination = db.employeeRecognitionNominations.Find(id);
            if (employeeRecognitionNomination == null)
            {
                return HttpNotFound();
            }
            return View(employeeRecognitionNomination);
        }

        // GET: EmployeeRecognitionNominations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeRecognitionNominations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employeeID,firstName,lastName,businessUnit,recognition")] EmployeeRecognitionNomination employeeRecognitionNomination)
        {
            if (ModelState.IsValid)
            {
                db.employeeRecognitionNominations.Add(employeeRecognitionNomination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeRecognitionNomination);
        }

        // GET: EmployeeRecognitionNominations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeRecognitionNomination employeeRecognitionNomination = db.employeeRecognitionNominations.Find(id);
            if (employeeRecognitionNomination == null)
            {
                return HttpNotFound();
            }
            return View(employeeRecognitionNomination);
        }

        // POST: EmployeeRecognitionNominations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "employeeID,firstName,lastName,businessUnit,recognition")] EmployeeRecognitionNomination employeeRecognitionNomination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeRecognitionNomination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeRecognitionNomination);
        }

        // GET: EmployeeRecognitionNominations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeRecognitionNomination employeeRecognitionNomination = db.employeeRecognitionNominations.Find(id);
            if (employeeRecognitionNomination == null)
            {
                return HttpNotFound();
            }
            return View(employeeRecognitionNomination);
        }

        // POST: EmployeeRecognitionNominations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeRecognitionNomination employeeRecognitionNomination = db.employeeRecognitionNominations.Find(id);
            db.employeeRecognitionNominations.Remove(employeeRecognitionNomination);
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
