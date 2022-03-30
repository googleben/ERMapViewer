using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMapViewer
{
    internal class MapModelPlacement
    {
        public MapModel model;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public Matrix transform;
        public BoundingBox bb;
        public string name;
        public Vector3 bbCenter;
        public MapModelPlacement(MapModel model, Vector3 position, Vector3 rotation, Vector3 scale, string name)
        {
            this.model = model;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.name = name;
            //transform = Matrix.CreateTranslation(position);
            transform = Matrix.CreateScale(scale) * Matrix.CreateRotationX(-MathHelper.ToRadians(rotation.X)) * Matrix.CreateRotationY(-MathHelper.ToRadians(rotation.Y)) * Matrix.CreateRotationZ(-MathHelper.ToRadians(rotation.Z)) * Matrix.CreateTranslation(position);
            /*var bbMin = Vector4.Transform(model.bbMin, transform);
            var bbMax = Vector4.Transform(model.bbMax, transform);
            bbMin /= bbMin.W;
            bbMax /= bbMax.W;
            Vec
            bb = new BoundingBox(new Vector3(bbMin.X, bbMin.Y, bbMin.Z), new Vector3(bbMax.X, bbMax.Y, bbMax.Z));*/
            var cs = model.bb.GetCorners();
            var bbMin = Vector3.Transform(cs[0], transform);
            var bbMax = bbMin;
            foreach (var c in cs) {
                var t = Vector3.Transform(c, transform);
                bbMin = new Vector3(Math.Min(bbMin.X, t.X), Math.Min(bbMin.Y, t.Y), Math.Min(bbMin.Z, t.Z));
                bbMax = new Vector3(Math.Max(bbMax.X, t.X), Math.Max(bbMax.Y, t.Y), Math.Max(bbMax.Z, t.Z));
            }
            bbCenter = (bbMin + bbMax) / 2;
            bb = new BoundingBox(bbMin, bbMax);
        }

        public MapMesh? GetLod(Vector3 cameraPos, bool highPerfMode)
        {
            float cameraDist = (cameraPos - bbCenter).Length();
            if (highPerfMode) {
                if (cameraDist > model.bbDiag * 5) return null;
                else return model.GetLod(float.MaxValue);
            }
            return model.GetLod(cameraDist);
        }
    }
}
