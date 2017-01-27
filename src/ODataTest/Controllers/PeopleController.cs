using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;

namespace ODataTest.Controllers
{
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
}