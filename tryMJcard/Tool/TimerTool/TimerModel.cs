using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.TimerTool
{
    /// <summary>
    /// 委托方案
    /// </summary>
    /// <param name="obj"></param>
    internal delegate void Function();

    internal class TimerModel
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public int startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public int endTime { get; set; }

        /// <summary>
        /// 每次指定的间隔是否结束
        /// </summary>
        public bool autoReset { get; set; }

        /// <summary>
        /// 任务
        /// </summary>
        public Function task { get; set; }

        /// <summary>
        /// 是否结束
        /// </summary>
        public bool isEnd { get; set; }

        /// <summary>
        /// 需要传参时使用
        /// </summary>
        public object value { get; set; }

        /// <summary>
        /// 是否开始
        /// </summary>
        public bool isStart { get; set; }

        /// <summary>
        /// 房间号（标识房间）
        /// </summary>
        public int roomId { get; set; }

        /// <summary>
        /// 标记
        /// </summary>
        public bool mark { get; set; }

        public TimerModel()
        {

        }

        public TimerModel(int _endTime, bool _autoReset, Function _task, int _roomId)
        {
            endTime = _endTime;
            autoReset = _autoReset;
            task = _task;
            roomId = _roomId;
        }

        public TimerModel(bool _autoReset, Function _task, int _roomId)
        {
            autoReset = _autoReset;
            task = _task;
            roomId = _roomId;
        }

        public TimerModel(bool _autoReset, List<Function> _taskList, int _roomId)
        {
            autoReset = _autoReset;
            roomId = _roomId;
            for (int i = 0; i < _taskList.Count; i++)
            {
                task += _taskList[i];
            }
        }

        /// <summary>
        /// 开始任务
        /// </summary>
        /// <param name="obj"></param>
        public void StartTesk(object obj)
        {
            try
            {
                //System.Console.WriteLine("执行");
                task();
                isStart = false;
            }
            catch (System.Exception ex)
            {
                //LogManagerSimple.WriteLog_ByDate(ex.Message + "" + ex.StackTrace, 404);
            }
        }

        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="_endTime"></param>
        /// <param name="_autiReset"></param>
        public void SetTimer(int _endTime, bool _autiReset)
        {
            endTime = _endTime;
            autoReset = _autiReset;
        }

        /// <summary>
        /// 开始执行
        /// </summary>
        public void Start()
        {
            isStart = true;
            isEnd = false;
            TimerManager.GetInstance().AddTimerModel(this);
        }

        /// <summary>
        /// 停止执行
        /// </summary>
        public void Stop()
        {
            isEnd = true;
            isStart = false;
            TimerManager.GetInstance().RemoveTimerModel();
        }
    }
}
