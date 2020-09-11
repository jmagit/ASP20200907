using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data.UnitOfWork;
using Tienda.Web.Models;

namespace Tienda.Web.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ApiClientesController : ControllerBase
    {
        private readonly TiendaDbContext _context;

        public ApiClientesController(TiendaDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<Object> GetCustomer(int? numpage, int pagesize = 8)
        {
            if(numpage.HasValue) {
                return new {
                    numPag = (int)Math.Floor((double)_context.Customer.Count() / pagesize),
                    listado = await _context.Customer
                        .Skip(numpage.Value * pagesize).Take(pagesize)
                        .Select(o => new { o.CustomerId, o.Title, o.FirstName, o.MiddleName, o.LastName})
                        .ToListAsync()
                };
            }
            return await _context.Customer.Select(o => CustomerDTO.From(o)).ToListAsync();
        }

        // GET: api/ApiClientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return CustomerDTO.From(customer);
        }

        // POST: api/ApiClientes
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerDTO customer)
        {
            var entity = CustomerDTO.From(customer);
            _context.Customer.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = entity.CustomerId }, CustomerDTO.From(entity));
        }

        // PUT: api/ApiClientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerDTO customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }
            var entity = await _context.Customer.FindAsync(id);
            if (entity == null) {
                return NotFound();
            }
            customer.To(entity);

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // PUT: api/ApiClientes/5
        [HttpPut("{id}/changepwd")]
        public async Task<IActionResult> PutCustomer(int id, string nueva) {
            //if (id != customer.CustomerId) {
            //    return BadRequest();
            //}
            //var entity = await _context.Customer.FindAsync(id);
            //if (entity == null) {
            //    return NotFound();
            //}
            //customer.To(entity);

            //_context.Entry(entity).State = EntityState.Modified;

            //try {
            //    await _context.SaveChangesAsync();
            //} catch (DbUpdateConcurrencyException) {
            //    if (!CustomerExists(id)) {
            //        return NotFound();
            //    } else {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // DELETE: api/ApiClientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return CustomerDTO.From(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
