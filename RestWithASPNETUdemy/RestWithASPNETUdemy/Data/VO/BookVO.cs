using RestWithASPNETUdemy.Hypermedia;
using RestWithASPNETUdemy.Hypermedia.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        public long Id { get; set; }
        public string Author {get; set;}
        public string Title {get; set;}
        public DateTime LaunchDate {get; set;}
        public decimal Price {get; set;}
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}