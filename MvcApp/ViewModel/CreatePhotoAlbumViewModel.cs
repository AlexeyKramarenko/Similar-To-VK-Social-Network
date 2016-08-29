using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApp.ViewModel
{
    public class CreatePhotoAlbumViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int VisibilityLevelOfViewer { get; set; }
        public int VisibilityLevelOfcommentator { get; set; }
    }
}
