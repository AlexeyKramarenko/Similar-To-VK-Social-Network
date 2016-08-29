using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.POCO
{
    public class Photo
    {
        [Key]
        public int PhotoID { get; set; } 
        public string Description { get; set; }
        public string UserID { get; set; }
        public string PhotoUrl { get; set; }
        public string Name { get; set; }
        public string ThumbnailPhotoUrl { get; set; }

        [ForeignKey("PhotoAlbum")]

        public int? PhotoAlbumID { get; set; }


        public PhotoAlbum PhotoAlbum { get; set; }
    }
}
