using CustomIoC;
using CustomIoC.LifeTimeManager;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    public interface ILanguage
    {
        public string GetContent();
    }
    public class Chinese : ILanguage
    {
        public string GetContent()
        {
            return "学习中文";
        }
    }

    public class English : ILanguage
    {
        public string GetContent()
        {
            return "Learning English";
        }
    }

    public class Learn
    {
        private ILanguage Language { get; set; }
         
        //标记特性
        [Myinjection]
        public Learn(ILanguage language)
        {
            Language = language;
        }

        public void Read()
        {
            Console.WriteLine(Language.GetContent());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var container = new MyContainer();
            //依赖注入 瞬时默认
            container.RegisterType<ILanguage, Chinese>();
            var language1 = container.Resolve<ILanguage>();
            var language2 = container.Resolve<ILanguage>();
            Console.WriteLine($"-----------------------瞬时默认-------------------------");
            Console.WriteLine($"language1 与 language2：{ReferenceEquals(language1, language2)}\r\n");

            //线程单例
            Console.WriteLine($"-----------------------线程单例-------------------------");
            container.RegisterType<ILanguage, Chinese>(TypeLifeTime.PerThread);
            ILanguage tlanguage1 = null;
            var task1 = Task.Run(() => {
                tlanguage1 = container.Resolve<ILanguage>();
                Console.WriteLine($"tlanguage1 线程id：{Thread.CurrentThread.ManagedThreadId}");
            });
            ILanguage tlanguage2 = null;
            ILanguage tlanguage3 = null;
            var task2 = Task.Run(() => {
                tlanguage2 = container.Resolve<ILanguage>();
                tlanguage3 = container.Resolve<ILanguage>();
                Console.WriteLine($"tlanguage2 线程id：{Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"tlanguage3 线程id：{Thread.CurrentThread.ManagedThreadId}");
            });

            task1.Wait();
            task2.Wait();
            Console.WriteLine($"tlanguage1 与 tlanguage2 ：{ReferenceEquals(tlanguage1, tlanguage2)}");
            Console.WriteLine($"tlanguage2 与 tlanguage3 ：{ReferenceEquals(tlanguage2, tlanguage3)}\r\n");

            //单例模式
            container.RegisterType<ILanguage, Chinese>(TypeLifeTime.Singleton);
            var slanguage1 = container.Resolve<ILanguage>();
            var slanguage2 = container.Resolve<ILanguage>();
            Console.WriteLine($"-----------------------单例模式-------------------------");
            Console.WriteLine($"slanguage1 与 slanguage2：{ReferenceEquals(slanguage1, slanguage2)}");

            Console.Read();
        }
    }
}
