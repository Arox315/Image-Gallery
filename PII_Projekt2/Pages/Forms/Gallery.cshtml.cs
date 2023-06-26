using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PII_Projekt2.Models;

namespace PII_Projekt2.Pages.Forms
{
    public class GaleryModel : PageModel
    {
        private AppDbContext _appDbContext;
        [BindProperty]
        public List<GModel> GalleryList { get; set; }

        public GaleryModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void OnGet()
        {
            GalleryList = _appDbContext.Gallery.ToList();

        }
    }
}
