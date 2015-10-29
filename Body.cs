using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spatial_hash
{
  class Body
  {
    public float x { get; set; }
    public float y { get; set; }
    public AABB aabb { get; set; }

    public List<int> gridIndex { get; set; }

    public Body()
    {
      gridIndex = new List<int>();
    }
  }
}
