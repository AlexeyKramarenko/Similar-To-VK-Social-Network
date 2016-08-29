using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.POCO
{
    public class PhotoUrlResult
    {
        public int PhotoId { get; set; }
        public string Photourl { get; set; }
        public string ThumbnailPhotoUrl { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
