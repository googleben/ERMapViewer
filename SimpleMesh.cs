using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMapViewer
{
    internal class SimpleMesh
    {
        public VertexPositionNormalTexture[] vertices;
        public VertexPositionNormalTexture[] reverseChiralityVertices;
        public int numTriangles;
        public SimpleMesh(VertexPositionNormalTexture[] vertices, int numTriangles)
        {
            this.vertices = vertices;
            reverseChiralityVertices = new VertexPositionNormalTexture[vertices.Length];
            for (int i = 0; i < vertices.Length - 2; i+=3) {
                reverseChiralityVertices[i] = vertices[i];
                reverseChiralityVertices[i+1] = vertices[i+2];
                reverseChiralityVertices[i + 2] = vertices[i + 1];
            }
            this.numTriangles = numTriangles;
        }
    }

    internal class SimpleMeshPlacement
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public Matrix transform;
        public BoundingBox bb;
        public SimpleMesh mesh;
        public SimpleMeshPlacement(SimpleMesh mesh, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.mesh = mesh;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            transform = Matrix.CreateScale(scale) * Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X)) * Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z)) * Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) * Matrix.CreateTranslation(position);
            Vector3 bbMin = Vector3.Transform(mesh.vertices[0].Position, transform);
            Vector3 bbMax = bbMin;
            foreach (var vi in mesh.vertices) {
                var v = vi.Position;
                v = Vector3.Transform(v, transform);
                bbMin = new Vector3(Math.Min(v.X, bbMin.X), Math.Min(v.Y, bbMin.Y), Math.Min(v.Z, bbMin.Z));
                bbMax = new Vector3(Math.Max(v.X, bbMax.X), Math.Max(v.Y, bbMax.Y), Math.Max(v.Z, bbMax.Z));
            }
            bb = new BoundingBox(bbMin, bbMax);
        }
    }
}
