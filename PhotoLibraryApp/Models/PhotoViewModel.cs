using PhotoLibraryApp.DataAccess.Context;
using PhotoLibraryApp.DataAccess.Repositories;
using System;
using PhotoLibraryApp.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;


namespace PhotoLibraryApp.Models
{
    public class PhotoViewModel
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
    }
}