using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Model
{
    /// <summary>
    /// Omotac kojim saljem trazene podatke, informaciju o uspjehu i prigodnu poruku
    /// Wrapper for requested data, success information and success or fail message
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }

        public bool Success { get; set; } = true;

        public string? Message { get; set; } = null;
    }
}
