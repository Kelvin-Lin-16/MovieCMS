using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;

namespace WebApplication1.Controllers
{
    public class MovieModelsController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        /*
        * Method: HTTP GET
        * URL: /MovieModels
        * Input: Page
        * Output: Movie list page
        */
        public ActionResult Index(int page  = 1)
        {
            var genreList = new List<string>();
            var genreQuery = from d in db.Movies
                             orderby d.Genre
                             select d.Genre;
            genreList.AddRange(genreQuery);
            ViewBag.movieGenre = new SelectList(genreList);
            var movies = from m in db.Movies select m;
            var movieList = movies.OrderByDescending(m => m.Title).ToPagedList(page,10);
            if (Request.IsAjaxRequest())
            {
                return PartialView("List", movieList);
            }
            return View(movieList);
        }
        
        /*
        * Method: HTTP GET
        * URL: /MovieModels/AutoComplete
        * Input: Search string
        * Output: JSON for searching results
        */
        public ActionResult AutoComplete(string term)
        {
            var model = db.Movies.Where(m => m.Title.StartsWith(term))
                .Take(10)
                .Select(m => new {
                    label = m.Title
                });
            return Json(model, JsonRequestBehavior.AllowGet);
        }
       
        /*
        * Method: HTTP POST
        * URL: /MovieModels/Search
        * Input: Search string, page number, movie category
        * Output: JSON for searching results
        */
        [HttpPost ActionName("Search")]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string movieGenre, string searchString, int page =1)
        {
            var genreList = new List<string>();
            var genreQuery = from d in db.Movies
                             orderby d.Genre
                             select d.Genre;
            genreList.AddRange(genreQuery.Distinct());
            ViewBag.movieGenre = new SelectList(genreList);

            var movies = from m in db.Movies select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }
            var movieList = movies.OrderByDescending(m => m.Title).ToPagedList(page, 10);
            if (Request.IsAjaxRequest())
            {
                return PartialView("List", movieList);     
            }
            return View("Index", movieList);
        }
        
        /*
        * Method: HTTP GET
        * URL: /MovieModels/Details/5
        * Input: Movie id
        * Output: Movie detail page
        */
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModels movieModels = db.Movies.Find(id);
            if (movieModels == null)
            {
                return HttpNotFound();
            }
            return View(movieModels);
        }
        
        /*
        * Method: HTTP GET
        * URL: /MovieModels/Create
        * Input: None
        * Output: Movie creation page
        */
        public ActionResult Create()
        {
            return View();
        }
        
        /*
        * Method: HTTP POST
        * URL: /MovieModels/Create
        * Input: Movie model(id, title, release date, price, category)
        * Output: Movie creation page
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] MovieModels movieModels)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movieModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movieModels);
        }
        
        /*
        * Method: HTTP GET
        * URL: /MovieModels/Edit/5
        * Input: Movie id
        * Output: Movie editing page
        */
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModels movieModels = db.Movies.Find(id);
            if (movieModels == null)
            {
                return HttpNotFound();
            }
            return View(movieModels);
        }
        
        /*
        * Method: HTTP POST
        * URL: /MovieModels/Edit/5
        * Input: Movie model
        * Output: Movie editing result page
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] MovieModels movieModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieModels);
        }
        
       /*
       * Method: HTTP GET
       * URL: /MovieModels/Delete/5
       * Input: Movie id
       * Output: Movie deleting page
       */
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModels movieModels = db.Movies.Find(id);
            if (movieModels == null)
            {
                return HttpNotFound();
            }
            return View(movieModels);
        }
        
       /*
       * Method: HTTP POST
       * URL: /MovieModels/Delete/5
       * Input: Movie id
       * Output: Movie deleting result page
       */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieModels movieModels = db.Movies.Find(id);
            db.Movies.Remove(movieModels);
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
