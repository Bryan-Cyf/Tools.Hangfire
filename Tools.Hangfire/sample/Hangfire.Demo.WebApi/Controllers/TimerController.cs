using Microsoft.AspNetCore.Mvc;
using Tools.Hangfire;

namespace Hangfire.Demo.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TimerController : ControllerBase
    {
        public TimerController()
        {

        }

        /// <summary>
        /// 一次性任务
        /// </summary>
        [HttpGet]
        public void OnceTask()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Simple!"));
        }

        /// <summary>
        /// 延迟任务
        /// </summary>
        [HttpGet]
        public void DelayedTask()
        {
            BackgroundJob.Schedule(() => Console.WriteLine("Reliable!"), TimeSpan.FromDays(7));
        }

        /// <summary>
        /// 周期性任务
        /// </summary>
        [HttpGet]
        public void RecurringTask()
        {
            RecurringJob.AddOrUpdate("test1", () => Console.WriteLine("Transparent!"), Cron.Minutely, queue: "test1");
            //立即执行周期任务
            RecurringJob.Trigger("test1");
        }
    }

    /// <summary>
    /// 自动注册周期性任务
    /// </summary>
    public class OrderReccurring : AutoReccurringBase
    {
        public override string Name => "Order";

        /// <summary>
        /// Cron表达式
        /// </summary>
        public override string Cron => Hangfire.Cron.Daily(1);

        /// <summary>
        /// 指定执行队列 默认default
        /// </summary>
        /// <returns></returns>
        [Queue("schedule")]
        public override Task ExcuteAsync()
        {
            Console.WriteLine("Transparent!");
            return Task.CompletedTask;
        }
    }
}