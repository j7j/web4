using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Web4.Models;

namespace Web4.API
{
    public class MovieController : ApiController
    {
        private readonly Web4AppContext _db = new Web4AppContext();

        // GET: api/values
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _db.Movies;
        }

        [HttpGet]
        public Movie Get(int id)
        {
            var movie = _db.Movies.FirstOrDefault(e => e.Id == id);

            return movie;
        }

        [HttpPost]
        public Movie Post([FromBody]Movie movie)
        {
            if (ModelState.IsValid)
            {
                var m = _db.Movies.SingleOrDefault(e => e.Id == movie.Id);
                if (m == null)
                {
                    _db.Movies.Add(movie);
                }
                else
                {
                    m.Director = movie.Director;
                    m.Title = movie.Title;
                }
                _db.SaveChanges();
                return m;
            }
            else
            {
                return null;
            }
        }

        [HttpPut]
        public Movie Put([FromBody]Movie movie)
        {
            _db.Movies.Add(movie);
            return movie;
        }


        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var m = _db.Movies.Single(e => e.Id == id);

            _db.Movies.Remove(m);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

    }
}
