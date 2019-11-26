using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using OrderDataService.Repository;
using System.Threading.Tasks;
using OrderDataService.Models;

namespace OrderDataService.Controllers
{
    [EnableCors(origins: "http://localhost:60866", headers: "*", methods: "*")]
    public class OrderDataController : ApiController
    {
        // GET api/values
        [Route("api/Tester")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Test Value 1", "Test Value 2" };
        }

        [Route("api/SimpleOrderSearch_OrderIDOnly/{OrderID}")]
        [HttpGet]
        public IHttpActionResult GetByOrderIDOnly(long OrderID)
        {
            try
            {
                var results = getData.filterData(OrderID);
                if (results.Count == 0)
                {
                    return NotFound();
                }

                return Ok(results);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, "Unable to return any data due to an unknown error in the API...");
            }
        }

        [Route("api/SimpleOrderSearch")]
        [HttpPost]
        public IHttpActionResult GetByMSAStatus([FromBody] DataParams dataParams)
        {
            try
            {
                var validParams = getData.ValidateParams(dataParams);
                if (!validParams)
                {
                    return BadRequest("Neither MSA or Status can be less than 0 and the CompletionDte must be in a date format.  Send an MSA and Status greater than or equal to 0 and try again...");
                }

                var results = getData.filterData(dataParams);
                if (results.Count == 0)
                {
                    return NotFound();
                }

                return Ok(results);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, "Unable to return any data due to an unknown error in the API...");
            }
        }

        //[Route("api/SimpleOrderSearch/{OrderID}/{CompletionDte}")]
        //[HttpGet]
        //public IHttpActionResult GetByMSAStatus(long OrderID, DateTime CompletionDte)
        //{
        //    try
        //    {
        //        var validParams = getData.ValidateParams(OrderID, -1 , -1, CompletionDte);
        //        if (!validParams)
        //        {
        //            return BadRequest("Neither MSA or Status can be less than 0 and the CompletionDte must be in a date format.  Send an MSA and Status greater than or equal to 0 and try again...");
        //        }

        //        var results = getData.filterData(OrderID, -1, -1, CompletionDte);
        //        if (results.Count == 0)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(results);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, "Unable to return any data due to an unknown error in the API...");
        //    }
        //}

        //[Route("api/SimpleOrderSearch/{MSA}/{Status}/{CompletionDte}")]
        //[HttpGet]
        //public IHttpActionResult GetByMSAStatus(int MSA, int Status, DateTime CompletionDte)
        //{
        //    try
        //    {
        //        var validParams = getData.ValidateParams(-1, MSA, Status, CompletionDte);
        //        if (!validParams)
        //        {
        //            return BadRequest("Neither MSA or Status can be less than 0 and the CompletionDte must be in a date format.  Send an MSA and Status greater than or equal to 0 and try again...");
        //        }

        //        var results = getData.filterData(-1, MSA, Status, CompletionDte);
        //        if (results.Count == 0)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(results);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, "Unable to return any data due to an unknown error in the API...");
        //    }
        //}
    }
}
