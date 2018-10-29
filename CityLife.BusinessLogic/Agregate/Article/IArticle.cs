using CityLife.BusinessLogic.CityNewsService;
using CityLife.Entities.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.BusinessLogic.Agregate.Article
{
    interface IArticle
    {
        INewsService NewsService { get; }

        ApplicationReferences EditTag(ApplicationReferences entry);


    }
}
