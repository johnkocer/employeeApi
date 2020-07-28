using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebApi
{
  public static class StringExt
  {
    public static bool IsNumeric(this string text)
    {
      double test;
      return double.TryParse(text, out test);
    }
  }
}
