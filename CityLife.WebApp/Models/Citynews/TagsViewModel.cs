using CityLife.WebApp.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.Citynews
{
    public class TagsViewModel: AbstractViewModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}