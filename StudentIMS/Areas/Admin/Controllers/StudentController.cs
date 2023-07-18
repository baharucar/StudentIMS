using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using DataAccess.Data;
using Models.Models;
using DataAccess.Data.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace StudentIMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StudentList()
        {

            List<Student> studentList = _unitOfWork.StudentRepository.GetAll(properties:"Department").ToList();//_db.Students.ToList();           

            return View(studentList);
        }

        [HttpGet]
        public IActionResult GetStudents()
        {

            List<Student> studentList = _unitOfWork.StudentRepository.GetAll(properties: "Department").ToList();//_db.Students.ToList();           

            return Json(new { data = studentList });
        }

        public IActionResult Create()
        {

            StudentViewModel _studentViewModel=new StudentViewModel();

            IEnumerable<SelectListItem> DepartmentList= _unitOfWork.DepartmentRepository.GetAll(properties: null).Select(i => new SelectListItem
            {
                Text = i.Depart,
                Value = i.Id.ToString()
            });

            _studentViewModel.DepartmentList = DepartmentList;
            _studentViewModel.Student = new Student();

            //ViewData["DepartmentList"] = DepartmentList;

            //ViewBag.DepartmentList = DepartmentList;            

            return View(_studentViewModel);
        }

        [HttpPost]
        public IActionResult Create(StudentViewModel obj, IFormFile? file)
        {
            if(file is not null)
            {
                string wwwpathName = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPattern=Path.Combine(wwwpathName+@"\images\",fileName);

                using(FileStream fs=new FileStream(productPattern,FileMode.Create))
                {
                    file.CopyTo(fs);
                }

                obj.Student.ImageUrl = @"\images\"+fileName;
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.StudentRepository.Add(obj.Student);//_db.Students.Add(obj);
                _unitOfWork.Save();//_db.SaveChanges();
                TempData["success"] = "New record added successfully";
                return RedirectToAction("StudentList", "Student");
            }
            else
            {
                IEnumerable<SelectListItem> DepartmentList = _unitOfWork.DepartmentRepository.GetAll(properties:null).Select(i => new SelectListItem
                {
                    Text = i.Depart,
                    Value = i.Id.ToString()
                });

                obj.DepartmentList = DepartmentList;

                //ViewData["DepartmentList"] = DepartmentList;

                //ViewBag.DepartmentList = DepartmentList;

                return View();
            }            
        }

        public IActionResult Edit(int id)
        {
            Student obj = _unitOfWork.StudentRepository.Get(o => o.Id == id);//_db.Students.Find(id);

            StudentViewModel _studentViewModel = new StudentViewModel();

            IEnumerable<SelectListItem> DepartmentList = _unitOfWork.DepartmentRepository.GetAll(properties: null).Select(i => new SelectListItem
            {
                Text = i.Depart,
                Value = i.Id.ToString()
            });

            _studentViewModel.DepartmentList = DepartmentList;
            _studentViewModel.Student = obj;


            //_db.Students.FirstOrDefault(obj=>obj.Name == name);

            //_db.Students.Where(obj => obj.Name == name).FirstOrDefault();

            if (_studentViewModel.Student is not null)
            {
                return View(_studentViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(StudentViewModel obj, IFormFile? file)
        {
            if (file is not null)
            {
                string wwwpathName = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPattern = Path.Combine(wwwpathName + @"\images\", fileName);

                using (FileStream fs = new FileStream(productPattern, FileMode.Create))
                {
                    file.CopyTo(fs);
                }

                string oldImagePath= wwwpathName+obj.Student.ImageUrl;

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                obj.Student.ImageUrl = @"\images\" + fileName;
            }



            if (obj.Student is not null)
            {
                _unitOfWork.StudentRepository.Update(obj.Student);//_db.Students.Update(obj);
                _unitOfWork.Save();//_db.SaveChanges();
                TempData["success"] = "Record updated successfully";
                return RedirectToAction("StudentList", "Student");
            }
            else
            {
                return NotFound();
            }

        }

        public IActionResult Delete(int id)
        {
            Student obj = _unitOfWork.StudentRepository.Get(o => o.Id == id);//_db.Students.Find(id);

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
        public IActionResult Delete(Student obj)
        {
            if (obj is not null)
            {
                _unitOfWork.StudentRepository.Delete(obj);//_db.Students.Remove(obj);
                _unitOfWork.Save();//_db.SaveChanges();
                TempData["success"] = "Record deleted successfully";
                return RedirectToAction("StudentList", "Student");
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
