using Microsoft.Xna.Framework;
using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMapViewer
{
    internal class MapRegion
    {
        public string name;
        public string regType;
        public Vector3 position;
        public Vector3 rotation;
        public List<SimpleMeshPlacement> meshes;
        
        public MapRegion(MSBE.Region region, List<MSBE.Region> regionList)
        {
            name = region.Name;
            regType = region.GetType().Name.Split('+').Last();
            position = new Vector3(region.Position.X, region.Position.Z, region.Position.Y);
            rotation = new Vector3(-region.Rotation.X, -region.Rotation.Z, -region.Rotation.Y);
            if (region.Shape is MSB.Shape.Composite c) {
                meshes = new List<SimpleMeshPlacement>();
                foreach (var child in c.Children) {
                    var cName = child.RegionName;
                    var cReg = regionList.Find(r => r.Name == cName);
                    if (cReg == null) {
                        Debug.WriteLine($"Missing region {cName}");
                        continue;
                    }
                    meshes.Add(GetPlacement(cReg, position, rotation));
                }
            } else {
                meshes = new List<SimpleMeshPlacement> { GetPlacement(region, position, rotation) };
            }
        }

        public float? Raycast(Ray ray)
        {
            float? min = null;
            foreach (var m in meshes) {
                var cast = ray.Intersects(m.bb);
                if (cast == null) continue;
                if (min == null || cast < min) min = cast;
            }
            return min;
        }

        public bool ShouldShow()
        {
            var i = SettingsWindow.Instance;
            return regType switch {
                "InvasionPoint" => i.ShowInvasionPoints,
                "EnvironmentMapPoint" => i.ShowEnvironmentMapPoints,
                "Sound" => i.ShowSounds,
                "SFX" => i.ShowSfx,
                "WindSFX" => i.ShowWindSfx,
                "SpawnPoint" => i.ShowSpawnPoints,
                "Message" => i.ShowMessages,
                "PatrolRoute" => i.ShowPatrolRoutes,
                "WarpPoint" => i.ShowWarpPoints,
                "ActivationArea" => i.ShowActivationAreas,
                "Event" => i.ShowEvents,
                "Logic" => i.ShowLogic,
                "EnvironmentMapEffect" => i.ShowEnvironmentMapEffects,
                "WindArea" => i.ShowWindAreas,
                "MufflingBox" => i.ShowMufflingBoxes,
                "MufflingPortal" => i.ShowMufflingPortals,
                "MufflingPlane" => i.ShowMufflingPlanes,
                "PartsGroupArea" => i.ShowPartsGroupAreas,
                "AutoDrawGroupPoints" => i.ShowAutoDrawGroupPoints,
                "Other" => i.ShowOthers,
                _ => i.ShowUnknown,
            };
        }

        private static SimpleMeshPlacement GetPlacement(MSBE.Region region, Vector3 position, Vector3 rotation)
        {
            SimpleMesh mesh;
            SimpleMeshPlacement? placement = null;
            switch (region.Shape) {
                case MSB.Shape.Point p:
                    mesh = Program.Sphere;
                    placement = new SimpleMeshPlacement(mesh, position, rotation, new Vector3(1));
                    break;
                case MSB.Shape.Circle c:
                    mesh = Program.Circle;
                    placement = new SimpleMeshPlacement(mesh, position, rotation, new Vector3(c.Radius));
                    break;
                case MSB.Shape.Sphere s:
                    mesh = Program.Sphere;
                    placement = new SimpleMeshPlacement(mesh, position, rotation, new Vector3(s.Radius));
                    break;
                case MSB.Shape.Cylinder c:
                    mesh = Program.Cylinder;
                    placement = new SimpleMeshPlacement(mesh, position, rotation, new Vector3(c.Radius, c.Radius, c.Height));
                    break;
                case MSB.Shape.Rect r:
                    mesh = Program.Rectangle;
                    placement = new SimpleMeshPlacement(mesh, position, rotation, new Vector3(r.Width, r.Depth, 0));
                    break;
                case MSB.Shape.Box b:
                    mesh = Program.Cube;
                    placement = new SimpleMeshPlacement(mesh, position, rotation, new Vector3(b.Width, b.Depth, b.Height));
                    break;
            }
            return placement!;
        }

    }
}
