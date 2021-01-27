using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Options;

namespace SampleModule2.Options.Hellos
{
    public class HelloOptions2 : IOptions<HelloOptions2>
    {
        public string Content { get; set; }

        public HelloOptions2 Value => this;
    }
}
