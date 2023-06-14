using PhotoLibraryApp.DataAccess.Context;
using PhotoLibraryApp.DataAccess.Repositories;
using System;
using PhotoLibraryApp.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace PhotoLibraryApp.Models
{
    public class PhotoViewModel
    {
        private readonly PhotoRepository _repo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public List<PhotoModel> PhotoList { get; set; }
        public PhotoModel CurrentPhoto { get; set; }
        public bool IsActionSuccess { get; set; }
        public string ActionMessage { get; set; }

        public PhotoViewModel(PhotosDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _repo = new PhotoRepository(context);
            _webHostEnvironment = webHostEnvironment;
            PhotoList = GetAllPhotos();
            CurrentPhoto = PhotoList.FirstOrDefault();
        }

        public PhotoViewModel(PhotosDbContext context, int photoId, IWebHostEnvironment webHostEnvironment)
        {
            _repo = new PhotoRepository(context);
            _webHostEnvironment = webHostEnvironment;
            PhotoList = GetAllPhotos();

            if (photoId > 0)
            {
                CurrentPhoto = GetPhoto(photoId);
            }
            else
            {
                CurrentPhoto = new PhotoModel();
            }
        }

        public void SavePhoto(PhotoModel photo)
        {
            if (photo.PhotoID > 0)
            {
                // Update existing photo

                // Check if a new image file is provided
                if (photo.ImageFile != null && photo.ImageFile.Length > 0)
                {
                    // Save the image file to the server
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.ImageFile.CopyTo(fileStream);
                    }

                    // Update the image path
                    photo.ImagePath = uniqueFileName;
                }

                _repo.Update(photo);
            }
            else
            {
                // Create new photo

                // Check if an image file is provided
                if (photo.ImageFile != null && photo.ImageFile.Length > 0)
                {
                    // Save the image file to the server
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.ImageFile.CopyTo(fileStream);
                    }

                    // Set the image path
                    photo.ImagePath = uniqueFileName;
                }

                photo.PhotoID = _repo.Create(photo);
            }

            PhotoList = GetAllPhotos();
            CurrentPhoto = GetPhoto(photo.PhotoID);
        }

        public void RemovePhoto(int photoId)
        {
            _repo.Delete(photoId);
            PhotoList = GetAllPhotos();
            CurrentPhoto = PhotoList.FirstOrDefault();
        }

        public List<PhotoModel> GetAllPhotos()
        {
            return _repo.GetAllPhotos();
        }

        public PhotoModel GetPhoto(int photoId)
        {
            return _repo.GetPhotosByID(photoId);
        }
    }
}




/*public class PhotoViewModel
    {
        private PhotoRepository _repo;

        public List<PhotoModel> PhotoList { get; set; }
        public PhotoModel CurrentPhoto { get; set; }
        public bool IsActionSuccess { get; set; }
        public string ActionMessage { get; set; }

        public PhotoViewModel(PhotosDbContext context)
        {
            _repo = new PhotoRepository(context);
            PhotoList = GetAllPhotos();
            CurrentPhoto = PhotoList.FirstOrDefault();
        }
        public PhotoViewModel(PhotosDbContext context, int photoId)
        {
            _repo = new PhotoRepository(context);
            PhotoList = GetAllPhotos();

            if (photoId > 0)
            {
                CurrentPhoto = GetPhoto(photoId);
            }
            else
            {
                CurrentPhoto = new PhotoModel();
            }
        }
        public void SavePhoto(PhotoModel photo)
        {
            if (photo.PhotoID > 0)
            {
                _repo.Update(photo);
            }
            else
            {
                photo.PhotoID = _repo.Create(photo);
            }
            PhotoList = GetAllPhotos();
            CurrentPhoto = GetPhoto(photo.PhotoID);
        }
        public void RemovePhoto(int photoId)
        {
            _repo.Delete(photoId);
            PhotoList = GetAllPhotos();
            CurrentPhoto = PhotoList.FirstOrDefault();
        }
        public List<PhotoModel> GetAllPhotos()
        {
            return _repo.GetAllPhotos();
        }
        public PhotoModel GetPhoto(int photoId)
        {
            return _repo.GetPhotosByID(photoId);
        }
    }*/