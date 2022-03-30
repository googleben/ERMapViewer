using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMapViewer
{
    internal class WavefrontObj
    {
        public struct Face
        {
            public List<(int vertexInd, int textureInd, int normalInd)> vertexIndices;
            public Face(List<(int vertexInd, int textureInd, int normalInd)> vertexIndices)
            {
                this.vertexIndices = vertexIndices;
            }
        }
        public List<Vector3> vertices;
        public List<Vector2> textureVertices;
        public List<Vector3> vertexNormals;
        public List<Face> faces;

        public WavefrontObj()
        {
            vertices = new List<Vector3>();
            textureVertices = new List<Vector2>();
            vertexNormals = new List<Vector3>();
            faces = new List<Face>();
        }

        /// <summary>
        /// Assumes a triangulated mesh
        /// </summary>
        public SimpleMesh ToMesh()
        {
            int numTris = faces.Count;
            List<VertexPositionNormalTexture> vertexInfo = new(numTris * 3);
            foreach (var f in faces) {
                foreach (var (vertInd, texInd, normInd) in f.vertexIndices) {
                    var vert = vertices[vertInd - 1];
                    var tex = textureVertices[texInd - 1];
                    var normal = vertexNormals[normInd - 1];
                    vertexInfo.Add(new VertexPositionNormalTexture(vert, normal, tex));
                }
            }
            return new SimpleMesh(vertexInfo.ToArray(), numTris);
        }

        public static WavefrontObj Read(string data)
        {
            var ans = new WavefrontObj();
            foreach (var line in data.Split('\n').Select(s => s.Trim())) {
                var sp = line.Split(' ');
                string ident = sp[0];
                var d = sp[1..];
                switch (ident) {
                    case "v": 
                        ans.vertices.Add(new Vector3(
                            float.Parse(d[0]),
                            float.Parse(d[2]),
                            float.Parse(d[1])
                        )); 
                        break;
                    case "vt":
                        ans.textureVertices.Add(new Vector2(
                            float.Parse(d[0]),
                            float.Parse(d[1])
                        ));
                        break;
                    case "vn":
                        ans.vertexNormals.Add(new Vector3(
                            float.Parse(d[0]),
                            float.Parse(d[2]),
                            float.Parse(d[1])
                        ));
                        break;
                    case "f":
                        List<(int vertexInd, int textureInd, int normalInd)> vertexIndices = new();
                        foreach (var s in d) {
                            var sp2 = s.Split('/');
                            vertexIndices.Add((int.Parse(sp2[0]), int.Parse(sp2[1]), int.Parse(sp2[2])));
                        }
                        ans.faces.Add(new Face(vertexIndices));
                        break;
                };
            }
            return ans;
        }
    }
}
