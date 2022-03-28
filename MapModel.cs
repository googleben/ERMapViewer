using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMapViewer
{
    internal class MapModel
    {
        public VertexPositionNormalTexture[] triangleList;
        //public VertexPosition[] lineList;
        public int numTriangles;
        public Vector3 bbMin;
        public Vector3 bbMax;
        public MapModel(FLVER2 flver)
        {
            var tmp = flver.Header.BoundingBoxMin;
            bbMin = new Vector3(tmp.X, tmp.Y, tmp.Z);
            tmp = flver.Header.BoundingBoxMax;
            bbMax = new Vector3(tmp.X, tmp.Y, tmp.Z);
            List<VertexPositionNormalTexture> ans = new();
            //HashSet<(VertexPosition a, VertexPosition b)> lines = new();
            foreach (var mesh in flver.Meshes) {
                foreach (var tri in mesh.GetFaces()) {
                    
                    numTriangles++;
                    var asXna = tri.Select(VertToXna).ToArray();
                    ans.AddRange(asXna);
                    //lines.Add((asXna[0], asXna[1]));
                    //lines.Add((asXna[1], asXna[2]));
                    //lines.Add((asXna[2], asXna[0]));
                }
            }
            triangleList = ans.ToArray();
            //lineList = lines.SelectMany(l => new VertexPosition[] {l.a, l.b}).ToArray();
        }

        public static VertexPositionNormalTexture VertToXna(FLVER.Vertex v)
        {
            return new VertexPositionNormalTexture(
                new Microsoft.Xna.Framework.Vector3(v.Position.X, v.Position.Y, v.Position.Z),
                new Microsoft.Xna.Framework.Vector3(v.Normal.X, v.Normal.Y, v.Normal.Z),
                Microsoft.Xna.Framework.Vector2.Zero
            );
        }
    }
}
