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

    Rectangle väpadel = new Rectangle(10,200,20,470);
                                    // x: y: width hight
    
    Rectangle höpadel = new Rectangle(770,200,20,470);

    List<Rectangle> boll = new List<Rectangle>();
    List<Point> bollhastighet = new List<Point>();
    int bollxvo = 5;
    int bollyvo = 5;

    int countdown = 1;
    
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



        if(countdown != 10 && stop == 1){
            countdown++;
        }
        else if(countdown == 10 && stop == 1){

            for(int i = 0; i < 4; i++){
                boll.Add(new Rectangle(390,230,20,20));
                bollhastighet.Add(new Point(bollxvo *= -1,bollyvo)); 
                bollhastighet.Add(new Point(bollxvo,bollyvo*=-1));
            }
            
            countdown = 1;
        }
        
        

        for(int i = 0; i < boll.Count; i++){
            boll[i] = new Rectangle(boll[i].X + bollhastighet[i].X, boll[i].Y + bollhastighet[i].Y, boll[i].Width,boll[i].Height);
        
            if(höpadel.Intersects(boll[i]) || väpadel.Intersects(boll[i])){
                bollhastighet[i] = new Point(-bollhastighet[i].X ,bollhastighet[i].Y);
                int nyXHastighet = Math.Sign(bollhastighet[i].X) * (Math.Abs(bollhastighet[i].X) + 1); // Behåll riktningen
                int nyYHastighet = Math.Sign(bollhastighet[i].Y) * (Math.Abs(bollhastighet[i].Y) + 1); // Behåll riktningen

                // Tilldela den nya hastigheten
                bollhastighet[i] = new Point(nyXHastighet, nyYHastighet);
                
            }

            if(boll[i].Y <= 0 || boll[i].Y >= 460){
                bollhastighet[i] = new Point(bollhastighet[i].X, -bollhastighet[i].Y);

            }

            if(boll[i].X <= 0 || boll[i].X >= 780){
                boll[i] = new Rectangle(390,230,20,20);
                bollhastighet[i] = new Point(bollxvo,bollyvo);
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
