using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atata.Configuration.Json;

namespace YoutubeAtata.Configs
{
    class AppConfig: JsonConfig<AppConfig>
    {
        public string ScreenShotFileOutput { get; set; }

        public string RtbConnectionString { get; set; }
    }
}
