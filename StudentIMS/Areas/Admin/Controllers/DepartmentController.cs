using DataAccess.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace StudentIMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult DepartmentList()
        {

            List<Department> DepartmentList = _unitOfWork.DepartmentRepository.GetAll(properties:null).ToList();//_db.Departments.ToList();           

            return View(DepartmentList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(obj);//_db.Departments.Add(obj);
                _unitOfWork.Save();//_db.SaveChanges();
                TempData["success"] = "New record added successfully";
                return RedirectToAction("DepartmentList", "Department");
            }


            return View();
        }

        public IActionResult Edit(int id)
        {
            Department obj = _unitOfWork.DepartmentRepository.Get(o => o.Id == id);//_db.Departments.Find(id);

            //_db.Departments.FirstOrDefault(obj=>obj.Name == name);

            //_db.Departments.Where(obj => obj.Name == name).FirstOrDefault();

            if (obj is not null)
            {
                return View(obj);
            }
            else
            {
                TempData["success"] = "Department not found";
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(Department obj)
        {
            if (obj is not null)
            {
                _unitOfWork.DepartmentRepository.Update(obj);//_db.Departments.Update(obj);
                _unitOfWork.Save();//_db.SaveChanges();
                TempData["success"] = "Record updated successfully";
                return RedirectToAction("DepartmentList", "Department");
            }
            else
            {
                return NotFound();
            }

        }

        public IActionResult Delete(int id)
        {
            Department obj = _unitOfWork.DepartmentRepository.Get(o => o.Id == id);//_db.Departments.Find(id);

            if (obj is not null)
            {
                return View(obj);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost] //ActionName("Delete")
        public IActionResult Delete(Department obj)
        {
            if (obj is not null)
            {
                _unitOfWork.DepartmentRepository.Delete(obj);//_db.Departments.Remove(obj);
                _unitOfWork.Save();//_db.SaveChanges();
                TempData["success"] = "Record deleted successfully";
                return RedirectToAction("DepartmentList", "Department");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
