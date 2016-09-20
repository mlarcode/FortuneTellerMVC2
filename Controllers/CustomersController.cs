using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC2.Models;

namespace FortuneTellerMVC2.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVC2Entities db = new FortuneTellerMVC2Entities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Color).Include(c => c.Month);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            if (customer.Age % 2 == 0)
            {
                //even
                ViewBag.NumberOfYears = 22;
            }
            else
            {
                ViewBag.NumberOfYears = 38;
            }

            var firstLetterOfMonth = customer.BirthMonth.MonthName[0];
            var secondLetterOfMonth = customer.BirthMonth.MonthName[1];
            var thirdLetterOfMonth = customer.BirthMonth.MonthName[2];
            string wholeName = customer.FirstName + customer.LastName;
            if (wholeName.Contains(firstLetterOfMonth))
            {
                ViewBag.AmountOfMoney = 700000;
            }
            else if (wholeName.Contains(secondLetterOfMonth))
            {
                ViewBag.AmounntOfMoney = 35000;
            }
            else if (wholeName.Contains(thirdLetterOfMonth))
            {
                ViewBag.AmountOfMoney = 1009;
            }
            else
            {
                ViewBag.AmountOfMoney = 15;
            }


            if (customer.NumberOfSiblings == 0)
            {
                ViewBag.Location = "Aruba";
            }
            else if (customer.NumberOfSiblings == 1)
            {
                ViewBag.Location = "St. Kitt";
            }
            else if (customer.NumberOfSiblings == 2)
            {
                ViewBag.Location = "Jersey";
            }
            else if (customer.NumberOfSiblings == 3)
            {
                ViewBag.Location = "Wonka Land";
            }
            else
            {
                ViewBag.Location = "North Pole";
            }




            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.FavoriteColors = new SelectList(db.Colors, "FavoriteColorsID", "Color1");
            ViewBag.BirthMonth = new SelectList(db.Months, "BirthMonthID", "Month1");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,BirthMonth,FavoriteColors,Age,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FavoriteColors = new SelectList(db.Colors, "FavoriteColorsID", "Color1", customer.FavoriteColors);
            ViewBag.BirthMonth = new SelectList(db.Months, "BirthMonthID", "Month1", customer.BirthMonth);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.FavoriteColors = new SelectList(db.Colors, "FavoriteColorsID", "Color1", customer.FavoriteColors);
            ViewBag.BirthMonth = new SelectList(db.Months, "BirthMonthID", "Month1", customer.BirthMonth);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,BirthMonth,FavoriteColors,Age,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FavoriteColors = new SelectList(db.Colors, "FavoriteColorsID", "Color1", customer.FavoriteColors);
            ViewBag.BirthMonth = new SelectList(db.Months, "BirthMonthID", "Month1", customer.BirthMonth);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
