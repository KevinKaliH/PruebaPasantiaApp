using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModel
{
    public class ResponseVM
    {

        public ResponseVM(bool _success, string _message, Object _data)
        {
            success = _success;
            message = _message;
            data = _data;
        }

        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
