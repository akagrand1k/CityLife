using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.Entities.Models.Custom
{
    public class YmapJSONData : BaseYmap
    {
        public YmapJSONData()
        {
            Type = "FeatureCollection";
        }
        //public List<Features> Features { get; set; }
        public Features Features { get; set; }
    }

    public class Features : BaseYmap
    {
        public Features()
        {
            Type = "Feature";
        }
        public int id { get; set; }
        public Geometry Geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry : BaseYmap
    {
        public Geometry()
        {
            Type = "Point";
            Coordinates = new List<string>();
        }

        public List<string> Coordinates { get; set; }
    }

    public class Coordinates
    {
        public  decimal latitude { get; set; }
        public decimal longitude { get; set; }
    }

    public class Properties
    {
        public string balloonContentHeader { get; set; }
        public string clusterCaption { get; set; }
        public string balloonContentBody { get; set; }
        public string balloonContentFooter { get; set; }
        public string hintContent { get; set; }
    }

    public class BaseYmap
    {
        public string Type { get; set; }
    }
}