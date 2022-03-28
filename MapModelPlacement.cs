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
        public MapModelPlacement(MapModel model, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.model = model;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            //transform = Matrix.CreateTranslation(position);
            transform = Matrix.CreateScale(scale) * Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X)) * Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z)) * Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) * Matrix.CreateTranslation(position);
        }
    }
}
