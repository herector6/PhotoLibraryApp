using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PhotoLibraryApp.DataAccess.Models
{
    public class PhotoModel
    {

        public int PhotoID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        [NotMapped] public IFormFile ImageFile { get; set; }

        public DateTime Date { get; set; }        

        public PhotoModel(int photoID, string name, IFormFile imageFile, DateTime date)
        {
            PhotoID = photoID;
            Name = name;
            //ImagePath = imagePath;
            ImageFile = imageFile;
            Date = date;
        }
        public PhotoModel()
        {

        }

    }
}
