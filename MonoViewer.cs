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
        public MapModelPlacement? selectedObj = null;
        public MapRegion? selectedRegion = null;

        SpriteFont font;

        List<(float, MapModelPlacement)>? previousObjRaycast = null;
        List<(float, MapRegion)>? previousRegionRaycast = null;
        int selectedObjInd;
        int selectedRegionInd;

        FrameCounter frameCounter = new FrameCounter();

        Texture2D box;

        /// <summary>
        /// Messages to be displayed on screen.
        /// (value of total seconds when this message should disappear, message)
        /// </summary>
        readonly Queue<(double, string)> messages = new();
        GameTime lastGameTime;

        public MonoViewer(Map map)
        {
            this.map = map;
            Window.Title = "Map Viewer";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            new GraphicsDeviceManager(this);
            cullFrustum = new BoundingFrustum(Matrix.Identity);
        }

        public void ShowMessage(string message)
        {
            messages.Enqueue((lastGameTime.TotalGameTime.TotalSeconds + 20, message));
            if (messages.Count > 5) messages.Dequeue();
        }

        protected override void Initialize()
        {
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(70), GraphicsDevice.Viewport.AspectRatio, 0.01f, 100000);
            view = Matrix.CreateLookAt(cameraPos, 
                cameraPos + new Vector3((float)Math.Cos(cameraThetaX), (float)Math.Sin(cameraThetaX), (float)Math.Sin(cameraThetaY)), 
                new Vector3(0, 0, 1));
            cullTransform = projection * view;
            cullFrustum = new BoundingFrustum(cullTransform);
            prevAspectRatio = GraphicsDevice.Viewport.AspectRatio;
            effect = new BasicEffect(GraphicsDevice);
            effect.Projection = projection;
            effect.View = view;
            effect.TextureEnabled = false;
            effect.VertexColorEnabled = false;
            font = Content.Load<SpriteFont>("font");
            box = Content.Load<Texture2D>("black");
            //box = new Texture2D(GraphicsDevice, 1, 1);
            //box.SetData(new Microsoft.Xna.Framework.Color[] { Microsoft.Xna.Framework.Color.Black });
            base.Initialize();
        }

        private void ShowSelectedRegionInfo()
        {
            SettingsWindow.Instance.Invoke(() => {
                if (selectedRegion == null) SettingsWindow.Instance.SelectionInformation = "No region selected";
                else SettingsWindow.Instance.SelectionInformation = 
$@"Selected region name: {selectedRegion.name} ({selectedRegionInd}/{previousRegionRaycast?.Count})
Selected region type: {selectedRegion.regType}
";
            });
        }

        private void ShowSelectedObjectInfo()
        {
            SettingsWindow.Instance.Invoke(() => {
                if (selectedObj == null) SettingsWindow.Instance.SelectionInformation = "No object selected";
                else SettingsWindow.Instance.SelectionInformation =
$@"Selected object name: {selectedObj.name} ({selectedObjInd}/{previousObjRaycast?.Count})
Selected object file path: {selectedObj.model.name}
Selecetd object triangles: {selectedObj.model.lodMax.numTriangles + selectedObj.model.lodMin.numTriangles + selectedObj.model.lodMed.numTriangles}
";
            });
        }

        protected override void Update(GameTime gameTime)
        {
            var settings = SettingsWindow.Instance;
            lastGameTime = gameTime;
            if (nextMap != null) map = nextMap;
            bool updateCamera = false;
            var aspectRatio = GraphicsDevice.Viewport.AspectRatio;
            if (aspectRatio != prevAspectRatio) {
                updateCamera = true;
                projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(70), aspectRatio, 0.01f, 100000);
                prevAspectRatio = aspectRatio;
            }
            //remove old messages
            var secondsNow = gameTime.TotalGameTime.TotalSeconds;
            while (messages.Count > 0) {
                var (t, _) = messages.Peek();
                if (secondsNow > t) messages.Dequeue();
                else break;
            }
            //handle kb&m input
            var kb = Keyboard.GetState();
            var mouse = Mouse.GetState();
            if (this.IsActive) {
                bool shiftDown = kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift);
                var moveMul =
                    shiftDown ? 10 :
                    kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftAlt) ? 0.1f :
                    kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl) ? 0.01f 
                    : 1;
                moveMul *= settings.CameraSpeed;
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W)) {
                    updateCamera = true;
                    cameraPos += new Vector3((float)(Math.Cos(cameraThetaX)), (float)(Math.Sin(cameraThetaX)), (float)Math.Sin(cameraThetaY)) * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S)) {
                    updateCamera = true;
                    cameraPos -= new Vector3((float)(Math.Cos(cameraThetaX)), (float)(Math.Sin(cameraThetaX)), (float)Math.Sin(cameraThetaY)) * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A)) {
                    updateCamera = true;
                    cameraPos += new Vector3((float)(-Math.Sin(cameraThetaX)), (float)(Math.Cos(cameraThetaX)), 0) * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D)) {
                    updateCamera = true;
                    cameraPos -= new Vector3((float)(-Math.Sin(cameraThetaX)), (float)(Math.Cos(cameraThetaX)), 0) * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q)) {
                    updateCamera = true;
                    cameraPos.Z -= 1 * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.E)) {
                    updateCamera = true;
                    cameraPos.Z += 1 * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left)) {
                    updateCamera = true;
                    cameraThetaX -= 0.001f * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right)) {
                    updateCamera = true;
                    cameraThetaX += 0.001f * moveMul;
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up) && !prevKb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up)) {
                    settings.Invoke(() => settings.CameraSpeed *= 2);
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down) && !prevKb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down)) {
                    settings.Invoke(() => settings.CameraSpeed /= 2);
                }
                if (mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed) {
                    updateCamera = true;
                    var dx = mouse.X - prevMouse.X;
                    var dy = mouse.Y - prevMouse.Y;
                    cameraThetaX -= dx / 200f;
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
                    if (settings.SelectingRegions) {
                        MapRegion? closest = null;
                        previousRegionRaycast = new List<(float, MapRegion)>();
                        foreach (var r in map.regions) {
                            var f = r.Raycast(ray);
                            if (f == null) continue;
                            previousRegionRaycast.Add(((float)f, r));
                            if (f < closestDist) {
                                closest = r;
                                closestDist = (float)f;
                            }
                        }
                        previousRegionRaycast.Sort((a, b) => a.Item1.CompareTo(b.Item1));
                        selectedRegionInd = 0;
                        if (closest == null) {
                            Debug.WriteLine("No object found from raycast");
                            ShowMessage("No object found from raycast");
                        } else {
                            Debug.WriteLine($"{closest.name} {closestDist} {closest.regType}");
                            //ShowMessage($"Selected {closest.name} ({closest.regType})");
                        }
                        selectedRegion = closest;
                        ShowSelectedRegionInfo();
                    } else {
                        bool allowEmpty = settings.SelectEmptyObjects;
                        MapModelPlacement? closest = null;
                        previousObjRaycast = new List<(float, MapModelPlacement)>();
                        foreach (var placement in map.placements) {
                            if (!allowEmpty && placement.model.totalTriangles == 0) continue;
                            var f = ray.Intersects(placement.bb);
                            if (f == null) continue;
                            previousObjRaycast.Add(((float)f, placement));
                            if (f < closestDist) {
                                closest = placement;
                                closestDist = (float)f;
                            }
                        }
                        previousObjRaycast.Sort((a, b) => a.Item1.CompareTo(b.Item1));
                        selectedObjInd = 0;
                        if (closest == null) {
                            Debug.WriteLine("No object found from raycast");
                            ShowMessage("No object found from raycast");
                        } else {
                            Debug.WriteLine($"{closest.name} {closestDist} {closest.bb} {closest.model.name}");
                            ShowMessage($"Selected {closest.name} ({closest.model.name})");
                        }
                        selectedObj = closest;
                        ShowSelectedObjectInfo();
                    }
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.N) && !prevKb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.N)) {
                    if (settings.SelectingRegions) {
                        if (previousRegionRaycast != null && previousRegionRaycast.Count > 1) {
                            selectedRegionInd = (selectedRegionInd + (shiftDown ? -1 : 1)) % previousRegionRaycast.Count;
                            if (selectedRegionInd < 0) selectedRegionInd += previousRegionRaycast.Count;
                            selectedRegion = previousRegionRaycast[selectedRegionInd].Item2;
                            Debug.WriteLine($"Selected {selectedRegion.name} ({selectedRegionInd+1}/{previousRegionRaycast.Count}) ({selectedRegion.regType})");
                            //ShowMessage($"Selected {selectedRegion.name} ({selectedRegion.regType})");
                            ShowSelectedRegionInfo();
                        }
                    } else {
                        if (previousObjRaycast != null && previousObjRaycast.Count > 1) {
                            selectedObjInd = (selectedObjInd + (shiftDown ? -1 : 1)) % previousObjRaycast.Count;
                            if (selectedRegionInd < 0) selectedObjInd += previousObjRaycast.Count;
                            selectedObj = previousObjRaycast[selectedObjInd].Item2;
                            ShowMessage($"Selected {selectedObj.name} ({selectedObjInd + 1}/{previousObjRaycast.Count}) ({selectedObj.model.name})");
                            ShowSelectedObjectInfo();
                        }
                    }
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.P) && !prevKb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.P)) {
                    bool highPerfMode = settings.HighPerfMode;
                    settings.Invoke(() => settings.HighPerfMode = !highPerfMode);
                    ShowMessage("High performance mode " + (highPerfMode ? "enabled" : "disabled"));
                }
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.T) && !prevKb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.T)) {
                    bool selectingRegions = settings.SelectingRegions;
                    settings.Invoke(() => settings.SelectingRegions = !selectingRegions);
                    ShowMessage("Now selecting " + (selectingRegions ? "regions" : "objects"));
                }

            }
            if (cameraThetaY > MathHelper.ToRadians(89)) cameraThetaY = MathHelper.ToRadians(89);
            if (cameraThetaY < -MathHelper.ToRadians(89)) cameraThetaY = -MathHelper.ToRadians(89);
            if (updateCamera) {
                var forward = new Vector3((float)Math.Cos(cameraThetaX), (float)Math.Sin(cameraThetaX), (float)Math.Tan(cameraThetaY));
                var right = new Vector3(-(float)Math.Sin(cameraThetaX), (float)Math.Cos(cameraThetaX), 0);
                var up = Vector3.Cross(right, forward);
                if (up.Z < 0) up = -up;
                view = Matrix.CreateLookAt(cameraPos,cameraPos + forward,up);
                cullTransform = view*projection;
                cullFrustum = new BoundingFrustum(cullTransform);
                effect.Projection = projection;
                effect.View = view;
            }
            prevKb = kb;
            prevMouse = mouse;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!IsActive) return;
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameCounter.Update(dt);
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);
            Draw3D(gameTime);
            Draw2D(gameTime);
            base.Draw(gameTime);
        }

        private void Draw2D(GameTime gameTime)
        {
            SpriteBatch sb = new(GraphicsDevice);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            sb.Begin();
            float dy = 10 + font.LineSpacing;
            Vector2 pos = new(10, 10);
            var secs = gameTime.TotalGameTime.TotalSeconds;
            if (SettingsWindow.Instance.ShowFps) {
                sb.DrawString(font, $"{(int)Math.Round(frameCounter.AverageFramesPerSecond)}", pos, new Microsoft.Xna.Framework.Color(1f, 1f, 0.2f));
                pos.Y += dy;
            }
            foreach (var (end, m) in messages.Reverse()) {
                var toEnd = end - secs;
                var alphaFactor = toEnd > 5 ? 1 : (float)(toEnd / 5);
                var size = font.MeasureString(m);
                sb.Draw(box, new Microsoft.Xna.Framework.Rectangle((int)pos.X - 5, (int)pos.Y - 5, (int)size.X + 5, (int)size.Y + 5), new Microsoft.Xna.Framework.Color(0, 0, 0, 0.5f*alphaFactor));
                sb.DrawString(font, m, pos, new Microsoft.Xna.Framework.Color(alphaFactor, alphaFactor, alphaFactor, alphaFactor));
                pos.Y += dy;
            }
            sb.End();
        }

        private void Draw3D(GameTime gameTime) {
            var settings = SettingsWindow.Instance;
            GraphicsDevice.DepthStencilState = new DepthStencilState();
            var rasterState = new RasterizerState {
                FillMode = SettingsWindow.Instance.WireframeObjects ? FillMode.WireFrame : FillMode.Solid,
                CullMode = CullMode.CullClockwiseFace,
            };
            GraphicsDevice.RasterizerState = rasterState;
            effect.Projection = projection;
            effect.View = view;
            effect.LightingEnabled = false;
            effect.World = Matrix.Identity;
            effect.DiffuseColor = new Vector3(0, 0, 1);
            effect.DiffuseColor = new Vector3(1, 0, 0);
            if (drawFrustumLines != null) {
                var contained = false;
                if (selectedObj != null) foreach (var c in drawFrustumLines) {
                    if (selectedObj.bb.Contains(c.Position) != ContainmentType.Disjoint) {
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
            effect.Alpha = 1f;
            effect.LightingEnabled = true;
            effect.AmbientLightColor = new Vector3(0.5f, 0.5f, 0.5f);
            effect.DiffuseColor = new Vector3(0.7f, 0.7f, 0.7f);
            effect.SpecularColor = new Vector3(1, 1, 1);
            effect.DirectionalLight0.Direction = new Vector3(0.2f, 0.2f, -1f);
            effect.DirectionalLight0.DiffuseColor = new Vector3(.7f, .7f, .7f);
            bool highPerfMode = settings.HighPerfMode;
            foreach (var placement in map.placements) {
                if (!cullFrustum.Intersects(placement.bb)) continue;
                var s = ReferenceEquals(placement, selectedObj);
                var model = placement.GetLod(cameraPos, highPerfMode);
                if (model == null) continue;
                if (model.numTriangles <= 0) continue;
                if (s) effect.AmbientLightColor = new Vector3(0.1f, 0.5f, 0.1f);
                var world = placement.transform;
                effect.World = world;
                foreach (var pass in effect.CurrentTechnique.Passes) {
                    pass.Apply();
                    GraphicsDevice.SetVertexBuffer(model.GetVertexBuffer(GraphicsDevice));
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, model.numTriangles);
                }
                if (s) effect.AmbientLightColor = new Vector3(0.5f, 0.5f, 0.5f);
            }
            if (SettingsWindow.Instance.ShowRegions) {
                var dss = new DepthStencilState {
                    DepthBufferWriteEnable = false
                };
                GraphicsDevice.DepthStencilState = dss;
                rasterState = new RasterizerState {
                    FillMode = FillMode.Solid,
                    CullMode = CullMode.None
                };
                GraphicsDevice.RasterizerState = rasterState;
                effect.DiffuseColor = new Vector3(1, 1, 0);
                effect.Alpha = 0.05f;
                foreach (var r in map.regions) {
                    if (!r.ShouldShow()) continue;
                    var s = ReferenceEquals(selectedRegion, r);
                    if (s) effect.DiffuseColor = new Vector3(0, 1, 1);
                    foreach (var m in r.meshes) {
                        bool inside = m.bb.Contains(cameraPos) != ContainmentType.Disjoint;
                        effect.World = m.transform;
                        foreach (var pass in effect.CurrentTechnique.Passes) {
                            pass.Apply();
                            GraphicsDevice.SetVertexBuffer(m.mesh.GetVertexBuffer(GraphicsDevice));
                            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, m.mesh.numTriangles);
                        }
                    }
                    if (s) effect.DiffuseColor = new Vector3(1, 1, 0);
                }
                rasterState = new RasterizerState {
                    FillMode = FillMode.WireFrame,
                    CullMode = CullMode.None
                };
                GraphicsDevice.RasterizerState = rasterState;
                effect.DiffuseColor = new Vector3(1, 1, 0);
                effect.Alpha = 1f;
                foreach (var r in map.regions) {
                    if (!r.ShouldShow()) continue;
                    var s = ReferenceEquals(selectedRegion, r);
                    if (s) effect.DiffuseColor = new Vector3(0, 1, 1);
                    foreach (var m in r.meshes) {
                        bool inside = m.bb.Contains(cameraPos) != ContainmentType.Disjoint;
                        effect.World = m.transform;
                        foreach (var pass in effect.CurrentTechnique.Passes) {
                            pass.Apply();
                            GraphicsDevice.SetVertexBuffer(m.mesh.GetVertexBuffer(GraphicsDevice));
                            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, m.mesh.numTriangles);
                        }
                    }
                    if (s) effect.DiffuseColor = new Vector3(1, 1, 0);
                }
            }
        }
    }

    public class FrameCounter
    {
        public FrameCounter()
        {
        }

        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }

        public const int MAXIMUM_SAMPLES = 100;

        private Queue<float> _sampleBuffer = new Queue<float>();

        public bool Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MAXIMUM_SAMPLES) {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            } else {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
            return true;
        }
    }
}
