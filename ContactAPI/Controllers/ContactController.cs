using ContactAPI.Data;
using ContactAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ContactAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly ContactAPIDbContext dbContext;

        public ContactController(ContactAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // Created Get Request
        [HttpGet]
        public IActionResult GetContact()
        {
            return Ok(dbContext.Contacts.ToList());
        }

        [HttpGet]
        [Route("{Id:guid}")]

        public IActionResult GetaContact([FromRoute] Guid Id)
        {
            var contact = dbContext.Contacts.Find(Id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }


        // Created Post Request 
        [HttpPost]
        public async Task<IActionResult> PostContact(AddContact addContact)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = addContact.Name,
                Email = addContact.Email,
                Phone = addContact.Phone,
                Address = addContact.Address,
            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{Id:guid}")]
        public async Task<IActionResult> UpdateContacts([FromRoute] Guid Id, UpdateContact updateContact)
        {
            var contact = dbContext.Contacts.Find(Id);

            if (contact != null)
            {
                contact.Name = updateContact.Name;
                contact.Email = updateContact.Email;
                contact.Phone = updateContact.Phone;
                contact.Address = updateContact.Address;
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{Id:Guid}")]

        public async Task<IActionResult> DeleteContact([FromRoute] Guid Id)
        {
            var contact = await dbContext.Contacts.FindAsync(Id);
            if(contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
    } 
}
