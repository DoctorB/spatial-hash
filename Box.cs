using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spatial_hash
{
  class Box : Body
  {

    public float width;
    public float height;
    public Color color = Color.White;

    private float dirX;
    private float dirY;
    private float speed;

    private Random rnd;

    private int screenW;
    private int screenH;

    public Box(int _screenW, int _screenH, Random _rndRandom)
      : base()
    {
      screenW = _screenW;
      screenH = _screenH;

      rnd = _rndRandom;

      x = randomBetween(0, screenW);
      y = randomBetween(0, screenH);
      width = randomBetween(10, 50);
      height = randomBetween(10, 50);
      speed = randomBetween(50, 250);
      dirX = rnd.NextFloat() < 0.5d ? -1 : 1;
      dirY = rnd.NextFloat() < 0.5d ? -1 : 1;

      aabb = new AABB();
      updateAABB();
    }

    public void update(float dt)
    {
      // wander about
      if (rnd.NextFloat() > 0.99)
      {
        dirX = -dirX;
        speed = randomBetween(50, 250);
      }

      if (rnd.NextFloat() > 0.99)
        dirY = -dirY;

      // update position
      x += (dirX * speed * dt);
      y += dirY * speed * dt;

      // bounds checks
      if (x < 0 || x > screenW)
        dirX = -dirX;

      if (y < 0 || y > screenH)
        dirY = -dirY;

      updateAABB();
    }

    private void updateAABB()
    {
      aabb.min.X = x;
      aabb.min.Y = y;
      aabb.max.X = x + width;
      aabb.max.Y = y + height;
    }

    /** Return a random float between 'from' and 'to', inclusive. */
    public float randomBetween(int from, int to)
    {
      return rnd.Next(from, to);
      //return from + ((to - from) * rnd.NextFloat());
    }

  }
}
