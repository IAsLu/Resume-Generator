using FINAL.Models;
using FINAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

public class UserRegistrationController : Controller
{
    private UserRegistrationRepository _repository = new UserRegistrationRepository();

    // Method to fetch states and cities
    private void PopulateStatesAndCities(UserRegistration model)
    {
        // Sample data; replace with actual data fetching logic
        model.States = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Tamil Nadu" },
        new SelectListItem { Value = "2", Text = "Kerala" },
        new SelectListItem { Value = "3", Text = "Karnataka" },
        new SelectListItem { Value = "4", Text = "Maharashtra" },
        new SelectListItem { Value = "5", Text = "Goa" }
    };

        model.Cities = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Coimbatore" },
        new SelectListItem { Value = "2", Text = "Chennai" },
        new SelectListItem { Value = "3", Text = "Madurai" },
        new SelectListItem { Value = "4", Text = "Palakkad" },
        new SelectListItem { Value = "5", Text = "Kochi" },
        new SelectListItem { Value = "6", Text = "Thiruvananthapuram" },
        new SelectListItem { Value = "7", Text = "Bengaluru" },
        new SelectListItem { Value = "8", Text = "Mysuru" },
        new SelectListItem { Value = "9", Text = "Hubli" },
        new SelectListItem { Value = "10", Text = "Mumbai"},
        new SelectListItem { Value = "11", Text = "Pune"},
        new SelectListItem { Value = "12", Text = "Nagpur"},
        new SelectListItem { Value = "13", Text = "Panaji"},
        new SelectListItem { Value = "14", Text = "Margao"},
        new SelectListItem { Value = "15", Text = "Vasco da Gama" }
    };
    }

    // GET: UserRegistration
    public ActionResult Index()
    {
        var users = _repository.GetAllUsers();
        return View(users);
    }

    // GET: UserRegistration/Details/5
    public ActionResult Details(int id)
    {
        var user = _repository.GetUser(id);
        if (user == null)
        {
            return HttpNotFound();
        }
        return View(user);
    }

    // GET: UserRegistration/Create
    public ActionResult Create()
    {
        var model = new UserRegistration();
        PopulateStatesAndCities(model);
        return View(model);
    }

    // POST: UserRegistration/Create
    [HttpPost]
    public ActionResult Create(UserRegistration user)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _repository.InsertUser(user);
                return RedirectToAction("Index");
            }
            // Repopulate dropdowns if validation fails
            PopulateStatesAndCities(user);
            return View(user);
        }
        catch
        {
            return View();
        }
    }
    // GET: UserRegistration/Edit/5
    public ActionResult Edit(int id)
    {
        var user = _repository.GetUser(id);
        if (user == null)
        {
            return HttpNotFound();
        }

        // Populate states and cities for the edit view
        PopulateStatesAndCities(user);
        return View(user);
    }

    // POST: UserRegistration/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, UserRegistration user)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateUser(user);
                return RedirectToAction("Index");
            }
            // Repopulate dropdowns if validation fails
            PopulateStatesAndCities(user);
            return View(user);
        }
        catch
        {
            return View();
        }
    }

    // GET: UserRegistration/Delete/5
    public ActionResult Delete(int id)
    {
        var user = _repository.GetUser(id);
        if (user == null)
        {
            return HttpNotFound();
        }
        return View(user);
    }

    // POST: UserRegistration/Delete/5
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            _repository.DeleteUser(id);
            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }
}