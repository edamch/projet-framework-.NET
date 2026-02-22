using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using tp2.Models;
using tp2.Models.Repositories;
using tp2.Models.ViewModels;

namespace tp2.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        readonly IRepository<Product> productRepository;
        public ProductController(IRepository<Product> productRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.productRepository = productRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            var products = productRepository.GetAll();
            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product=productRepository.Get(id);
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModels model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.ImagePath != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;

                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.ImagePath.CopyTo(new FileStream(filePath, FileMode.Create));

                }
            
Product newProduct = new Product

{
    Désignation = model.Désignation,
    Prix = model.Prix,
    Quantite = model.Quantite,
    Image = uniqueFileName
};
                productRepository.Add(newProduct);

                return RedirectToAction("details", new { id = newProduct.Id });

            }
            return View();
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)

        {
            Product product = productRepository.Get(id);
            EditViewModel productEditViewModel = new EditViewModel
            {
                Id = product.Id,
                Désignation = product.Désignation,
                Prix = product.Prix,
                Quantite = product.Quantite,
                ExistingImagePath = product.Image
            };
            return View(productEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(EditViewModel model)

        {

            if (ModelState.IsValid)
            {

                Product product = productRepository.Get(model.Id);

                product.Désignation = model.Désignation;
                product.Prix = model.Prix;
                product.Quantite = model.Quantite;

                if (model.ImagePath != null)
                {

                    if (model.ExistingImagePath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingImagePath);
                        System.IO.File.Delete(filePath);
                    }

                    product.Image = ProcessUploadedFile(model);
                }

                Product updatedProduct = productRepository.Update(product);
                if (updatedProduct != null)
                    return RedirectToAction("Index");
                else
                    return NotFound();

            }
            return View(model);
        }
        [NonAction]
        private string ProcessUploadedFile(EditViewModel model)
        {
            string uniqueFileName = null;
            if (model.ImagePath != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))

                {
                    model.ImagePath.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                productRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
