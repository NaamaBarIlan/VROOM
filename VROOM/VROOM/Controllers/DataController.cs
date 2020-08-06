using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VROOM.Models;
using VROOM.Models.DTOs;
using VROOM.Models.Interfaces;
using VROOM.ViewModels;

namespace VROOM.Controllers
{
    [AllowAnonymous]
    public class DataController : Controller
    {
        private IEmployee _employee;
        private IEquipmentItem _equipmentItem;
        private IEmployeeEquipmentItem _employeeEquipmentItem;

        public DataController(IEmployee employee, IEquipmentItem equipmentItem, IEmployeeEquipmentItem employeeEquipmentItem)
        {
            _employee = employee;
            _equipmentItem = equipmentItem;
            _employeeEquipmentItem = employeeEquipmentItem;
        }

        // GET: EmployeeViewController
        public async Task<ActionResult> Index()
        {
            var allEmployees = await _employee.GetEmployees();
            var allEquipment = await _equipmentItem.GetEquipmentItems();
            var allEmployeeEquipment = await _employeeEquipmentItem.GetAllEmployeeEquipmentRecords();
            var allData = new AllData()
            {
                EmployeeList = allEmployees,
                EquipmentList = allEquipment,
                EmployeeEquipmentList = allEmployeeEquipment
            };
            return View(allData);
        }

        // GET: EmployeeViewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
