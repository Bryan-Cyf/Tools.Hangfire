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
        /// һ��������
        /// </summary>
        [HttpGet]
        public void OnceTask()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Simple!"));
        }

        /// <summary>
        /// �ӳ�����
        /// </summary>
        [HttpGet]
        public void DelayedTask()
        {
            BackgroundJob.Schedule(() => Console.WriteLine("Reliable!"), TimeSpan.FromDays(7));
        }

        /// <summary>
        /// ����������
        /// </summary>
        [HttpGet]
        public void RecurringTask()
        {
            RecurringJob.AddOrUpdate("test1", () => Console.WriteLine("Transparent!"), Cron.Minutely, queue: "test1");
            //����ִ����������
            RecurringJob.Trigger("test1");
        }
    }

    /// <summary>
    /// �Զ�ע������������
    /// </summary>
    public class OrderReccurring : AutoReccurringBase
    {
        public override string Name => "Order";

        public override string Cron => Hangfire.Cron.Minutely();

        /// <summary>
        /// ָ��ִ�ж��� Ĭ��defualt
        /// </summary>
        /// <returns></returns>
        [Queue("schdule")]
        public override Task ExcuteAsync()
        {
            Console.WriteLine("Transparent!");
            return Task.CompletedTask;
        }
    }
}