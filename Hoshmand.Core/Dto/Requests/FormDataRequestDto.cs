using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoshmand.Core.Dto.Requests
{
    public class FormDataRequestDto
    {
        public string FormFieldName { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public StreamContent ContentStream { get; set; }
    }
}
