using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RavenDBFirstSteps.Client.Model
{
    public class Tea
    {
        public String Id { get; set; }

        public String Name { get; set; }

        public TeaType TeaType { get; set; }

        public Double WaterTemp { get; set; }

        public Int32 SleepTime { get; set; }
    }
}
