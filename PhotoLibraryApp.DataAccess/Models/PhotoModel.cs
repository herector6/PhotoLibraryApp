using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoLibraryApp.DataAccess.Models
{
    public class PhotoModel
    {

        public int PhotoID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }        

        public PhotoModel(int photoID, string name, string imagePath, DateTime date)
        {
            PhotoID = photoID;
            Name = name;
            ImagePath = imagePath;
            Date = date;
        }
        public PhotoModel()
        {

        }

    }
}
