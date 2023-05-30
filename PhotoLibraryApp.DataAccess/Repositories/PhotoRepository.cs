using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoLibraryApp.DataAccess.Context;
using PhotoLibraryApp.DataAccess.Models;

namespace PhotoLibraryApp.DataAccess.Repositories
{
    public class PhotoRepository
    {
        private readonly PhotosDbContext _dbContext;

        public PhotoRepository(PhotosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(PhotoModel photo)
        {
            _dbContext.Add(photo);
            _dbContext.SaveChanges();

            return photo.PhotoID;
        }
        public int Update(PhotoModel newPhoto)
        {
            PhotoModel existingPhoto = _dbContext.Photos.Find(newPhoto.PhotoID);

            existingPhoto.Name = newPhoto.Name;
            existingPhoto.ImagePath = newPhoto.ImagePath;
            existingPhoto.Date = newPhoto.Date;            

            _dbContext.SaveChanges();

            return existingPhoto.PhotoID;
        }
        public bool Delete(int photoId)
        {
            PhotoModel photo = _dbContext.Photos.Find(photoId);
            _dbContext.Remove(photo);
            _dbContext.SaveChanges();

            return true;
        }
        public List<PhotoModel> GetAllPhotos()
        {
            List<PhotoModel> photoList = _dbContext.Photos.ToList();

            return photoList;
        }
        public PhotoModel GetPhotosByID(int photoId)
        {
            PhotoModel photoList = _dbContext.Photos.Find(photoId);

            return photoList;
        }
    }
}
