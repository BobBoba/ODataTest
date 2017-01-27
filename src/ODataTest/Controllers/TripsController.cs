using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;

namespace ODataTest.Controllers
{
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