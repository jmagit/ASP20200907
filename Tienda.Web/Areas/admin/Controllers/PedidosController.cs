using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data.UnitOfWork;

namespace Tienda.Web.Areas.admin.Controllers
{
    [Area("admin")]
    public class PedidosController : Controller
    {
        private readonly TiendaDbContext _context;

        public PedidosController(TiendaDbContext context)
        {
            _context = context;
        }

        // GET: admin/Pedidos
        public async Task<IActionResult> Index()
        {
            var tiendaDbContext = _context.SalesOrderHeader.Include(s => s.BillToAddress).Include(s => s.Customer).Include(s => s.ShipToAddress);
            return View(await tiendaDbContext.ToListAsync());
        }

        // GET: admin/Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderHeader = await _context.SalesOrderHeader
                .Include(s => s.BillToAddress)
                .Include(s => s.Customer)
                .Include(s => s.ShipToAddress)
                .FirstOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }

            return View(salesOrderHeader);
        }

        // GET: admin/Pedidos/Create
        public IActionResult Create()
        {
            ViewData["BillToAddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["ShipToAddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1");
            return View();
        }

        // POST: admin/Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesOrderId,RevisionNumber,OrderDate,DueDate,ShipDate,Status,OnlineOrderFlag,SalesOrderNumber,PurchaseOrderNumber,AccountNumber,CustomerId,ShipToAddressId,BillToAddressId,ShipMethod,CreditCardApprovalCode,SubTotal,TaxAmt,Freight,TotalDue,Comment,Rowguid,ModifiedDate")] SalesOrderHeader salesOrderHeader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesOrderHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillToAddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", salesOrderHeader.BillToAddressId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", salesOrderHeader.CustomerId);
            ViewData["ShipToAddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", salesOrderHeader.ShipToAddressId);
            return View(salesOrderHeader);
        }

        // GET: admin/Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderHeader = await _context.SalesOrderHeader.FindAsync(id);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }
            ViewData["BillToAddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", salesOrderHeader.BillToAddressId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", salesOrderHeader.CustomerId);
            ViewData["ShipToAddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", salesOrderHeader.ShipToAddressId);
            return View(salesOrderHeader);
        }

        // POST: admin/Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesOrderId,RevisionNumber,OrderDate,DueDate,ShipDate,Status,OnlineOrderFlag,SalesOrderNumber,PurchaseOrderNumber,AccountNumber,CustomerId,ShipToAddressId,BillToAddressId,ShipMethod,CreditCardApprovalCode,SubTotal,TaxAmt,Freight,TotalDue,Comment,Rowguid,ModifiedDate")] SalesOrderHeader salesOrderHeader)
        {
            if (id != salesOrderHeader.SalesOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesOrderHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesOrderHeaderExists(salesOrderHeader.SalesOrderId))
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
            ViewData["BillToAddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", salesOrderHeader.BillToAddressId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", salesOrderHeader.CustomerId);
            ViewData["ShipToAddressId"] = new SelectList(_context.Address, "AddressId", "AddressLine1", salesOrderHeader.ShipToAddressId);
            return View(salesOrderHeader);
        }

        // GET: admin/Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderHeader = await _context.SalesOrderHeader
                .Include(s => s.BillToAddress)
                .Include(s => s.Customer)
                .Include(s => s.ShipToAddress)
                .FirstOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }

            return View(salesOrderHeader);
        }

        // POST: admin/Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesOrderHeader = await _context.SalesOrderHeader.FindAsync(id);
            _context.SalesOrderHeader.Remove(salesOrderHeader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesOrderHeaderExists(int id)
        {
            return _context.SalesOrderHeader.Any(e => e.SalesOrderId == id);
        }
    }
}
