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
    internal class MapMesh
    {
        public VertexPositionNormalTexture[] triangleList;
        public int numTriangles;
        private VertexBuffer? vertexBuffer;
        public VertexBuffer GetVertexBuffer(GraphicsDevice g)
        {
            if (vertexBuffer != null) return vertexBuffer;
            vertexBuffer = new VertexBuffer(g, VertexPositionNormalTexture.VertexDeclaration, triangleList.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(triangleList);
            return vertexBuffer;
        }
        public MapMesh(VertexPositionNormalTexture[] triangleList, int numTriangles)
        {
            this.triangleList = triangleList;
            this.numTriangles = numTriangles;
        }
    }
    internal class MapModel
    {
        public Vector3 bbMin;
        public Vector3 bbMax;
        public BoundingBox bb;
        public string name;
        public MapMesh lodMax;
        public MapMesh lodMed;
        public MapMesh lodMin;
        public float lodMaxDist;
        public float lodMedDist;
        public float bbDiag;
        public int totalTriangles;
        public MapModel(FLVER2 flver, string name)
        {
            this.name = name;
            var tmp = flver.Header.BoundingBoxMin;
            bbMin = new Vector3(tmp.X, tmp.Z, tmp.Y);
            tmp = flver.Header.BoundingBoxMax;
            bbMax = new Vector3(tmp.X, tmp.Z, tmp.Y);
            bb = new BoundingBox(bbMin, bbMax);
            List<VertexPositionNormalTexture> lodMaxAns = new();
            int lodMaxTris = 0;
            List<VertexPositionNormalTexture> lodMedAns = new();
            int lodMedTris = 0;
            List<VertexPositionNormalTexture> lodMinAns = new();
            int lodMinTris = 0;
            //HashSet<(VertexPosition a, VertexPosition b)> lines = new();
            foreach (var mesh in flver.Meshes) {
                var verts = mesh.Vertices;
                foreach (var faceSet in mesh.FaceSets) {
                    List<VertexPositionNormalTexture> ans = lodMaxAns;
                    ref int tris = ref lodMaxTris;
                    if (faceSet.Flags.HasFlag(FLVER2.FaceSet.FSFlags.LodLevel2)) {
                        ans = lodMinAns;
                        tris = ref lodMinTris;
                    } else if (faceSet.Flags.HasFlag(FLVER2.FaceSet.FSFlags.LodLevel1)) {
                        ans = lodMedAns;
                        tris = ref lodMedTris;
                    }
                    var triList = faceSet.Triangulate(true);
                    for (int i = 0; i < triList.Count - 2; i+=3) {
                        totalTriangles++;
                        tris++;
                        ans.Add(VertToXna(verts[triList[i]]));
                        ans.Add(VertToXna(verts[triList[i+1]]));
                        ans.Add(VertToXna(verts[triList[i+2]]));
                    }
                }
            }
            lodMin = new MapMesh(lodMinAns.ToArray(), lodMinTris);
            lodMed = new MapMesh(lodMedAns.ToArray(), lodMedTris);
            lodMax = new MapMesh(lodMaxAns.ToArray(), lodMaxTris);
            bbDiag = (bbMax - bbMin).Length();
            if (lodMinTris == 0) lodMedDist = float.PositiveInfinity;
            else lodMedDist = bbDiag * 5;
            if (lodMedTris == 0) lodMaxDist = lodMedDist;
            else lodMaxDist = bbDiag * 3;
            if (lodMaxTris == 0) lodMaxDist = 0;
            if (lodMedTris == 0 && lodMaxTris == 0) lodMedDist = 0;
        }

        public MapMesh GetLod(float cameraDistance)
        {
            if (cameraDistance < lodMedDist) {
                if (cameraDistance < lodMaxDist) {
                    return lodMax;
                } else return lodMed;
            } else return lodMin;
        }

        public static VertexPositionNormalTexture VertToXna(FLVER.Vertex v)
        {
            return new VertexPositionNormalTexture(
                new Microsoft.Xna.Framework.Vector3(v.Position.X, v.Position.Z, v.Position.Y),
                new Microsoft.Xna.Framework.Vector3(v.Normal.X, v.Normal.Z, v.Normal.Y),
                Microsoft.Xna.Framework.Vector2.Zero
            );
        }
    }
}
