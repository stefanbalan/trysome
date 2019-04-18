using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData;
//using DbContexts;
//using Domain;

namespace ts.OData.Core.WithModel.Controllers
{

    public class MainEntity
    {
        public int Id { get; set; }
        public ICollection<ChildEntity> Children { get; set; }
    }

    public class ChildEntity
    {
        public int Id { get; set; }
    }

    public class SomeDbContext : DbContext
    {
        public DbSet<MainEntity> MainEntitySet;
        public DbSet<ChildEntity> ChildrenSet { get; set; }
    }

    /* 
     *  This is a sample controller WITHOUT using model, exposing entities directly (but... I was in a hurry)
     */

    public class MainEntitysController : ODataController
    {
        private readonly SomeDbContext _SomeDbContext;
        private static readonly ODataValidationSettings ValidationSettings = new ODataValidationSettings();

        public MainEntitysController(/*SomeDbContext SomeDbContext*/)
        {
            //            _SomeDbContext = SomeDbContext;
        }

        // GET: odata/MainEntitys
        [EnableQuery]
        public IActionResult GetMainEntitys(ODataQueryOptions<MainEntity> queryOptions)
        {
            try
            {
                queryOptions.Validate(ValidationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            try
            {
                return Ok(_SomeDbContext.MainEntitySet.AsQueryable());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [EnableQuery]
        public IActionResult GetMainEntity([FromODataUri] int key, ODataQueryOptions<MainEntity> queryOptions)
        {
            try
            {
                queryOptions.Validate(ValidationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            try
            {
                return Ok(_SomeDbContext.MainEntitySet.Where(ls => ls.Id == key).AsQueryable());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult GetDbServerIdentities([FromODataUri] int key)
        {
            try
            {
                var result = _SomeDbContext.MainEntitySet
                                            .Include(ls => ls.Children)
                                            .FirstOrDefault(ls => ls.Id == key)
                                            ?.Children;
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //PUT: odata/MainEntitys(5)
        public async Task<IActionResult> Put([FromODataUri] int key, Delta<MainEntity> delta)
        {
            //Validate(delta.GetInstance()); // TryValidateModel?

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var MainEntity

            // delta.Put(MainEntity);

            // TODO: Save the patched entity.

            // return Updated(MainEntity);
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        // POST: odata/MainEntitys
        public async Task<IActionResult> Post(MainEntity MainEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(MainEntity);
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        // PATCH: odata/MainEntitys(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<MainEntity> delta)
        {
            //Validate(delta.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(MainEntity);

            // TODO: Save the patched entity.

            // return Updated(MainEntity);
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        // DELETE: odata/MainEntitys(5)
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(StatusCodes.NoContent);
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [AcceptVerbs("POST", "PUT")]
        //https://stackoverflow.com/a/54111132/1187199
        public async Task<IActionResult> CreateRef([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        {
            var MainEntity = await _SomeDbContext.MainEntitySet.SingleOrDefaultAsync(ls => ls.Id == key);
            if (MainEntity == null)
            {
                return NotFound();
            }

            switch (navigationProperty)
            {
                case "Children":
                    var relatedKey = ts.OData.Core.WithModel.Infrastructure.Extensions.GetKeyFromUri<Guid>(this.HttpContext?.Request, link);

                    //var serverIdentity = await _SomeDbContext.ChildrenSet.SingleOrDefaultAsync(si => si.ID == relatedKey);
                    //if (serverIdentity == null)
                    //{
                    //    return NotFound();
                    //}

                    //MainEntity.Children.Add(serverIdentity);
                    break;

                default:
                    return StatusCode(StatusCodes.Status501NotImplemented);
            }
            await _SomeDbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
