using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PII_Projekt2.Models;

namespace PII_Projekt2.Pages.Forms
{
    public class ViewModel : PageModel
    {
        private AppDbContext _appDbContext;
        [BindProperty]
        public List<GModel> GalleryList { get; set; }

        public ViewModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void OnGet()
        {
            GalleryList = _appDbContext.Gallery.ToList();
        }

        public IActionResult OnPost( int Id)
        {
            GModel itemToDelete = _appDbContext.Gallery.Find(Id);
            if(itemToDelete == null)
            {
                return NotFound();
            }
            _appDbContext.Gallery.Remove(itemToDelete);
            _appDbContext.SaveChanges();
            if(itemToDelete.ImagePath != null)
            {
                string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, itemToDelete.ImagePath);
                System.IO.File.Delete(filePath);
            }

            return RedirectToPage("/Forms/View");
        }
    }
}
