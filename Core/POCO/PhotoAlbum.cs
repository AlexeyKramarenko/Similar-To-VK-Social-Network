using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace  Core.POCO
{
    public class PhotoAlbum
    {
        [Key]
        public int PhotoAlbumID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int VisibilityLevelOfViewer { get; set; }
        public int VisibilityLevelOfСommentator { get; set; }
        public string UserID { get; set; }
        public bool IsWallAlbum { get; set; }

        public List<Photo> Photos { get; set; }
    }


}
