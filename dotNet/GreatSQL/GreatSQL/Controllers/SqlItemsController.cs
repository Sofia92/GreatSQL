using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GreatSQL.Filters;
using GreatSQL.Models;
using Rule = GreatSQL.Enums.Rule;

namespace GreatSQL.Controllers
{
    [BasicAuthentication]
    public class SqlItemsController : ApiController
    {
        private GreatSQLContext db = new GreatSQLContext();

        // GET: api/SqlItems
        [RuleAuthorization(Rule.ReadLog)]
        public IQueryable<SqlItem> GetSqlItems()
        {
            return db.SqlItems;
        }

        // GET: api/SqlItems/5
        [ResponseType(typeof(SqlItem))]
        [RuleAuthorization(Rule.ReadLog)]
        public IHttpActionResult GetSqlItem(int id)
        {
            SqlItem sqlItem = db.SqlItems.Find(id);
            if (sqlItem == null)
            {
                return NotFound();
            }

            return Ok(sqlItem);
        }

        // PUT: api/SqlItems/5
        [ResponseType(typeof(void))]
        [RuleAuthorization(Rule.CreateSql)]
        public IHttpActionResult PutSqlItem(int id, SqlItem sqlItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sqlItem.ID)
            {
                return BadRequest();
            }

            db.Entry(sqlItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SqlItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SqlItems
        [ResponseType(typeof(SqlItem))]
        [RuleAuthorization(Rule.CreateSql)]
        public IHttpActionResult PostSqlItem(SqlItem sqlItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SqlItems.Add(sqlItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sqlItem.ID }, sqlItem);
        }

        // DELETE: api/SqlItems/5
        [ResponseType(typeof(SqlItem))]
        [RuleAuthorization(Rule.CreateSql)]
        public IHttpActionResult DeleteSqlItem(int id)
        {
            SqlItem sqlItem = db.SqlItems.Find(id);
            if (sqlItem == null)
            {
                return NotFound();
            }

            db.SqlItems.Remove(sqlItem);
            db.SaveChanges();

            return Ok(sqlItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SqlItemExists(int id)
        {
            return db.SqlItems.Count(e => e.ID == id) > 0;
        }
    }
}