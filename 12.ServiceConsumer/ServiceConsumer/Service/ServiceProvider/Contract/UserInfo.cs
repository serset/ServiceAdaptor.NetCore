 


using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.ServiceProvider.Contract
{

    public class UserInfo
    {
        public string name;
        public int? age;
        public List<string> callStack;
    }



}
