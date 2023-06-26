using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PII_Projekt2.Models;

namespace PII_Projekt2.Pages.Forms
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        [BindProperty]
        public GModel Gallery { get; set; }
        [BindProperty]
        public IFormFile PhotoFile { get; set; }

       
        public CreateModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Gallery.Name == null)
            {
                ViewData["Message"] = "Error! No name is given!";
                return null;
            }
            if (PhotoFile != null)
            {
                Gallery.ImagePath = Gallery.Name + ".bmp";
                string filePath = Path.Combine(Environment.CurrentDirectory, Gallery.ImagePath);
                FileStream file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                PhotoFile.CopyTo(file);
                file.Close();
            }
            else
            {
                ViewData["Message"] = "Error! No image is loaded!";
                return null;
            }
            _appDbContext.Add(Gallery);
            _appDbContext.SaveChanges();
            return RedirectToPage("/Forms/View");
        }
    }
}
