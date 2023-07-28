﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoLibraryApp.DataAccess.Context;
using PhotoLibraryApp.DataAccess.Models;
using PhotoLibraryApp.Models;
using System;
using System.IO;

namespace PhotoLibraryApp.Controllers
{
    public class PhotosController : Controller
    {
        private readonly PhotosDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public PhotosController(PhotosDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            PhotoViewModel model = new PhotoViewModel(_context, _hostingEnvironment);
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateNewFile(int photoId, string name, IFormFile imageFile, DateTime date)
        {
            PhotoViewModel model = new PhotoViewModel(_context, _hostingEnvironment);
                // Save the image file to the server
                if (imageFile != null && imageFile.Length > 0)
                {
                    PhotoModel photo = new PhotoModel(photoId, name, imageFile, date);
                    model.SavePhoto(photo);
                    model.IsActionSuccess = true;
                    model.ActionMessage = "The photo has been saved successfully!";
                }
            return View("Index", model);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult SaveNewPicture(int photoId, string name, IFormFile imageFile, DateTime date) 
        {
            PhotoViewModel model = new PhotoViewModel(_context, _hostingEnvironment);
            if (ModelState.IsValid)
            {
                // Save the image file to the server
                if (imageFile != null && imageFile.Length > 0)
                {
                    PhotoModel photo = new PhotoModel(photoId, name, imageFile, date);
                    model.SavePhoto(photo);
                    model.IsActionSuccess = true;
                    model.ActionMessage = "The photo has been saved successfully!";
                }
            }
            return View(model);
        }
        public IActionResult Update(int id)
        {
            PhotoViewModel model = new PhotoViewModel(_context, id, _hostingEnvironment);
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            PhotoViewModel model = new PhotoViewModel(_context, _hostingEnvironment);
            if (id > 0)
            {
                model.RemovePhoto(id);
            }
            model.IsActionSuccess = true;
            model.ActionMessage = "Photo has been removed successfully.";
            return View("Index", model);
        }
    }
}




/*public class PhotosController : Controller
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
    }*/