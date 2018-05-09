using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YachtShop.Data;
using YachtShop.Data.Repositories.Interfaces;
using YachtShop.Data.UnitOfWork.Abstraction;
using YachtShop.Models;

namespace YachtShop.Controllers
{
    [Authorize(Roles = "Administrator, Seller")]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientController(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        public IActionResult Error(int? statusCode)
        {
            var vm = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(vm);
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {
            return View("Index",await _clientRepository.GetAll());
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var client = await _clientRepository.GetById(id);
            if (client == null)
            {
                return View("NotFound");
            }

            return View("Details", client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,FirstName,SecondName,PhoneNumber,Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                _clientRepository.Add(client);
                await _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var client = await _clientRepository.GetById(id);
            if (client == null)
            {
                return View("NotFound");
            }
            return View("Edit",client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ClientId,FirstName,SecondName,PhoneNumber,Email")] Client client)
        {
            if (id != client.ClientId)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _clientRepository.Update(client);
                    await _unitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var temp = await ClientExists(client.ClientId);
                    if (!temp)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var client = await _clientRepository.GetById(id);
            if (client == null)
            {
                return View("NotFound");
            }

            return View("Delete", client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var client = await _clientRepository.GetById(id);
            try
            {
                _clientRepository.Delete(client);
                await _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View("NotFound");
            }
        }

        private async Task<bool> ClientExists(string id)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null)
            {
                return false;
            }
            return true;
        }
    }
}
