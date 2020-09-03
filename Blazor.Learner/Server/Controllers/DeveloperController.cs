using Blazor.Learner.Server.Data;
using Blazor.Learner.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Learner.Server.Controllers
{
    // API controller som holder data til blazor clienten.
    //Her har vi injiceret en ny forekomst af ApplicationDBContent til controllerens konstruktor.
    //Lad os fortsætte med at tilføje hvert af slutpunkterne til vores CRUD-operationer.


    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public DeveloperController(ApplicationDBContext context)
        {
            this._context = context;
        }

        // En Action method til at få alle udviklerne fra kontekstforekomsten.

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var devs = await _context.Developers.ToListAsync();
            return Ok(devs);
        }

        //Get By Id
        // Giver detaljerne om en developer der matcher det indtastede id.
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dev = await _context.Developers.FirstOrDefaultAsync(a => a.ID == id);
            return Ok(dev);
        }


        //Creates a new Developer with the passed developer object data.
        [HttpPost]
        public async Task<IActionResult> Post(Developer developer)
        {
            _context.Add(developer);
            await _context.SaveChangesAsync();
            return Ok(developer.ID);
        }

        //Modifies an existing developer record
        [HttpPut]
        public async Task<IActionResult> Put(Developer developer)
        {
            _context.Entry(developer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        //Deletes a developer record by Id.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dev = new Developer { ID = id };
            _context.Remove(dev);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }


}
