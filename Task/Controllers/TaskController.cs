using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Models;
using Task.Repository;

namespace Task.Controllers
{
    public class TaskController : Controller
    {
        public GenericUnitOfWork _unitOfWord = new GenericUnitOfWork();
        public List<SelectListItem> GetGenders()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var gender = _unitOfWord.GetRepositoryInstance<Gender>().GetAllRecorders();
            foreach (var item in gender)
            {
                list.Add(new SelectListItem { Value = item.GenderId.ToString(), Text = item.GenderName });
            }
            return list;
        }

        public List<SelectListItem> GetStudents()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var students = _unitOfWord.GetRepositoryInstance<Student>().GetAllRecorders();
            foreach (var item in students)
            {
                list.Add(new SelectListItem { Value = item.StId.ToString(), Text = item.firstName });
            }
            return list;
        }

        public List<SelectListItem> GetClasses()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var classes = _unitOfWord.GetRepositoryInstance<Class>().GetAllRecorders();
            foreach (var item in classes)
            {
                list.Add(new SelectListItem { Value = item.classId.ToString(), Text = item.className });
            }
            return list;
        }
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Students()
        {
            return View(_unitOfWord.GetRepositoryInstance<Student>().GetAllRecorders());
        }

        public ActionResult Classes()
        {
            return View(_unitOfWord.GetRepositoryInstance<Class>().GetAllRecorders());
        }

        public ActionResult AddClass()
        {
            ViewBag.Gen = GetGenders();
            return View();
        }

        [HttpPost]
        public ActionResult AddClass(Class x)
        {
            _unitOfWord.GetRepositoryInstance<Class>().Add(x);
            return RedirectToAction("Classes");
        }

        public ActionResult AddStudent()
        {
            ViewBag.Gen = GetGenders();
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            _unitOfWord.GetRepositoryInstance<Student>().Add(student);
            return RedirectToAction("Students");
        }

        public ActionResult ClassEnrolment()
        {
            ViewBag.Students = GetStudents();
            ViewBag.Classes = GetClasses();
            return View();
        }

        [HttpPost]
        public ActionResult ClassEnrolment(classEnrolment cs)
        {
            Class cla = _unitOfWord.GetRepositoryInstance<Class>().GetById(cs.classId);
            Student stu = _unitOfWord.GetRepositoryInstance<Student>().GetById(cs.stId);
            if (cla.classGender != stu.Gender || cla.count>=cla.classCapacity)
            {
                ViewBag.ErrorMessage = "You Make Something Correct, ";
                return RedirectToAction("ClassEnrolment");
            }
            else
            {
                if (cla.count==null)
                {
                    cla.count = 1;
                }
                else
                {
                    cla.count += 1;
                }
                _unitOfWord.GetRepositoryInstance<classEnrolment>().Add(cs);
                return RedirectToAction("ClassStudent");
            }
        }

        public ActionResult ClassStudent()
        {
            ViewBag.Students = GetStudents();
            ViewBag.Classes = GetClasses();
            return View(_unitOfWord.GetRepositoryInstance<classEnrolment>().GetAllRecorders());
        }

    }
}