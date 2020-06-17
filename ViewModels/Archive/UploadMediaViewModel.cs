using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using PinkUmbrella.Models;

namespace PinkUmbrella.ViewModels.Archive
{
    public class UploadMediaViewModel
    {
        public List<IFormFile> Files { get; set; }

        public string Description { get; set; }
        
        public string Title { get; set; }
        
        public Visibility Visibility { get; set; }
    }
}