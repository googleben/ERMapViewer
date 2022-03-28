using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMapViewer
{
    internal class MonoViewer : Game
    {
        KeyboardState prevKb = new KeyboardState();
        MouseState prevMouse = new MouseState();
        Vector3 cameraPos = new Vector3();
        float cameraThetaX = 0;
        float cameraThetaY = 0;
        public Map map;
        public Map? nextMap = null;
        GraphicsDeviceManager gdm;

        public MonoViewer(Map map)
        {
            this.map = map;
            Window.Title = "Map Viewer";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            gdm = new GraphicsDeviceManager(this);
        }

        protected override void Update(GameTime gameTime)
        {
            if (nextMap != null) map = nextMap;
            var kb = Keyboard.GetState();
            var mouse = Mouse.GetState();
            var moveMul = kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift) ? 10 : 1;
            if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W)) cameraPos += new Vector3((float)(Math.Cos(cameraThetaX)), (float)Math.Sin(cameraThetaY), (float)(Math.Sin(cameraThetaX))) * moveMul;
            if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S)) cameraPos -= new Vector3((float)(Math.Cos(cameraThetaX)), (float)Math.Sin(cameraThetaY), (float)(Math.Sin(cameraThetaX))) * moveMul;
            if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A)) cameraPos -= new Vector3((float)(-Math.Sin(cameraThetaX)), 0, (float)(Math.Cos(cameraThetaX))) * moveMul;
            if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D)) cameraPos += new Vector3((float)(-Math.Sin(cameraThetaX)), 0, (float)(Math.Cos(cameraThetaX))) * moveMul;
            if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q)) cameraPos.Y -= 1 * moveMul;
            if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.E)) cameraPos.Y += 1 * moveMul;
            if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left)) cameraThetaX -= 0.001f * moveMul;
            if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right)) cameraThetaX += 0.001f * moveMul;
            if (mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed) {
                var dx = mouse.X - prevMouse.X;
                var dy = mouse.Y - prevMouse.Y;
                cameraThetaX += dx / 200f;
                cameraThetaY -= dy / 200f;

            }
            prevKb = kb;
            prevMouse = mouse;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            var rasterState = new RasterizerState();
            rasterState.FillMode = FillMode.Solid;
            rasterState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterState;
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);
            //var w = GraphicsDevice.Viewport.Width;
            //var h = GraphicsDevice.Viewport.Height;
            BasicEffect effect = new(GraphicsDevice);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), GraphicsDevice.Viewport.AspectRatio, 1, 100000);
            effect.FogEnabled = false;
            effect.LightingEnabled = true;
            effect.AmbientLightColor = new Vector3(0.5f, 0.5f, 0.5f);
            
            effect.View = Matrix.CreateLookAt(cameraPos, cameraPos + new Vector3((float)Math.Cos(cameraThetaX), (float)Math.Sin(cameraThetaY), (float)Math.Sin(cameraThetaX)), new Vector3(0, 1, 0));
            //effect.View = Matrix.CreateLookAt(new Vector3(5, 3, 4)*5, new Vector3(0, 0, 0), new Vector3(0, 0, 1));
            effect.DiffuseColor = new Vector3(0.7f, 0.7f, 0.7f);
            effect.SpecularColor = new Vector3(1, 1, 1);
            effect.DirectionalLight0.Direction = new Vector3(0.2f, -1f, 0.2f);
            effect.DirectionalLight0.DiffuseColor = new Vector3(.7f, .7f, .7f);
            effect.TextureEnabled = false;
            effect.VertexColorEnabled = false;
            foreach (var placement in map.placements) {
                var model = placement.model;
                if (model.numTriangles <= 0) continue;
                var world = placement.transform;
                effect.World = world*Matrix.CreateReflection(new Plane(new Vector3(0, 0, 1), 1));
                foreach (var pass in effect.CurrentTechnique.Passes) {
                    pass.Apply();
                    GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, model.triangleList, 0, model.numTriangles);
                }
            }
            /*effect.World = Matrix.CreateWorld(Vector3.Zero, new Vector3(1, 0, 0), new Vector3(0, 0, 1));
            effect.DiffuseColor = new Vector3(0, 0, 1);
            foreach (var pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, new VertexPosition[] {
                    new VertexPosition(new Vector3(0, 0, 0)),
                    new VertexPosition(new Vector3(100, 0, 0)),
                    new VertexPosition(new Vector3(0, 0, 0)),
                    new VertexPosition(new Vector3(0, 100, 0)),
                    new VertexPosition(new Vector3(0, 0, 0)),
                    new VertexPosition(new Vector3(0, 0, 100)),
                }, 0, 3);
            }*/
            base.Draw(gameTime);
        }
    }
}
