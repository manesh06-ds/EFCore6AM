using Microsoft.AspNetCore.Mvc;
using DAL;
using WebAPP.Models;

namespace WebAPP.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext _db;
            public ProductController()
        {
            _db=new AppDbContext();
        }
        public IActionResult Index()
        {
            var data = _db.Products.ToList();
            //var data1 = _db.Products.Select(p => p);
            //var data2=_db.Products.Where(p => p.ProductId>0);  

            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.CategoryList = _db.Categories;
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            ModelState.Remove("ProductId");
            if (ModelState.IsValid)
            {
                Product product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    UnitPrice = model.UnitPrice,
                    CategoryId = model.CategoryId
                };
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryList = _db.Categories;
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _db.Categories;
            Product product = _db.Products.FirstOrDefault(x => x.ProductId == id);
            ProductViewModel obj = new ProductViewModel();
            if (product == null)
            {
                return RedirectToAction("Index");
            }
                obj.Name = product.Name;
            obj.ProductId = product.ProductId;
            obj.Description = product.Description;
            obj.UnitPrice = product.UnitPrice;  
            obj.CategoryId = product.CategoryId;
            
           
           

            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductViewModel model)
        {
            if(id!=model.ProductId)
            {
                return RedirectToAction("Index");
            }
            if(ModelState.IsValid)
            {
                Product product = _db.Products.FirstOrDefault(x => x.ProductId == id);
                product.Name = model.Name;  
                product.Description = model.Description;    
                product.UnitPrice = model.UnitPrice;
                product.CategoryId = model.CategoryId;
                _db.Update(product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("index");
            }
            ViewBag.CategoryList = _db.Categories;
            
            Product product = _db.Products.FirstOrDefault(x => x.ProductId == id);
            ProductViewModel obj = new ProductViewModel();
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            obj.Name = product.Name;
            obj.ProductId = product.ProductId;
            obj.Description = product.Description;
            obj.UnitPrice = product.UnitPrice;
            obj.CategoryId = product.CategoryId;
             return View(obj);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
var product = _db.Products.FirstOrDefault(x=>x.ProductId== id);
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
