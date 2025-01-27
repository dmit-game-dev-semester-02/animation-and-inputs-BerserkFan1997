using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment_01;

public class Game1 : Game
{
    //Skeleton Spritesheet: https://astrobob.itch.io/animated-pixel-art-skeleton/download/eyJleHBpcmVzIjoxNzM3OTQ5MzM2LCJpZCI6OTE0NjQ4fQ%3d%3d.lFm51wE6ik4Gc6z%2b1u2ejg4a19U%3d

    //Knight Spritesheet: https://free-game-assets.itch.io/free-fantasy-knight/download/eyJleHBpcmVzIjoxNzM3OTUxNzQyLCJpZCI6MTgwMTcyMX0%3d.n8mly%2f%2f0fDR7VYvs%2b7YLx7rvGFo%3d
    //Foreground Image: https://www.cyberpunk.net/en/edgerunners
    private const int _WindowWidth = 925;
    private const int _WindowHeight = 518;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteBatch _knightAnimationSpriteBatch;

    private Texture2D _background;
    private Texture2D _foreground;

    private CelAnimationSequence _skeletonAnimation; //Moves in response to key input

    private CelAnimationSequence _knightAnimation;
    private CelAnimationPlayer _animationPlayer;
    private CelAnimationPlayer _knightAnimationPlayer;

    private Vector2 _spritePosition;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _knightAnimationSpriteBatch = new SpriteBatch(GraphicsDevice);
        _background = Content.Load<Texture2D>("background");
        _foreground = Content.Load<Texture2D>("foreground");

        Texture2D skeletonSpriteSheet = Content.Load<Texture2D>("SkeletonSpritesheet");
        Texture2D knightSpriteSheet = Content.Load<Texture2D>("KnightSpritesheet");

        _skeletonAnimation = new CelAnimationSequence(skeletonSpriteSheet, 64, 1 / 8f); //celwidth, celtime

        _knightAnimation = new CelAnimationSequence(knightSpriteSheet, 86, 1 / 8f);
        // TODO: use this.Content to load your game content here


        _animationPlayer = new CelAnimationPlayer();
        _animationPlayer.Play(_skeletonAnimation);

        _knightAnimationPlayer = new CelAnimationPlayer();
        _knightAnimationPlayer.Play(_knightAnimation);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        
        KeyboardState kbCurrentState = Keyboard.GetState();

        //Moves skeleton

         if(kbCurrentState.IsKeyDown(Keys.Down))
        {
            _spritePosition.Y += 1;
        }
        if(kbCurrentState.IsKeyDown(Keys.Up))
        {
            _spritePosition.Y -= 1;
        }
        if(kbCurrentState.IsKeyDown(Keys.Left))
        {
            _spritePosition.X -= 1;
        }
        if(kbCurrentState.IsKeyDown(Keys.Right))
        {
            _spritePosition.X += 1;
        }



        _animationPlayer.Update(gameTime);
        _knightAnimationPlayer.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_foreground, Vector2.Zero, Color.White);
        _animationPlayer.Draw(_spriteBatch, _spritePosition, SpriteEffects.None);
        _spriteBatch.End();

        _knightAnimationSpriteBatch.Begin();
        _knightAnimationPlayer.Draw(_knightAnimationSpriteBatch, new Vector2(0, 50), SpriteEffects.None);
        _knightAnimationSpriteBatch.End();

        base.Draw(gameTime);
    }
}
