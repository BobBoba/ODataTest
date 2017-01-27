using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Routing.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace ODataTest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    [EnableQuery]
    [Route("odata/[controller]")]
    public class PeopleController : Controller
    {
        private readonly IDemoDataSources _dataSource;

        public PeopleController(IDemoDataSources dataSource)
        {
            this._dataSource = dataSource;
        }

        [HttpGet]
        public IQueryable<Person> Get()
        {
            return _dataSource.People;
        }

        [HttpGet("{id}")]
        public Person Get(string id)
        {
            return _dataSource.People.Single(t => t.ID == id);
        }

        [HttpGet("{id}/Trips")]
        public IQueryable<Trip> GetTripsForPerson(string id)
        {
            return _dataSource.People.Single(t => t.ID == id).Trips.AsQueryable();
        }
    }
    
    [EnableQuery]
    [Route("odata/[controller]")]
    public class TripsController : Controller
    {
        private readonly IDemoDataSources _dataSource;

        public TripsController(IDemoDataSources dataSource)
        {
            _dataSource = dataSource;
        }

        [HttpGet]
        public IQueryable<Trip> Get()
        {
            return _dataSource.Trips;
        }

        [HttpGet("{id}")]
        public Trip Get(string id)
        {
            return _dataSource.Trips.Single(t => t.ID == id);
        }
    }
}