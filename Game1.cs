using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using SharpDX.XInput;

namespace Pong_igen;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D pixel;
    int hastighet = 4;

    // skärmen är x: bredd = 800, y: höjd = 480

    Rectangle väpadel = new Rectangle(10,200,20,100);
                                    // x: y: width hight
    
    Rectangle höpadel = new Rectangle(770,200,20,100);

    Rectangle boll = new Rectangle(390,230,20,20);

    int bollxvo = 3;
    int bollyvo = 3;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        pixel = new Texture2D(GraphicsDevice, 1,1);
        pixel.SetData(new Color[]{Color.White});
        // pixel = Content.Load<Texture2D>("pixel");
        

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)){
            Exit();
        }

        KeyboardState kstate  = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.W) && väpadel.Y > 10){
            väpadel.Y-=hastighet;
        }
        else if(kstate.IsKeyDown(Keys.S) && väpadel.Y < 370){
            väpadel.Y+=hastighet;
        }

        if (kstate.IsKeyDown(Keys.Up) && höpadel.Y > 10){
            höpadel.Y-=hastighet;
        }
        else if(kstate.IsKeyDown(Keys.Down) && höpadel.Y < 370){
            höpadel.Y+=hastighet;
        }

        boll.X += bollxvo;
        boll.Y -= bollyvo;

        if(höpadel.Intersects(boll) || väpadel.Intersects(boll)){
            bollxvo *= -2;
        }

        if(boll.Y <= 0 || boll.Y >= 460){
            bollyvo *= -2;
        }

        if(boll.X <= 0 || boll.X >= 780){
            boll.X = 390;
            boll.Y = 230;
            bollxvo = 2;
            bollyvo = 2;
        }
    
        
        
        

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);


        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        _spriteBatch.Draw(pixel, väpadel, Color.White);
        _spriteBatch.Draw(pixel, boll, Color.White);
        _spriteBatch.Draw(pixel, höpadel, Color.White);
        
        _spriteBatch.End();


        base.Draw(gameTime);
    }
}
