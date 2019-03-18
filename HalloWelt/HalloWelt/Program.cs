using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace HalloWelt
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hallo Welt!");

            LoadBooks().items.Select(x => x.volumeInfo)
                             .Where(x=>x.title.StartsWith("b"))
                             .ToList()
                             .ForEach(x => Console.WriteLine(x.title));

            //foreach (var item in LoadBooks().items)
            //{
            //    Console.WriteLine(item.volumeInfo.title);
            //}

            object o = new Volumeinfo() { title = "testbuch!" };
            o = new PlatformNotSupportedException();
            //casting = doof
            {

                if (o is Volumeinfo)
                {
                    Volumeinfo vii = (Volumeinfo)o;
                    Console.WriteLine(vii.title);
                }
            }

            //boxing = alt aber OK
            Volumeinfo vi = o as Volumeinfo;
            PlatformNotSupportedException pe = o as PlatformNotSupportedException;

            if (vi != null)
            {
                Console.WriteLine(vi.title);
            }

            //pattern match = neu und cool!
            if (o is Volumeinfo viiii)
            {
                Console.WriteLine(viiii);
            }

            string text = "Heute ist: " + DateTime.Now.DayOfWeek + " der " + DateTime.Now.Day;
            string text2 = string.Format("Heute ist {0} der {1:00}", DateTime.Now.DayOfWeek, DateTime.Now.Day);
            string text3 = $"Heute ist {DateTime.Now.DayOfWeek} der {DateTime.Now.Day:00}";
            


            Console.WriteLine("Ende");
            Console.ReadLine();
        }


        public static BookResults LoadBooks()
        {
            var url = "https://www.googleapis.com/books/v1/volumes?q=csharp";
            using (var wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                var json = wc.DownloadString(url);
                return JsonConvert.DeserializeObject<BookResults>(json);
            }//wc.Dispose();
        }
    }
}
