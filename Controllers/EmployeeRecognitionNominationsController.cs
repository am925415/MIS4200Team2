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
using System.Net.Mail;


namespace MIS4200Team2.Controllers
{
    public class EmployeeRecognitionNominationsController : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        // GET: Recognitions
        public ActionResult Index()
        {
            var recognition = db.employeeRecognitionNominations.Include(r => r.Profile);
            return View(recognition.ToList());
        }

        // GET: Recognitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recognition = db.employeeRecognitionNominations.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }

            return View(recognition);
        }

        // GET: Recognitions/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.Profiles, "id", "FullName");
            return View();
        }

        // POST: Recognitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "profileID,description,values,id")] EmployeeRecognitionNomination employeeRecognitionNomination)
        {
            if (ModelState.IsValid)
            {
                db.employeeRecognitionNominations.Add(employeeRecognitionNomination);
                db.SaveChanges();
                SmtpClient myClient = new SmtpClient();
                // the following line has to contain the email address and password of someone
                // authorized to use the email server (you will need a valid Ohio account/password
                // for this to work)
                myClient.Credentials = new NetworkCredential("AuthorizedUser", "UserPassword");
                MailMessage myMessage = new MailMessage();
                // the syntax here is email address, username (that will appear in the email)
                MailAddress from = new MailAddress("jg346015@ohio.edu", "SysAdmin");
                myMessage.From = from;
                // first, the customer found in the order is used to locate the customer record
                var profile = db.Profiles.Find(employeeRecognitionNomination.profileID);
                // then extract the email address from the customer record
                var profileEmail = profile.email;
                // finally, add the email address to the “To” list
                myMessage.To.Add(profileEmail);
                // note: it is possible to add more than one email address to the To list
                // it is also possible to add CC addresses
                myMessage.To.Add("mc200015@ohio.edu"); // this should be replaced with model data
                                                       // as shown at the end of this document
                myMessage.Subject = "Centric Recognition";
                // the body of the email is hard coded here but could be dynamically created using data
                // from the model- see the note at the end of this document
                myMessage.Body = "Congratulations! Your employee as recognized you for displaying exceptional work!";
                try
                {
                    myClient.Send(myMessage);
                    TempData["mailError"] = "";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // this captures an Exception and allows you to display the message in the View
                    TempData["mailError"] = ex.Message;
                }

            }
            // return View();
            ViewBag.id = new SelectList(db.Profiles, "id", "firstName", employeeRecognitionNomination.profileID);
            return View(employeeRecognitionNomination);
        }

        // GET: Recognitions/Edit/5
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
            ViewBag.id = new SelectList(db.Profiles, "id", "firstName", employeeRecognitionNomination.profileID);
            return View(employeeRecognitionNomination);
        }

        // POST: Recognitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recognitionID,description,values,id")] EmployeeRecognitionNomination employeeRecognitionNomination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeRecognitionNomination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.Profiles, "id", "firstName", employeeRecognitionNomination.profileID);
            return View(employeeRecognitionNomination);
        }

        // GET: Recognitions/Delete/5
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

        // POST: Recognitions/Delete/5
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
