using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovieLogin.Models;
using Microsoft.AspNetCore.Identity;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MoviesController(MvcMovieContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            //var movie = await _context.Movie
            //.SingleOrDefaultAsync(m => m.ID == id);

            var reviews = from m in _context.Review
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieReviewVM = new MovieReview();
            //movieReviewVM.movie = movie;
            movieReviewVM.mReview = new SelectList(await genreQuery.Distinct().ToListAsync());
            movieReviewVM.review = await reviews.ToListAsync();

            var movieGenreVM = new MovieGenreViewModel();
            movieGenreVM.genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            movieGenreVM.movies = await movies.ToListAsync();

            var movieReviewModelVM = new MovieReviewModel();
            movieReviewModelVM.MGenre = movieGenreVM;
            movieReviewModelVM.MReview = movieReviewVM;


            var user = _userManager.GetUserAsync(User);
            //user.Result.UserName
            //bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            Console.WriteLine("user: " + user.Result);
            Console.WriteLine("User: " + User);
            Console.WriteLine("Identity: " + User?.Identity);
            if (User?.Identity.IsAuthenticated == true) {
                Console.WriteLine("User: " + User);
                Console.WriteLine("Identity: " + User?.Identity);
                ViewData["LoggedIn"] = true;
            }
            else {
                ViewData["LoggedIn"] = false;
            }
            return View(movieReviewModelVM);

            //return View(await movies.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            IQueryable<int> movieReviewQuery = from m in _context.Movie
                                               orderby m.ID
                                               select m.ID;

            var movie = await _context.Movie
                .SingleOrDefaultAsync(m => m.ID == id);

            var reviews = from m in _context.Review
                          select m;

            if (id == null)
            {
                return NotFound();
            }

            if (movie == null)
            {
                return NotFound();
            }

            var movieReviewVM = new MovieReview();
            movieReviewVM.movie = movie;
            movieReviewVM.mReview = new SelectList(await movieReviewQuery.Distinct().ToListAsync());
            movieReviewVM.review = await reviews.ToListAsync();

            ViewData["mID"] = movie.ID;

            return View(movieReviewVM);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .SingleOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
