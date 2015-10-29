using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spatial_hash
{
  public static class ExtensionMethods
  {
    public static float NextFloat(this Random random)
    {
      double mantissa = (random.NextDouble() * 2.0) - 1.0;
      double exponent = Math.Pow(2.0, random.Next(-126, 128));
      return (float)(mantissa * exponent);
    }

  }
}
