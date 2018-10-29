using CityLife.Entities.Models.BaseModel;
using CityLife.Entities.Models.References;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.PageTemplates
{
    public class PageTextTemplate : BaseEntity
    {
        public bool IsExistHeaderPic { get; set; }
        public string HeadPicPath { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string H1 { get; set; }
        public string Paragraph1 { get; set; }
        public string H2 { get; set; }
        public string Paragraph2 { get; set; }
        public string H3 { get; set; }
        public string Paragraph3 { get; set; }
        public string H4 { get; set; }
        public string Paragraph4 { get; set; }
        public string H5 { get; set; }
        public string Paragraph5 { get; set; }
        public string H6 { get; set; }
        public string Paragraph6 { get; set; }

        public string ContentPage { get; set; }
        public int ReferenceId { get; set; }

        [ForeignKey("ReferenceId")]
        public ApplicationReferences ApplicationReference { get; set; }
    }
}
