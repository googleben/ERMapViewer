using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMapViewer
{
    internal class MonoViewer : Game
    {
        KeyboardState prevKb = new KeyboardState();
        MouseState prevMouse = new MouseState();
        Vector3 cameraPos = new Vector3(1, 0, 0);
        Matrix view;
        Matrix projection;
        Matrix cullTransform;
        BasicEffect effect;
        BoundingFrustum cullFrustum;
        List<VertexPositionNormalTexture>? drawFrustumLines = null;
        float prevAspectRatio;
        float cameraThetaX = 0;
        float cameraThetaY = 0;
        public Map map;
        public Map? nextMap = null;
        public MapModelPlacement? selected = null;
        GraphicsDeviceManager gdm;

        public MonoViewer(Map map)
        {
            this.map = map;
            Window.Title = "Map Viewer";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            gdm = new GraphicsDeviceManager(this);
            cullFrustum = new BoundingFrustum(Matrix.Identity);
        }

        protected override void Initialize()
        {
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), GraphicsDevice.Viewport.AspectRatio, 1, 100000);
            view = Matrix.CreateLookAt(cameraPos, cameraPos + new Vector3((float)Math.Cos(cameraThetaX), (float)Math.Sin(cameraThetaY), (float)Math.Sin(cameraThetaX)), new Vector3(0, 1, 0));
            cullTransform = projection * view;
            cullFrustum = new BoundingFrustum(cullTransform);
            prevAspectRatio = GraphicsDevice.Viewport.AspectRatio;
            effect = new BasicEffect(GraphicsDevice);
            effect.Projection = projection;
            effect.View = view;
            effect.TextureEnabled = false;
            effect.VertexColorEnabled = false;
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (nextMap != null) map = nextMap;
            bool updateCamera = false;
            var aspectRatio = GraphicsDevice.Viewport.AspectRatio;
            if (aspectRatio != prevAspectRatio) {
                updateCamera = true;
                projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, 1, 100000);
                prevAspectRatio = aspectRatio;
            }
            var kb = Keyboard.GetState();
            var mouse = Mouse.GetState();
            if (this.IsActive) {
                var moveMul = kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift) ? 10 : kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl) ? 0.01f : 1;
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W)) {
                    updateCamera = true;
                    cameraPos += new Vector3((float)(Math.Cos(cameraThetaX)), (float)Math.Sin(cameraThetaY), (float)(Math.Sin(cameraThetaX))) * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S)) {
                    updateCamera = true;
                    cameraPos -= new Vector3((float)(Math.Cos(cameraThetaX)), (float)Math.Sin(cameraThetaY), (float)(Math.Sin(cameraThetaX))) * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A)) {
                    updateCamera = true;
                    cameraPos -= new Vector3((float)(-Math.Sin(cameraThetaX)), 0, (float)(Math.Cos(cameraThetaX))) * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D)) {
                    updateCamera = true;
                    cameraPos += new Vector3((float)(-Math.Sin(cameraThetaX)), 0, (float)(Math.Cos(cameraThetaX))) * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q)) {
                    updateCamera = true;
                    cameraPos.Y -= 1 * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.E)) {
                    updateCamera = true;
                    cameraPos.Y += 1 * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left)) {
                    updateCamera = true;
                    cameraThetaX -= 0.001f * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right)) {
                    updateCamera = true;
                    cameraThetaX += 0.001f * moveMul;
                }
                if (mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed) {
                    updateCamera = true;
                    var dx = mouse.X - prevMouse.X;
                    var dy = mouse.Y - prevMouse.Y;
                    cameraThetaX += dx / 200f;
                    cameraThetaY -= dy / 200f;

                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.R)) {
                    updateCamera = true;
                    cameraPos = new Vector3(1, 0, 0);
                    cameraThetaX = 0;
                    cameraThetaY = 0;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F) && !prevKb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F)) {
                    var cs = (cullFrustum).GetCorners();
                    foreach (var c in cs) Debug.WriteLine(c);
                    var lines = new List<VertexPositionNormalTexture>();
                    foreach (var (a, b) in new (int, int)[] {
                    (0, 1), (1, 2), (2, 3), (3, 0), (4, 5), (5, 6), (6, 7), (7, 4),
                    (0, 4), (1, 5), (2, 6), (3, 7)
                    }) {
                            lines.Add(new VertexPositionNormalTexture(cs[a], new Vector3(), new Vector2()));
                            lines.Add(new VertexPositionNormalTexture(cs[b], new Vector3(), new Vector2()));
                    }
                    drawFrustumLines = lines;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.C) && !prevKb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.C)) {
                    var p = cameraPos;
                    var d = 0.2f;
                    var cs = new Vector3[] {
                        new Vector3(p.X - d, p.Y - d, p.Z - d), new Vector3(p.X - d, p.Y - d, p.Z + d),
                        new Vector3(p.X + d, p.Y - d, p.Z + d), new Vector3(p.X + d, p.Y - d, p.Z - d),
                        new Vector3(p.X - d, p.Y + d, p.Z - d), new Vector3(p.X - d, p.Y + d, p.Z + d),
                        new Vector3(p.X + d, p.Y + d, p.Z + d), new Vector3(p.X + d, p.Y + d, p.Z - d),
                    }; var lines = new List<VertexPositionNormalTexture>();
                    foreach (var (a, b) in new (int, int)[] {
                    (0, 1), (1, 2), (2, 3), (3, 0), (4, 5), (5, 6), (6, 7), (7, 4),
                    (0, 4), (1, 5), (2, 6), (3, 7)
                    }) {
                        lines.Add(new VertexPositionNormalTexture(cs[a], new Vector3(), new Vector2()));
                        lines.Add(new VertexPositionNormalTexture(cs[b], new Vector3(), new Vector2()));
                    }
                    drawFrustumLines = lines;
                }
                if (mouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && mouse.RightButton != prevMouse.RightButton) {
                    var near = GraphicsDevice.Viewport.Unproject(new Vector3(mouse.X, mouse.Y, 0), projection, view, Matrix.Identity);
                    var far = GraphicsDevice.Viewport.Unproject(new Vector3(mouse.X, mouse.Y, 1), projection, view, Matrix.Identity);
                    var dir = far - near;
                    dir.Normalize();
                    var ray = new Ray(near, dir);
                    float closestDist = float.PositiveInfinity;
                    MapModelPlacement? closest = null;
                    foreach (var placement in map.placements) {
                        var f = ray.Intersects(placement.bb);
                        if (f == null || object.ReferenceEquals(selected, placement)) continue;
                        if (f < closestDist) {
                            closest = placement;
                            closestDist = (float)f;
                        }
                    }
                    if (closest == null) {
                        Debug.WriteLine("No object found from raycast");
                    } else {
                        Debug.WriteLine($"{closest.name} {closestDist} {closest.bb} {closest.model.name}");
                    }
                    selected = closest;
                }
            }
            if (cameraThetaY > MathHelper.ToRadians(89)) cameraThetaY = MathHelper.ToRadians(89);
            if (cameraThetaY < -MathHelper.ToRadians(89)) cameraThetaY = -MathHelper.ToRadians(89);
            if (updateCamera) {
                view = Matrix.CreateLookAt(cameraPos, cameraPos + new Vector3((float)Math.Cos(cameraThetaX), (float)Math.Sin(cameraThetaY), (float)Math.Sin(cameraThetaX)), new Vector3(0, 1, 0));
                cullTransform = view*projection;
                cullFrustum = new BoundingFrustum(cullTransform);
                effect.Projection = projection;
                effect.View = view;
            }
            //Debug.WriteLine(cameraPos);
            prevKb = kb;
            prevMouse = mouse;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!IsActive) return;
            var rasterState = new RasterizerState();
            rasterState.FillMode = FillMode.Solid;
            rasterState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterState;
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);
            effect.Projection = projection;
            effect.View = view;
            effect.LightingEnabled = false;
            effect.World = Matrix.Identity;
            effect.DiffuseColor = new Vector3(0, 0, 1);
            foreach (var placement in map.placements) {
                var s = ReferenceEquals(placement, selected);
                if (s) effect.DiffuseColor = new Vector3(0, 1, 0);
                var bb = placement.bb;
                var cs = bb.GetCorners();
                var lines = new List<VertexPositionNormalTexture>();
                foreach (var (a, b) in new (int, int)[] {
                    (0, 1), (1, 2), (2, 3), (3, 0), (4, 5), (5, 6), (6, 7), (7, 4),
                    (0, 4), (1, 5), (2, 6), (3, 7)
                }) {
                    lines.Add(new VertexPositionNormalTexture(cs[a], new Vector3(), new Vector2()));
                    lines.Add(new VertexPositionNormalTexture(cs[b], new Vector3(), new Vector2()));
                }
                foreach (var pass in effect.CurrentTechnique.Passes) {
                    pass.Apply();
                    GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, lines.ToArray(), 0, 12);
                }
                if (s) effect.DiffuseColor = new Vector3(0, 0, 1);
            }
            effect.DiffuseColor = new Vector3(1, 0, 0);
            if (drawFrustumLines != null) {
                var contained = false;
                if (selected != null) foreach (var c in drawFrustumLines) {
                    if (selected.bb.Contains(c.Position) != ContainmentType.Disjoint) {
                            contained = true;
                            break;
                    }
                }
                if (contained) effect.DiffuseColor = new Vector3(0.2f, 1, 0.2f);
                foreach (var pass in effect.CurrentTechnique.Passes) {
                    pass.Apply();
                    GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, drawFrustumLines.ToArray(), 0, 12);
                }
            }
            effect.LightingEnabled = true;
            effect.AmbientLightColor = new Vector3(0.5f, 0.5f, 0.5f);
            effect.DiffuseColor = new Vector3(0.7f, 0.7f, 0.7f);
            effect.SpecularColor = new Vector3(1, 1, 1);
            effect.DirectionalLight0.Direction = new Vector3(0.2f, -1f, 0.2f);
            effect.DirectionalLight0.DiffuseColor = new Vector3(.7f, .7f, .7f);
            var cullCorners = cullFrustum.GetCorners();
            foreach (var placement in map.placements) {
                if (!cullFrustum.Intersects(placement.bb)) continue;
                var s = ReferenceEquals(placement, selected);
                if (s) effect.AmbientLightColor = new Vector3(0.1f, 0.5f, 0.1f);
                var model = placement.GetLod(cameraPos);
                if (model.numTriangles <= 0) continue;
                var world = placement.transform;
                effect.World = world;
                foreach (var pass in effect.CurrentTechnique.Passes) {
                    pass.Apply();
                    GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, model.triangleList, 0, model.numTriangles);
                }
                if (s) effect.AmbientLightColor = new Vector3(0.5f, 0.5f, 0.5f);
            }
            base.Draw(gameTime);
        }
    }
}
