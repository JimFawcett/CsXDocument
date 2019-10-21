using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoExtensions
{
  public static class DemoExtensions
  {
    public static void title(this string aString, char underline='-')
    {
      Console.Write("\n  {0}", aString);
      Console.Write("\n {0}", new string(underline, aString.Length + 2));
    }
  }
#if (TEST_DEMOEXTENSIONS)
  class TestDemoExtensions
  {
    static void Main(string[] args)
    {
      "Test of DemoExtensions".title('=');
      Console.Write("\n\n");
    }
  }
#endif
}
