using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
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

    Rectangle väpadel = new Rectangle(10,200,20,400);
                                    // x: y: width hight
    
    Rectangle höpadel = new Rectangle(770,200,20,400);

    List<Rectangle> boll = new List<Rectangle>();
    List<Point> bollhastighet = new List<Point>();
    int bollxvo = 5;
    int bollyvo = 5;
    int xbollgräns = 780;
    int ybollgräns = 480;

    int countdown = 1;
    int countdown1 = 1;
    
    int stop = 2;

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
        pixel.SetData(new Color[]{Color.Black});
        // pixel = Content.Load<Texture2D>("pixel");
        

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)){
            Exit();
        }if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.O)){
            ResetElapsedTime();
        }

        KeyboardState kstate  = Keyboard.GetState();
        Random r = new Random();
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



        if(kstate.IsKeyDown(Keys.F)){
            stop = 2;
        }
        if(kstate.IsKeyDown(Keys.D)){
            stop = 1;
        }



        if(countdown != 145 && stop == 1){
            countdown++;
        }
        if(countdown == 145 && stop == 1 || kstate.IsKeyDown(Keys.R)){

            for(int i = 1; i < 5; i++){
                boll.Add(new Rectangle(390,224 + i*3,20,20));
                bollhastighet.Add(new Point(bollxvo *= -1,bollyvo += i)); 
                bollhastighet.Add(new Point(bollxvo += i,bollyvo*=-1));
            }
            bollxvo = 5;
            bollyvo = 5;
            countdown = 1;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.F))
        {
            _graphics.PreferredBackBufferWidth++;
            _graphics.PreferredBackBufferHeight++;
            höpadel.X++;
            ybollgräns++;
            xbollgräns++;
            _graphics.ApplyChanges();
        }

        
        
        
        
        

        for(int i = 0; i < boll.Count; i++){
            boll[i] = new Rectangle(boll[i].X + bollhastighet[i].X, boll[i].Y + bollhastighet[i].Y, boll[i].Width,boll[i].Height);
        
            if(höpadel.Intersects(boll[i]) || väpadel.Intersects(boll[i])){
                bollhastighet[i] = new Point(-bollhastighet[i].X ,bollhastighet[i].Y);
                
                //boll[i]= new Rectangle(30,boll[i].Y,20,20);
                
            }

            if(boll[i].Y <= 0 || boll[i].Y >= ybollgräns){
                bollhastighet[i] = new Point(bollhastighet[i].X, -bollhastighet[i].Y);

            }

            if(boll[i].X <= 0 || boll[i].X >= xbollgräns){
                boll[i] = new Rectangle(390,230,20,20);
                //bollhastighet[i] = new Point(bollxvo *= -1,bollyvo *= -1);
            }
        
        }

        // TODO: Add your update logic here

        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.HotPink);
        

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        _spriteBatch.Draw(pixel, väpadel, Color.Black);
        _spriteBatch.Draw(pixel, höpadel, Color.Black);
        
        foreach(Rectangle bollar in boll){
            _spriteBatch.Draw(pixel,bollar,Color.Black);
        }
        _spriteBatch.End();


        base.Draw(gameTime);
    }
}
