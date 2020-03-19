using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class COB_Run
    {
        public int COB_RunId { get; set; }
        public DateTime RunningDate { get; set; }
        public bool COB_done { get; set; }
        public bool IsRunning { get; set; }
        public bool IsCurrentDate { get; set; }
    }
}
