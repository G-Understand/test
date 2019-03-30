using System;
using System.Linq;
using System.Timers;

namespace tryMJcard
{
    class TimerManage : Timer
    {
        public object parameter { get; set; }
        public void SetParameter(object value)
        {
            parameter = value;
        }
    }
}
