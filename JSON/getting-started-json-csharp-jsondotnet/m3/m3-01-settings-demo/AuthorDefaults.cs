using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_01_settings_demo
{
    class AuthorDefaults
    {
        public string name { get; set; }

        [DefaultValue(true)]
        public bool happy { get; set; }

        [DefaultValue(true)]
        public bool resting { get; set; }

        [DefaultValue(4)]
        public int courses { get; set; }

        [DefaultValue("Passionate about teaching")]
        public string state { get; set; }

    }
}
