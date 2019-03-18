using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloWelt
{
    delegate void EinfacherDelegate();
    delegate void DelegateMitParameter(string msg);
    delegate long CalcDelegate(int a, int b);

    class HalloDelegate
    {
        public HalloDelegate()
        {
            EinfacherDelegate meinDings = EinfacheMethode;
            Action meinDingsAlsAction = EinfacheMethode;
            Action meinDingsAlsAction2 = delegate () { Console.WriteLine("Hallo"); };
            Action meinDingsAlsAction3 = () => { Console.WriteLine("Hallo"); };
            Action meinDingsAlsAction4 = () => Console.WriteLine("Hallo");

            DelegateMitParameter dingsMitPara = MethodeMitPara;
            Action<string> dingsMitParaAlsAction = MethodeMitPara;
            Action<string> dingsMitParaAlsAction2 = delegate (string txt) { Console.WriteLine(txt); };
            Action<string> dingsMitParaAlsAction3 = (string txt) => Console.WriteLine(txt);
            Action<string> dingsMitParaAlsAction4 = x => Console.WriteLine(x);

            CalcDelegate calcDele = Sum;
            Func<int, int, long> calcDeleFunc = Sum;
            Func<int, int, long> calcDeleFunc2 = delegate (int a, int b) { return a + b; };
            Func<int, int, long> calcDeleFunc3 = (a, b) => { return a + b; };
            Func<int, int, long> calcDeleFunc4 = (a, b) => a + b;

            List<string> text = new List<string>();
            var res = text.Where(Filter);
            var res2 = text.Where(x => x.StartsWith("b"));
        }

        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return false;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        private void MethodeMitPara(string txt)
        {
            Console.WriteLine(txt);
        }

        private void EinfacheMethode()
        {
            Console.WriteLine("Hallo");
        }
    }
}
