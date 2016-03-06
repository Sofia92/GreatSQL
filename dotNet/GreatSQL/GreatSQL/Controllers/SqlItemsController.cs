using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using GreatSQL.Filters;
using GreatSQL.Models;
using static System.String;
using Rule = GreatSQL.Enums.Rule;

namespace GreatSQL.Controllers
{
    public class SqlItemsController : BaseApiController
    {
        // GET: api/SqlItems
        [RuleAuthorization(Rule.ReadAllLog)]
        public IQueryable<SqlItem> GetSqlItems()
        {
            return db.SqlItems;
        }

        // GET: api/SqlItems
        [RuleAuthorization(Rule.ReadLog)]
        public IHttpActionResult GetSqlItems(int creater)
        {
            // 权限检查
            if (User.ID != creater) return Unauthorized();

            var items = from item in db.SqlItems
                        where item.Creater.ID == creater
                        select item;

            return Ok(items);
        }

        // GET: api/SqlItems/5
        [ResponseType(typeof(SqlItem))]
        [RuleAuthorization(Rule.ReadLog | Rule.ReadAllLog)]
        public IHttpActionResult GetSqlItem(int id)
        {
            var sqlItem = db.SqlItems.Find(id);

            if (sqlItem == null)
            {
                return NotFound();
            }

            // 权限检查
            if ((UserRule & Rule.ReadLog) > 0 && sqlItem.Creater_ID != User.ID)
                return Unauthorized();

            sqlItem.Creater = db.Users.Find(sqlItem.Creater_ID);

            return Ok(sqlItem);
        }

        // PUT: api/SqlItems/5
        [ResponseType(typeof(void))]
        [RuleAuthorization(Rule.CreateSql | Rule.UpdateSql)]
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

            sqlItem.ID = db.SqlItems.Any() ? db.SqlItems.Max(i => i.ID) + 1 : 1;
            sqlItem.Record = 0;
            sqlItem.Message = Empty;
            sqlItem.Creater = User;
            sqlItem.Created = DateTime.Now;

            db.SqlItems.Add(sqlItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sqlItem.ID }, sqlItem);
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