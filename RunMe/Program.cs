using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunMe
{
    class Program
    {
        static void Main(string[] args)
        {
            Deva();
            while (true)
            {
                try
                {
                    Console.WriteLine("输入要存入的key");

                    string key = Console.ReadLine();
                    Console.WriteLine("输入要存入的Value");

                    string value = Console.ReadLine();

                    using (var sentinel = new CSRedis.RedisSentinelManager())
                    {
                        sentinel.Add("172.20.1.2", 26379);
                        sentinel.Add("172.20.1.3", 26379);
                        sentinel.Add("172.20.1.4", 26379);
                        
                        sentinel.Connected += (s, e) =>
                        {
                            sentinel.Call(x => x.Auth("hVrSF2plgMOradAulChe"));

                        };
                
                        sentinel.Connect("webcache",20000);
                
                        var client1 = sentinel.Call(x => x.Set(key,value));
                
                    }
                    
                    Console.WriteLine("输入要获取的key");
                    key = Console.ReadLine();
                    using (var sentinel = new CSRedis.RedisSentinelManager())
                    {
                        sentinel.Add("172.20.1.2", 26379);
                        sentinel.Add("172.20.1.3", 26379);
                        sentinel.Add("172.20.1.4", 26379);
                        
                        sentinel.Connected += (s, e) =>
                        {
                            sentinel.Call(x => x.Auth("hVrSF2plgMOradAulChe"));

                        };
                
                        sentinel.Connect("webcache",20000);
                
                        var client2 = sentinel.Call(x => x.Get(key));
                
                        Console.WriteLine(client2);
                    }
                }catch(Exception ex)
                {
                    StringBuilder sb = new StringBuilder();
                    GetAllMsg(ex, ref sb);
                    Console.WriteLine(sb.ToString());
                    Console.ReadLine();
                    Console.WriteLine(ex.StackTrace);
                    
                }
            }
                
        }
        public static void GetAllMsg(Exception ex,ref StringBuilder sb)
        {
            if (ex == null)
            {
                return;
            }
            sb.AppendLine(ex.Message);
            GetAllMsg(ex.InnerException, ref sb);
        }
        public static void Deva()
        {
            using (var sentinel = new CSRedis.RedisSentinelManager())
            {
                sentinel.Add("172.20.9.202", 26379);
                sentinel.Connected += (s, e) => sentinel.Call(x => x.Auth("easido"));
                sentinel.Connect("webcache");
                var client1 = sentinel.Call(x => x.Set("ensleep", "hi"));
                var client2 = sentinel.Call(x => x.Get("ensleep"));
                Console.WriteLine(client1);
                Console.WriteLine(client2);
            }
            Console.ReadLine();
        }
        public static void Release()
        {
            using (var sentinel = new CSRedis.RedisSentinelManager())
            {
                Console.WriteLine(1);
                sentinel.Add("172.20.1.2", 26379);
                sentinel.Add("172.20.1.3", 26379);
                sentinel.Add("172.20.1.4", 26379);

                Console.WriteLine(2);
                sentinel.Connected += (s, e) =>
                {
                    Console.WriteLine(3);
                    sentinel.Call(x => x.Auth("hVrSF2plgMOradAulChe"));
                    Console.WriteLine(4);

                };
                
                Console.WriteLine(5);
                sentinel.Connect("webcache",20000);
                Console.WriteLine(6);
                
                var client1 = sentinel.Call(x => x.Set("ensleep", "hi"));
                
                var client2 = sentinel.Call(x => x.Get("ensleep"));
                
                Console.WriteLine(15);
                Console.WriteLine(client1);
                Console.WriteLine(client2);
            }
            Console.ReadLine();
        }
    }
}
