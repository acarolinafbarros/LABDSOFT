using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAM.Controllers.LaboratorioController
{
    [Produces("application/json")]
    [Route("api/PedidoAnalises")]
    public class PedidoAnalisesController : Controller
    {
        // GET: api/PedidoAnalises
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PedidoAnalises/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/PedidoAnalises
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }
        
        // PUT: api/PedidoAnalises/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
    }
}
