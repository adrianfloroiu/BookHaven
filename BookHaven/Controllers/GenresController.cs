using BookHaven.Core.Contracts;
using BookHaven.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHaven.Controllers
{
    [Authorize(Roles = "admin")]
    public class GenresController : Controller
    {
        private readonly IGenreRepository _genreRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _genreRepository.GetAllAsync();
            return View(genres);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_CreateModal", new Genre());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genreRepository.AddAsync(genre);
                await _genreRepository.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_CreateModal", genre);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            return PartialView("_EditModal", genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Genre genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _genreRepository.UpdateAsync(genre);
                await _genreRepository.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_EditModal", genre);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteModal", genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            await _genreRepository.DeleteAsync(genre);
            await _genreRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
