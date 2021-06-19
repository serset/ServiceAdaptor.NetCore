using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceProvider;

namespace ServiceConsumer.Controllers
{
    [Route("ServiceConsumer/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet("get")]
        public object Get()
        {
            var result = ServiceProviderService.GetItem(new Service.ServiceProvider.Contract.UserInfo { 
                name = "lith",
                age = 10,
                callStack=new List<string> {"ServiceConsumer_"+DateTime.Now.ToString("HH:mm:ss.fff") } 
            });
            return result;
        }

        // GET api/values
        [HttpGet("post")]
        public object Post()
        {
            var result = ServiceProviderService.PostApiReturnAsync(new Service.ServiceProvider.Contract.UserInfo
            {
                name = "lith" ,
                age = 10,
                callStack = new List<string> { "ServiceConsumer_" + DateTime.Now.ToString("HH:mm:ss.fff") }
            }).Result;
            return result;
        }



    }
}
