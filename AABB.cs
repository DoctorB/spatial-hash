using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace spatial_hash
{
  class AABB
  {
    public Vector2 min;
    public Vector2 max;

    public AABB()
    {
      min = new Vector2();
      max = new Vector2();
    }
  }
}
