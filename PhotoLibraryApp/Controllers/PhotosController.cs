using Microsoft.AspNetCore.Mvc;
using PhotoLibraryApp.Models;
using PhotoLibraryApp.DataAccess.Context;
using PhotoLibraryApp.DataAccess.Models;

namespace PhotoLibraryApp.Controllers
{
    public class PhotosController : Controller
    {
        private readonly PhotosDbContext _context;

        public PhotosController(PhotosDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            PhotoViewModel model = new PhotoViewModel(_context);
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(int photoId, string name, string imagePath, DateTime date)
        {
            PhotoViewModel model = new PhotoViewModel(_context);

            PhotoModel photos = new(photoId, name, imagePath, date);

            model.SavePhoto(photos);
            model.IsActionSuccess = true;
            model.ActionMessage = "The photo has been saved successfully!";

            return View(model);
        }
        public IActionResult Update(int id)
        {
            PhotoViewModel model = new PhotoViewModel(_context, id);
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            PhotoViewModel model = new PhotoViewModel(_context);

            if (id > 0)
            {
                model.RemovePhoto(id);
            }
            model.IsActionSuccess = true;
            model.ActionMessage = "Photo has been removed succesfully.";
            return View("Index", model);
        }
    }
}
