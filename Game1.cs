using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace spatial_hash
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    private SpatialHash spatialHash;
    private List<Box> entities;

    private Texture2D recTexture2D;
    private Random random;

    private FrameCounter _frameCounter = new FrameCounter();
    private SpriteFont spriteFont;        

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      //graphics.GraphicsDevice.Viewport.
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
      // TODO: Add your initialization logic here
      spatialHash = new SpatialHash(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height, 32);
      random = new Random();
      entities = new List<Box>();
      addEntities(10);
      base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);
      recTexture2D = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
      recTexture2D.SetData(new[] { Color.White });
      spriteFont = Content.Load<SpriteFont>("SpriteFont1"); 

      // TODO: use this.Content to load your game content here
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// </summary>
    protected override void UnloadContent()
    {
      // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      if (Keyboard.GetState().IsKeyDown(Keys.A))
        addEntities(10);

      // TODO: Add your update logic here

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.Black);

      spriteBatch.Begin();

      drawGrid();

      foreach (var box in entities)
      {
        box.update((float)gameTime.ElapsedGameTime.TotalSeconds);
        spatialHash.updateBody(box);

        Color color;
        if (spatialHash.isBodySharingAnyCell(box))
        {
          color = Color.Red;
        }
        else
        {
          color = box.color;
        }
        drawRect((int)box.x, (int)box.y, (int)box.width, (int)box.height, 1, color);
      }
      
      var fps = string.Format("FPS: {0} Box: {1}", _frameCounter.AverageFramesPerSecond, entities.Count);
      _frameCounter.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
      spriteBatch.DrawString(spriteFont, fps, new Vector2(1, 1), Color.White);


      spriteBatch.End();


      base.Draw(gameTime);
    }

    private void drawRect(int x, int y, int widht, int height, int thicknessOfBorder, Color borderColor)
    {

      Rectangle rectangleToDraw = new Rectangle(x, y, widht, height);

      // Draw top line
      spriteBatch.Draw(recTexture2D, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

      // Draw left line
      spriteBatch.Draw(recTexture2D, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

      // Draw right line
      spriteBatch.Draw(recTexture2D, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                      rectangleToDraw.Y,
                                      thicknessOfBorder,
                                      rectangleToDraw.Height), borderColor);
      // Draw bottom line
      spriteBatch.Draw(recTexture2D, new Rectangle(rectangleToDraw.X,
                                      rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                      rectangleToDraw.Width,
                                      thicknessOfBorder), borderColor);

    }

    private void drawGrid(bool onlyDrawOccupiedCells = true)
    {
      if (onlyDrawOccupiedCells)
      {
        for (int i = 0; i < spatialHash.grid.Count; i++)
        {
          if (spatialHash.grid[i].Count > 0)
          {
            float x = (float)((decimal)(i % spatialHash.gridWidth) * spatialHash.cellSize);
            float y = (float)(Math.Floor((decimal)i / spatialHash.gridWidth) * spatialHash.cellSize);

            Color color;
            if (spatialHash.grid[i].Count == 1)
            {
              color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
            }
            else
            {
              color = new Color(0.7f, 0.4f, 0.4f, 0.6f);
            }

            drawRect((int)x, (int)y, spatialHash.cellSize, spatialHash.cellSize, 1, color);
          }
        }
      }
      else
      {
        //TODO
      }
    }

    private void addEntities(int totalToAdd)
    {
      for (int i = 0; i < totalToAdd; i++)
      {
        var box = new Box(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height, random);
        spatialHash.addBody(box);
        entities.Add(box);
      }
    }

  }
}
