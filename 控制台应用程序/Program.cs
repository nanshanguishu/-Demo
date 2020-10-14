using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 控制台应用程序
{
    class Program
    {
        static void Main(string[] args)
        {

            DuoXianChengJT();//多线程调用静态方法
            DuoXianChengSL();//多线程调用实例方法
            DuoXianChengWTorLambda();//多线程调用委托和lambda表达式
            DuoXianChengYCSWeiTuo();//多线程有参数
            DuoXianChengFangFa();//多线程常用到的方法
            DuoXianChengQtxcAndHtxc();//前台线程和后台线程
            SaleBook();//多线程执行计算图书库存（不做线程同步）
            SaleBook1();//多线程执行计算图书库存（采用线程同步）

            Console.ReadKey();

        }
        #region 折叠
        /// <summary>
        /// 多线程运行实例方法
        /// </summary>
        static void DuoXianChengSL()
        {
            Test a = new Test();
            Thread thread = new Thread(new ThreadStart(a.ShuChuStringShiLi));
            Program p = new Program();
            Thread thread1 = new Thread(new ThreadStart(p.ShuChuStringShiLi));
            thread1.Start();
            thread.Start();
        }
        /// <summary>
        /// 多线程调用静态方法
        /// </summary>
        static void DuoXianChengJT()
        {
            Thread thread = new Thread(new ThreadStart(ShuChuString0));
            Thread thread1 = new Thread(new ThreadStart(ShuChuString1));
            Thread thread2 = new Thread(new ThreadStart(ShuChuString2));
            Thread thread3 = new Thread(new ThreadStart(ShuChuString3));
            Thread thread4 = new Thread(new ThreadStart(ShuChuString4));
            Thread thread5 = new Thread(new ThreadStart(ShuChuString5));
            thread.Start();
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();


            Console.ReadKey();
        }
        /// <summary>
        /// 多线程有参数委托
        /// </summary>
        static void DuoXianChengYCSWeiTuo()
        {
            Thread thread = new Thread(new ParameterizedThreadStart(Thread1));
            thread.Start("输出多线程有参数委托");
        }
        static void DuoXianChengWTorLambda()
        {
            Thread thread = new Thread(new ThreadStart(delegate () { Console.WriteLine("输出委托"); }));
            Thread thread1 = new Thread(new ThreadStart(() => { Console.WriteLine("输出lambda表达式"); }));
            thread.Start();
            thread1.Start();
        }
        /// <summary>
        /// 线程常用的方法
        /// </summary>
        static void DuoXianChengFangFa()
        {
            //获取正在运行的线程
            Thread thread = Thread.CurrentThread;
            //设置线程的名字
            thread.Name = "主线程";
            //获取当前线程的唯一标识符
            int id = thread.ManagedThreadId;
            //获取当前线程的状态
            ThreadState state = thread.ThreadState;
            //获取当前线程的优先级
            ThreadPriority priority = thread.Priority;
            string strMsg = string.Format("Thread ID:{0}\n" + "Thread Name:{1}\n" +
                "Thread State:{2}\n" + "Thread Priority:{3}\n", id, thread.Name,
                state, priority);

            Console.WriteLine(strMsg);
        }
        /// <summary>
        ///     前台线程和后台线程
        /// </summary>
        static void DuoXianChengQtxcAndHtxc()
        {
            //演示前台、后台线程
            BackGroundTest background = new BackGroundTest(10);
            //创建前台线程
            Thread fThread = new Thread(new ThreadStart(background.RunLoop));
            //给线程命名
            fThread.Name = "前台线程";


            BackGroundTest background1 = new BackGroundTest(20);
            //创建后台线程
            Thread bThread = new Thread(new ThreadStart(background1.RunLoop));
            bThread.Name = "后台线程";
            //设置为后台线程
            bThread.IsBackground = true;

            //启动线程
            fThread.Start();
            bThread.Start();
        }
        /// <summary>
        /// 多线程执行计算图书库存（不做线程同步）
        /// </summary>
        static void SaleBook()
        {
            BookShop book = new BookShop();
            //创建两个线程同时访问Sale方法
            Thread t1 = new Thread(new ThreadStart(book.Sale));
            Thread t2 = new Thread(new ThreadStart(book.Sale));
            //启动线程
            t1.Start();
            t2.Start();
            Console.ReadKey();
        }

        /// <summary>
        /// 多线程执行计算图书库存（采用线程同步）
        /// </summary>
        static void SaleBook1()
        {
            BookShop1 book = new BookShop1();
            //创建两个线程同时访问Sale方法
            Thread t1 = new Thread(new ThreadStart(book.Sale));
            Thread t2 = new Thread(new ThreadStart(book.Sale));
            //启动线程
            t1.Start();
            t2.Start();
            Console.ReadKey();
        }
        static void Thread1(object obj)
        {
            Console.WriteLine(obj);
        }
        static void ShuChuString0()
        {
            Console.WriteLine("输出0；");
        }
        static void ShuChuString1()
        {
            Console.WriteLine("输出1；");
        }
        static void ShuChuString2()
        {
            Console.WriteLine("输出2；");
        }
        static void ShuChuString3()
        {
            Console.WriteLine("输出3；");
        }

        static void ShuChuString4()
        {
            Console.WriteLine("输出4；");
        }
        static void ShuChuString5()
        {
            Console.WriteLine("输出5；");
        }
        class Test
        {
            public void ShuChuStringShiLi()
            {
                Console.WriteLine("输出实例方法");
            }
        }

        public void ShuChuStringShiLi()
        {
            Console.WriteLine("输出实例方法");
        }
        public static void sqlConTest()
        {
            string sqlstring = "Data Source=bipvmscm.rokin.cn,16172;Initial Catalog=bipvmscm;Persist Security Info=True;User ID=pvmbangda;Password=aMeOx$kC6FrB";
            SqlConnection Conn = new SqlConnection(sqlstring);
            DataTable dt = new DataTable();
            Conn.Open();
            string sqlTransferRate = "select * from V_TransferRate_BDLF where 1=2";
            SqlDataAdapter adapterTransferRatenew = new SqlDataAdapter(sqlTransferRate, Conn);
            DataSet dsTransferRatenew = new DataSet();
            SqlCommandBuilder cbTransferRatenew = new SqlCommandBuilder(adapterTransferRatenew);
            adapterTransferRatenew.Fill(dsTransferRatenew, "V_TransferRate_BDLF");
            DataRow TransferRateRow = dsTransferRatenew.Tables[0].NewRow();
            TransferRateRow["total"] = 1;
            TransferRateRow["transfer"] = 1;
            TransferRateRow["transferRatio"] = 1;
            TransferRateRow["WriteTime"] = DateTime.Now;
            TransferRateRow["operatorName"] = "自动导入";
            TransferRateRow["operationTime"] = DateTime.Now;
            TransferRateRow["state"] = 1;
            TransferRateRow["operationCompanyId"] = 2;
            TransferRateRow["PW"] = "3g2!hEp*TP5U^";
            DataRow TransferRateRow1 = dsTransferRatenew.Tables[0].NewRow();
            TransferRateRow1["total"] = 1;
            TransferRateRow1["transfer"] = 1;
            TransferRateRow1["transferRatio"] = 1;
            TransferRateRow1["WriteTime"] = DateTime.Now;
            TransferRateRow1["operatorName"] = "自动导入";
            TransferRateRow1["operationTime"] = DateTime.Now;
            TransferRateRow1["state"] = 1;
            TransferRateRow1["operationCompanyId"] = 2;
            TransferRateRow1["PW"] = "3g2!hEp*TP5U^";
            dsTransferRatenew.Tables["V_TransferRate_BDLF"].Rows.Add(TransferRateRow.ItemArray);
            dsTransferRatenew.Tables["V_TransferRate_BDLF"].Rows.Add(TransferRateRow1.ItemArray);

            dt = dsTransferRatenew.Tables[0];
            using (SqlDataAdapter da = new SqlDataAdapter())//创建数据适配器对象
            {
                SqlCommand command = new SqlCommand("INSERT INTO V_TransferRate_BDLF " +//创建SQL命令对象
                "VALUES (@total,@transfer,@transferRatio,@WriteTime,@operatorName,@operationTime,@state,@operationCompanyId,@PW)", Conn);
                command.Parameters.Add(@"total", SqlDbType.Decimal, 10, "total");
                command.Parameters.Add(@"transfer", SqlDbType.Decimal, 10, "transfer");
                command.Parameters.Add(@"transferRatio", SqlDbType.Decimal, 10, "transferRatio");
                command.Parameters.Add(@"WriteTime", SqlDbType.DateTime, 10, "WriteTime");
                command.Parameters.Add(@"operatorName", SqlDbType.VarChar, 10, "operatorName");
                command.Parameters.Add(@"operationTime", SqlDbType.DateTime, 10, "operationTime");
                command.Parameters.Add(@"state", SqlDbType.Int, 10, "state");
                command.Parameters.Add(@"operationCompanyId", SqlDbType.Int, 10, "operationCompanyId");
                command.Parameters.Add(@"PW", SqlDbType.VarChar, 20, "PW");
                da.InsertCommand = command;//设置插入命令属性
                da.Update(dt);//同步数据
            }
        }

        public static void linqTest()
        {
            double[] radii = { 1, 2, 3 };

            // LINQ query using method syntax.
            IEnumerable<string> output =
                radii.Select(r => $"Area for a circle with a radius of '{r}' = {r * r * Math.PI:F2}");
            IEnumerable<string> output1 = radii.Select(r => $"{ r * r * Math.PI:F2}");
            /*
            // LINQ query using query syntax.
            IEnumerable<string> output =
                from rad in radii
                select $"Area for a circle with a radius of '{rad}' = {rad * rad * Math.PI:F2}";
            */

            foreach (string s in output)
            {
                Console.WriteLine(s);
            }

            foreach (string s in output1)
            {
                Console.WriteLine(s);
            }

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
        }
        public static void groupBy()
        {
            List<int> numbers = new List<int>() { 35, 44, 200, 84, 3987, 4, 199, 329, 446, 208 };

            IEnumerable<IGrouping<int, int>> query = from number in numbers
                                                     group number by number % 2;

            foreach (var group in query)
            {
                Console.WriteLine(group.Key == 0 ? "nEven numbers:" : "nOdd numbers:");
                foreach (int i in group)
                    Console.WriteLine(i);
            }
        }

        public static void linq_Concat()
        {
            Pet[] cats = GetCats();
            Pet[] dogs = GetDogs();
            var query = cats.Select(cat => new { mc = cat.Name, nl = cat.Age }).Concat(dogs.Select(dog => new
            { mc = dog.Name, nl = dog.Age }));
            var query1 = cats.Concat(dogs);
        }

        static Pet[] GetCats()
        {
            Pet[] cats = { new Pet { Name="Barley", Age=8 },
                       new Pet { Name="Boots", Age=4 },
                       new Pet { Name="Whiskers", Age=1 } };
            return cats;
        }

        static Pet[] GetDogs()
        {
            Pet[] dogs = { new Pet { Name="Bounder", Age=3 },
                       new Pet { Name="Snoopy", Age=14 },
                       new Pet { Name="Fido", Age=9 } };
            return dogs;
        }
        class Pet
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        class BackGroundTest
        {
            private int Count;
            public BackGroundTest(int count)
            {
                this.Count = count;
            }
            public void RunLoop()
            {
                //获取当前线程的名称
                string threadName = Thread.CurrentThread.Name;
                for (int i = 0; i < Count; i++)
                {
                    Console.WriteLine("{0}计数：{1}", threadName, i.ToString());
                    //线程休眠500毫秒
                    //Thread.Sleep(1000);
                }
                Console.WriteLine("{0}完成计数", threadName);

            }
        }

        class BookShop
        {
            //剩余图书数量
            public int num = 1;
            public void Sale()
            {
                int tmp = num;
                if (tmp > 0)//判断是否有书，如果有就可以卖
                {
                    Thread.Sleep(1000);
                    num -= 1;
                    Console.WriteLine("售出一本图书，还剩余{0}本", num);
                }
                else
                {
                    Console.WriteLine("没有了");
                }
            }
        }

        class BookShop1
        {
            //剩余图书数量
            public int num = 1;
            public void Sale()
            {
                //使用lock关键字解决线程同步问题
                lock (this)
                {
                    int tmp = num;
                    if (tmp > 0)//判断是否有书，如果有就可以卖
                    {
                        Thread.Sleep(1000);
                        num -= 1;
                        Console.WriteLine("售出一本图书，还剩余{0}本", num);
                    }
                    else
                    {
                        Console.WriteLine("没有了");
                    }
                }
            }
        } 
        #endregion


    }
   

    public static  class SessionValue
    {
        public static  string operationRole;
        public static  string operationCompanyId;
    }
}