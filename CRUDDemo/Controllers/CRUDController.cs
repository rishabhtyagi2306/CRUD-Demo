using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


//I' m updatiiiiiiiuiiiiiiiiiiiiiiiiiiiiiiiiiiiing master because I want to merge.
//Lets merge our branches.
namespace CRUDDemo.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]    //Specify the type of attribute i.e. it will add the record to the database
        public ActionResult create(Student model)
        {
            using(var context = new demoCRUDEntities()) //To open a connection to the database
            {
                context.Student.Add(model); // Add data to the particular table
                context.SaveChanges(); // save the changes to the that are made
            }
            string message = "Created the record successfully";
            ViewBag.Message = message;     // To display the message on the screen after the record is created successfully
            return View(); // write @Viewbag.Message in the created view at the place where you want to display the message
        }

        [HttpGet] // Set the attribute to Read
        public ActionResult Read()
        {
            using(var context = new demoCRUDEntities())
            {
                var data = context.Student.ToList(); // Return the list of data from the database
                return View(data);
            }
            
        }


        public ActionResult Update(int Studentid) // To fill data in the form to enable easy editing
        {
            using(var context = new demoCRUDEntities())
            {
                var data = context.Student.Where(x => x.StudentNo == Studentid).SingleOrDefault();
                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // To specify that this will be invoked when post method is called
        public ActionResult Update(int Studentid, Student model)
        {
            using(var context = new demoCRUDEntities())
            {
                var data = context.Student.FirstOrDefault(x => x.StudentNo == Studentid); // Use of lambda expression to access particular record from a database
                if (data != null) // Checking if any such record exist 
                {
                    data.Name = model.Name;
                    data.Section = model.Section;
                    data.EmailId = model.EmailId;
                    data.Branch = model.Branch;
                    context.SaveChanges();
                    return RedirectToAction("Read"); // It will redirect to the Read method
                }
                else
                    return View();
            }
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Studentid)
        {
            using(var context = new demoCRUDEntities())
            {
                var data = context.Student.FirstOrDefault(x => x.StudentNo == Studentid);
                if (data != null)
                {
                    context.Student.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Read");
                }
                else
                    return View();
            }
        }
    }
}

// Just a small change