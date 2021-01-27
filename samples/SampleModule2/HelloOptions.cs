using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Options;

namespace SampleModule2
{
    public class HelloOptions : IOptions<HelloOptions>
    {
        public string Content { get; set; }

        public HelloOptions Value => this;
    }
}
