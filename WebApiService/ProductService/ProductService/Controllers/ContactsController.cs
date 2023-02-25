using CommonServiceHelper.Service_Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using System.Net;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : Controller
    {
        private readonly ProductServiceContext _contactsDbContext;
        public ContactsController(ProductServiceContext contactDbContext)
        {
            _contactsDbContext = contactDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            //Here _contactsDbContext is database and Contacts is table 
            return  Ok(await _contactsDbContext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await _contactsDbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<CreatedResult> AddContacts(AddContactPayload contact)
        {
            Contact contactModel = new Contact
            {
                Id = Guid.NewGuid(),
                Address = contact.Address,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                MiddleName = contact.MiddleName,
                Phone = contact.Phone
            };
            await _contactsDbContext.Contacts.AddAsync(contactModel);
            await _contactsDbContext.SaveChangesAsync();
            return Created(contactModel.Id.ToString(), contactModel);//201
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContacts([FromRoute] Guid id , UpdateContactPayload payload)//variable name should match with route declaration
        {
            var contact = await _contactsDbContext.Contacts.FindAsync(id);
            if(contact != null)
            {
                contact.Address = payload.Address;
                contact.Email = payload.Email;
                contact.FirstName = payload.FirstName;
                contact.LastName = payload.LastName;
                contact.MiddleName = payload.MiddleName;
                contact.Phone = payload.Phone;
                await _contactsDbContext.SaveChangesAsync();
                return Ok(contact);//200
            }
            return NotFound();//404
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletContacts([FromRoute] Guid id)//variable name should match with route declaration
        {
            var contact = await _contactsDbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                 _contactsDbContext.Contacts.Remove(contact);
                await _contactsDbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();//404
        }
    }
}

//https://www.youtube.com/watch?v=3NWT9k-6xGg
/*
 * 
 * Microsoft.EntityFrameworkCore.InMemory
 1. same controller code with InMemory database

builder.Services.AddDbContext<ContactsAPIDbContext>(options =>
{
    options.UseInMemoryDatabase("SoftwaruditeDB");
});


2. For sql server
Use consistent version of nuget packages ~ 7.0.-

Microsoft.EntityFrameworkCore (if in memory installed then it comes with that)
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools

builder.Services.AddDbContext<ProductServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductServiceContext") ?? throw new InvalidOperationException("Connection string 'ProductServiceContext' not found.")));

//https://www.entityframeworktutorial.net/efcore/entity-framework-core-migration.aspx read
//Then to do migration follow below steps
//go to tools=>nuget package manager => Package Manager Console
// command 1 = "Add-Migration"
//it will create migration folder, for intial migration , up and down method, it has c# code to create sql database and tables

command 2 = "Update-Database"
it will actually run the database


//if domain class changes , and second add migration you do and then if update db is not done then remove-migration can be done
but if db is already updated then throws exception

__EFMigrationsHistory => is table stores all info of migrations , when they applies

so over time if domain class changed , and you added second migration
you can update database

for some reason if you want to revert db you can " update-database <migration name>"
remove all the changes applied for the second migration named MySecondMigration. This will also remove MySecondMigration entry from the __EFMigrationsHistory table in the database.

 Note: This will not remove the migration files related to SecondMigration. Use the remove commands to remove them from the project.
 

Use the following command to generate a SQL script for the database.
"script-migration"



 */

//TrustServerCertificate=Yes (need to add this for SSL)
//ProductServiceContext": "Server=PC-NIKHIL-4159\\SQLEXPRESS;Initial Catalog=SoftwaruditeDB;Integrated Security=True; Database=SoftwaruditeDB;Trusted_Connection=True;MultipleActiveResultSets=true" //"Server=(localdb)\\mssqllocaldb;Database=ProductService.Data;Trusted_Connection=True;MultipleActiveResultSets=true"