using Microsoft.Xna.Framework;
using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ERMapViewer
{
    internal class Map : IDisposable
    {
        public Dictionary<string, MapModel> geometry;
        public MSBE msb;
        public List<MapModelPlacement> placements;
        public List<MapRegion> regions;

        public Map(string exFolder, MSBE msb)
        {
            this.msb = msb;
            geometry = new Dictionary<string, MapModel>();
            string mapGeomFolder = Path.Combine(exFolder, "data2", "map");
            HashSet<string> pieceNames = new();
            Program.progress.TaskName = "Reading placements";
            Program.progress.CurrentIndex = 0;
            List<MSBE.Part> parts = new(msb.Parts.MapPieces);
            parts.AddRange(msb.Parts.Objects);
            parts.AddRange(msb.Parts.Collisions);
            parts.AddRange(msb.Parts.ConnectCollisions);
            parts.AddRange(msb.Parts.Unk1s);
            Program.progress.MaxIndex = parts.Count;
            foreach (var part in parts) {
                pieceNames.Add(part.ModelName);
                Program.progress.CurrentIndex++;
            }
            Program.progress.TaskName = "Loading map geometry";
            Program.progress.CurrentIndex = 0;
            Program.progress.MaxIndex = msb.Models.MapPieces.Count;
            Program.progress.IsTaskIndexed = true;
            
            foreach (var mp in msb.Models.MapPieces) {
                if (!pieceNames.Contains(mp.Name)) continue;
                //firstPart is format "mxx_xx_xx_xx"
                var firstPart = mp.SibPath[@"N:\GR\data\Model\map\".Length..][..12];
                var bucket = firstPart[..3];
                //secondPart is format "xxxxxx"
                var secondPart = mp.Name[1..];
                //mxx_xx_xx_xx_xxxxxx
                var fullName = firstPart + "_" + secondPart;
                var fileName = $"/map/{bucket}/{firstPart}/{fullName}.mapbnd.dcx";
                var bnd = Program.ReadBnd(Program.data2Bhd, Program.data2Bdt, fileName);
                if (bnd == null) {
                    Program.progress.CurrentIndex++;
                    continue;
                }
                var flver = Program.ReadFlver(bnd, $"{secondPart}.flver");
                if (flver == null) {
                    Program.progress.CurrentIndex++;
                    continue;
                }
                geometry[mp.Name] = new MapModel(flver, fileName);
                Program.progress.CurrentIndex++;
            }
            string geomFolder = Path.Combine(exFolder, "data1", "asset", "aeg");
            Program.progress.TaskName = "Loading geometry";
            Program.progress.CurrentIndex = 0;
            Program.progress.MaxIndex = msb.Models.Geometry.Count;
            foreach (var g in msb.Models.Geometry) {
                if (!pieceNames.Contains(g.Name)) continue;
                //start is format "AEGxxx"
                var start = g.Name[..6];
                var fileName = $"/asset/aeg/{start}/{g.Name}.geombnd.dcx";
                var bnd = Program.ReadBnd(Program.data1Bhd, Program.data1Bdt, fileName.ToLower());
                if (bnd == null) {
                    Program.progress.CurrentIndex++;
                    continue;
                }
                //try {
                var flver = Program.ReadFlver(bnd, $"{g.Name}.flver");
                if (flver == null) {
                    Program.progress.CurrentIndex++;
                    continue;
                }
                geometry[g.Name] = new MapModel(flver, fileName);
                //} catch {
                //    Debug.WriteLine($"Couldn't read flver {fileName}");
                //}
                Program.progress.CurrentIndex++;
            }
            placements = new List<MapModelPlacement>();
            Program.progress.TaskName = "Loading placements";
            Program.progress.CurrentIndex = 0;
            Program.progress.MaxIndex = parts.Count;
            foreach (var p in parts) {
                MapModel geom;
                Program.progress.CurrentIndex++;
                if (p.ModelName == null) continue;
                if (!geometry.TryGetValue(p.ModelName, out geom!)) continue;
                var placement = new MapModelPlacement(geom, VecToXna(p.Position), VecToXna(p.Rotation), VecToXna(p.Scale), p.Name);
                placements.Add(placement);
            }
            Program.progress.TaskName = "Loading regions";
            Program.progress.CurrentIndex = 0;
            var regionList = msb.Regions.GetEntries();
            Program.progress.MaxIndex = regionList.Count;
            regions = new List<MapRegion>(regionList.Count);
            foreach (var r in regionList) {
                regions.Add(new MapRegion(r, regionList));
            }
            Program.progress.Finish();
        }

        public static Vector3 PosVecToXna(System.Numerics.Vector3 pos)
        {
            return new Vector3(pos.X, pos.Z, pos.Y);
        }

        public static Vector3 VecToXna(System.Numerics.Vector3 v)
        {
            return new Vector3(v.X, v.Z, v.Y);
        }

        public void Dispose()
        {
            this.placements.Clear();
            this.regions.Clear();
            foreach (var model in geometry.Values) {
                model.Dispose();
            }
        }
    }
}
