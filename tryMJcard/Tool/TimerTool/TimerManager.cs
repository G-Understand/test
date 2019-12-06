﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace tryMJcard.Tool.TimerTool
{
    /// <summary>
    /// 该定时器为全局，只每秒执行。
    /// 通过任务池来处理任务，对任务时间管理。
    /// </summary>
    internal class TimerManager
    {
        /// <summary>
        /// 定时器
        /// </summary>
        private Timer iTimer;

        /// <summary>
        /// 启动时间
        /// </summary>
        private TimeSpan dueTime;

        /// <summary>
        /// 方法调用间隔
        /// </summary>
        private TimeSpan period;

        /// <summary>
        /// 委托
        /// </summary>
        private TimerCallback timerDelegate;

        /// <summary>
        /// 任务池
        /// </summary>
        private List<TimerModel> modelList = new List<TimerModel>();

        /// <summary>
        /// 静态实例
        /// </summary>
        private static readonly TimerManager self = new TimerManager();

        /// <summary>
        /// 执行时间
        /// </summary>
        private int time = 0;

        /// <summary>
        /// 构造函数（禁止添加更多响应行为）
        /// </summary>
        private TimerManager()
        {
            timerDelegate = new TimerCallback(CheckStatus);
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static TimerManager GetInstance()
        {
            return self;
        }

        /// <summary>
        /// 行为(禁止添加新线程)
        /// </summary>
        /// <param name="nObject"></param>
        private void CheckStatus(object nObject)
        {
            time++;
            new Thread(Execute).Start(nObject);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        private void Execute(object obj)
        {
            lock (modelList)
            {
                for (int i = 0; i < modelList.Count; i++)
                {
                    TimerModel model = modelList[i];
                    if (model.mark)
                    {
                        int sidjsd = 0;
                    }
                    if (!model.isEnd && time - model.startTime >= model.endTime)
                    {
                        model.startTime = time;
                        if (model.autoReset)
                        {
                            model.isEnd = true;
                        }
                        ThreadPool.QueueUserWorkItem(new WaitCallback(model.StartTesk));
                        //new Thread(model.StartTesk).Start(obj);
                    }
                }
                modelList.RemoveAll(d => d.isEnd);
            }
            //应该从数据库获得Paper对象的集合，这里简略
            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            ////执行数据更新，这里省略
        }

        /// <summary>
        /// 移除TimerModel
        /// </summary>
        public void RemoveTimerModel()
        {
            modelList.RemoveAll(d => d.isEnd);
        }

        /// <summary>
        /// 添加时间成员
        /// </summary>
        /// <param name="model"></param>
        public void AddTimerModel(TimerModel model)
        {
            model.startTime = time;
            modelList.Add(model);

            int a = 1;

        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            dueTime = TimeSpan.FromSeconds(0);
            period = TimeSpan.FromSeconds(1);
            iTimer = new Timer(timerDelegate, autoEvent, dueTime, period);

            //autoEvent.WaitOne(5000, false);
            iTimer.Change(dueTime, period);
        }

        /// <summary>
        /// 设置启动时间间隔
        /// </summary>
        /// <param name="days">天</param>
        /// <param name="hours">小时</param>
        /// <param name="minutes">分钟</param>
        /// <param name="seconds">秒</param>
        /// <param name="milisecond">毫秒</param>
        public void setDueTime(int days, int hours, int minutes, int seconds, int milisecond)
        {
            dueTime = new TimeSpan(days, hours, minutes, seconds, milisecond);
        }

        /// <summary>
        /// 设置回调时间间隔
        /// </summary>
        /// <param name="days">天</param>
        /// <param name="hours">小时</param>
        /// <param name="minutes">分钟</param>
        /// <param name="seconds">秒</param>
        /// <param name="milisecond">毫秒</param>
        public void setPeriod(int days, int hours, int minutes, int seconds, int milisecond)
        {
            period = new TimeSpan(days, hours, minutes, seconds, milisecond);
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            iTimer.Dispose();
        }

        /// <summary>
        /// 执行一次
        /// </summary>
        public void ExcuteOneTime()
        {
            if (iTimer != null)
            {
                iTimer.Dispose();
            }
            //如果 period 为零 (0) 或 -1 毫秒，而且 dueTime 为正，则只会调用一次 callback；
            //计时器的定期行为将被禁用，但通过使用 Change 方法可以重新启用该行为。
            setDueTime(0, 0, 0, 0, 1);
            setPeriod(0, 0, 0, 0, -1);
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            iTimer = new Timer(timerDelegate, autoEvent, dueTime, period);
            iTimer.Change(dueTime, period);
        }
    }
}