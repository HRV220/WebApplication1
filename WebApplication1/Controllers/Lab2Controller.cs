using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Entities;
using WebApplication1.Models.ViewModels;
using System.Data;

namespace WebApplication1.Controllers
{
    public class Lab2Controller : Controller
    {
        // GET: Lab2
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PersonDetails(Guid studentId)
        {
            Students model = new Students();
            using (var db = new Entities())
            {
                model = db.Students.Find(studentId);
            }
            return View(model);
        }

        public ActionResult ListOfPeople()
        {
            List<Students> student = new List<Students>();
            using (var db = new Entities())
            {
                student = db.Students.OrderByDescending(x => (DateTime.Now.Year - x.DateOfBirth.Year))
                    .ThenBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .ThenBy(x => x.MiddleName).ToList();
            }
            return View(student);
        }

        List<Tuple<string, string>> GetGenderList()
        {
            List<Tuple<string, string>> genders = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("true", "Женский"),
                new Tuple<string,string>("false", "Мужской")
            };
            return genders;
        }

        [ChildActionOnly]
        public ActionResult HavingGroups(Guid studentID)
        {
            string message = "";
            using (var db = new Entities())
            {
                int StudentHavingGroups = db.Students.Find(studentID).StudentsInGroups.Count;
                message = $"Студент нахрдится в {StudentHavingGroups} группах";
            }
            return PartialView("HavingGroups",message);
        }

        [HttpGet]
        public ActionResult DeleteStudent(Guid studentId)
        {
            Students StudentToDelete;
            using (var db = new Entities())
            {
                StudentToDelete = db.Students.Find(studentId);
            }
            return View(StudentToDelete);
        }

        [HttpPost, ActionName("DeleteStudent")]
        public ActionResult DeleteConfirmed(Guid studentId)
        {
            using (var db = new Entities())
            {
                Students studentToDelete = new Students
                {
                    StudentID = studentId,
                };

                db.Entry(studentToDelete).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return RedirectToAction("ListOfPeople");
        }

        [HttpGet]
        public ActionResult EditStudent(Guid StudentID)
        {
            StudentVM model;
            using (var db = new Entities())
            {
                Students student = db.Students.Find(StudentID);
                model = new StudentVM
                {
                    StudentID = student.StudentID,
                    LastName = student.LastName,
                    FirstName = student.FirstName,
                    MiddleName = student.MiddleName,
                    Gender = student.Gender,
                    Address = student.Address,
                    DateOfBirth = student.DateOfBirth,
                    EducationLevel = student.EducationLevel
                };               
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent (StudentVM model)
        {
            if(ModelState.IsValid)
            {
                using (var db = new Entities())
                {
                    Students editedStudent = new Students
                    {
                        StudentID = model.StudentID,
                        LastName = model.LastName,
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        Gender = model.Gender,
                        Address = model.Address,
                        DateOfBirth = model.DateOfBirth,
                        EducationLevel = model.EducationLevel
                    };
                    db.Students.Attach(editedStudent);
                    db.Entry(editedStudent).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("ListOfPeople");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateStudents()
        {
            ViewBag.Genders = new SelectList(GetGenderList(), "Item1", "Item2");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult CreateStudents(StudentVM newStudent)
        {
            if (ModelState.IsValid)
            {
                using (var context = new Entities())
                {
                    Students student = new Students
                    {
                        StudentID = Guid.NewGuid(),
                        LastName = newStudent.LastName,
                        FirstName = newStudent.FirstName,
                        MiddleName = newStudent.MiddleName,
                        Gender = newStudent.Gender,
                        Address = newStudent.Address,
                        DateOfBirth = newStudent.DateOfBirth,
                        EducationLevel = newStudent.EducationLevel,
                    };
                    context.Students.Add(student);
                    context.SaveChanges();
                }
                return RedirectToAction("ListOfPeople");
            }
            ViewBag.Genders = new SelectList(GetGenderList(), "Item1", "Item2");
            return View(newStudent);
        }


    }
}