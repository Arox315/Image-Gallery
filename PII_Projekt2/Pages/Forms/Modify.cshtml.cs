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
    public class ModifyModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        [BindProperty]
        public GModel Gallery { get; set; }
        [BindProperty]
        public IFormFile PhotoFile { get; set; }

        public byte[] filebytes;

        public ModifyModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public void OnGet(int Id)
        {
            Gallery = _appDbContext.Gallery.Find(Id);
            filebytes = System.IO.File.ReadAllBytes(Gallery.ImagePath);
        }

        public IActionResult OnPost(int Id)
        {
            GModel itemToUpdate = _appDbContext.Gallery.Find(Id);
            itemToUpdate.Name = Gallery.Name;
            itemToUpdate.Description = Gallery.Description;

            if (Gallery.Name == null)
            {
                ViewData["Message"] = "Error! No name is given!";
                return null;
            }
            if (PhotoFile != null)
            {
                itemToUpdate.ImagePath = itemToUpdate.Name + ".bmp";
                string filePath = Path.Combine(Environment.CurrentDirectory, itemToUpdate.ImagePath);
                FileStream file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                PhotoFile.CopyTo(file);
                file.Close();
            }
            else
            {
                ViewData["Message"] = "Error! No image is loaded!";
                return null;
            }

            _appDbContext.Update(itemToUpdate);
            _appDbContext.SaveChanges();
            return RedirectToPage("/Forms/View");
        }

    }
}
