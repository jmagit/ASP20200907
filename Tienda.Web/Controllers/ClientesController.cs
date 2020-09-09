﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data.UnitOfWork;
using Domain.Services.Contracts;

namespace Tienda.Web.Controllers {
    public class ClientesController : Controller {
        private readonly TiendaDbContext _context;
        private readonly ICustomerDomainService service;

        public ClientesController(ICustomerDomainService service) {
            this.service = service;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(int numpage = 0, int pagesize = 10) {
            //return View(service.Get<Customer>(o => o.FirstName.StartsWith("Ja")));
            return View(service.GetPage(numpage, pagesize));
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var customer = service.GetOne(id.Value);
            if (customer == null) {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Clientes/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,CompanyName,SalesPerson,EmailAddress,Phone,PasswordHash,PasswordSalt,Rowguid,ModifiedDate")] Customer customer) {
            if (ModelState.IsValid) {
                service.Add(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var customer = service.GetOne(id.Value);
            if (customer == null) {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,CompanyName,SalesPerson,EmailAddress,Phone,PasswordHash,PasswordSalt,Rowguid,ModifiedDate")] Customer customer) {
            if (id != customer.CustomerId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    service.Modify(customer);
                } catch (DbUpdateConcurrencyException) {
                    if (!CustomerExists(customer.CustomerId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var customer = service.GetOne(id.Value);
            if (customer == null) {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            service.Remove(service.GetOne(id));
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id) {
            return service.GetOne(id) != null;
        }
    }

    /*
     * VERSIÓN GENERADA
     * 
        public class ClientesController : Controller
        {
            private readonly TiendaDbContext _context;

            public ClientesController(TiendaDbContext context)
            {
                _context = context;
            }

            // GET: Clientes
            public async Task<IActionResult> Index(int numpage = 0, int pagesize = 20)
            {
                return View(await _context.Customer.Skip(numpage * pagesize).Take(pagesize).ToListAsync());
            }

            // GET: Clientes/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customer
                    .FirstOrDefaultAsync(m => m.CustomerId == id);
                if (customer == null)
                {
                    return NotFound();
                }

                return View(customer);
            }

            // GET: Clientes/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Clientes/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("CustomerId,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,CompanyName,SalesPerson,EmailAddress,Phone,PasswordHash,PasswordSalt,Rowguid,ModifiedDate")] Customer customer)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }

            // GET: Clientes/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customer.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }

            // POST: Clientes/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("CustomerId,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,CompanyName,SalesPerson,EmailAddress,Phone,PasswordHash,PasswordSalt,Rowguid,ModifiedDate")] Customer customer)
            {
                if (id != customer.CustomerId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(customer);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CustomerExists(customer.CustomerId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }

            // GET: Clientes/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customer
                    .FirstOrDefaultAsync(m => m.CustomerId == id);
                if (customer == null)
                {
                    return NotFound();
                }

                return View(customer);
            }

            // POST: Clientes/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var customer = await _context.Customer.FindAsync(id);
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool CustomerExists(int id)
            {
                return _context.Customer.Any(e => e.CustomerId == id);
            }
        }
     */
}
