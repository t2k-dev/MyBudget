﻿using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBudget.Controllers.API
{
    public class AuthController : ApiController
    {
        private ApplicationDbContext _context;

        public AuthController()
        {
            _context = new ApplicationDbContext();
        }      

        ///<summary>
        ///Получаем настройки пользователя
        ///</summary>

        [HttpGet]
        [Route("api/getconfig")]
        public IHttpActionResult GetConfig(string id)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.Id == id);
                if (user == null)
                    return NotFound();

                return Ok( new { user.CarryoverRests, user.DefCurrency, user.UpdateDate, user.UseTemplates });
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }
        //TODO : убрать это в DTO
        class catDTo
        {
            public int Id { get; set; }
            public string Name { get; set; }            
            public bool IsSpendingCategory { get; set; }
            public string CreatedBy { get; set; }
            public string Icon { get; set; }
        }

        [HttpGet]
        [Route("api/getcategories")]
        public IHttpActionResult GetCategories(string id)
        {
            try
            {                
                var categories = _context.Users.Find(id).Categories.ToList();

                if (categories == null)
                    return NotFound();

                List<catDTo> ReturnCatList = new List<catDTo>();
                foreach (var item in categories)
                {
                    ReturnCatList.Add(new catDTo {
                                            Id = item.Id,
                                            Name =  item.Name,
                                            IsSpendingCategory = item.IsSpendingCategory,
                                            CreatedBy = item.CreatedBy,
                                            Icon = item.Icon
                                    });

                }
                return Ok(ReturnCatList);
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }


    }
}
