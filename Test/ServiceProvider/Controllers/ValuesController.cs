using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vit.Core.Util.ComponentModel.Data;
using Vit.Extensions;

namespace ServiceProvider.Controllers
{
    [Route("ServiceProvider/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        #region model
        public class UserInfo 
        {
            public string name;
            public int? age;
            public List<string> callStack;
            public object headers;
        }
        #endregion



        #region (x.1)Get
        // GET api/values/5
        [HttpGet("item")]
        public ApiReturn<object> Get([FromQuery]string name, [FromQuery]int age, [FromQuery] string callStack)
        {
            var userInfo= new UserInfo { name = name, age = age , callStack =  callStack.Deserialize<List<string>>() };

            if (userInfo.callStack == null) 
            {
                userInfo.callStack = new List<string>();
            }
            userInfo.callStack.Add("ServiceProvider_" + DateTime.Now.ToString("HH:mm:ss.fff"));
            userInfo.headers = Request.Headers;
            return userInfo;
        }

        #endregion


        #region (x.2)Post
    
        [HttpPost("item")]
        public ApiReturn<UserInfo> Post([FromBody]UserInfo userInfo)
        {
            if (userInfo.callStack == null)
            {
                userInfo.callStack = new List<string>();
            }
            userInfo.callStack.Add("ServiceProvider_" + DateTime.Now.ToString("HH:mm:ss.fff"));
            userInfo.headers = Request.Headers;
            return userInfo;
        }

        #endregion
         
    }
}
