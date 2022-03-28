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
    internal class Map
    {
        public Dictionary<string, MapModel> geometry;
        public MSBE msb;
        public List<MapModelPlacement> placements;

        public Map(string exFolder, MSBE msb)
        {
            this.msb = msb;
            geometry = new Dictionary<string, MapModel>();
            string mapGeomFolder = Path.Combine(exFolder, "data2", "map");
            HashSet<string> pieceNames = new();
            Program.progress.TaskName = "Reading placements";
            Program.progress.CurrentIndex = 0;
            Program.progress.MaxIndex = msb.Parts.Unk1s.Count + msb.Parts.MapPieces.Count + msb.Parts.Objects.Count;
            foreach (var p in msb.Parts.MapPieces) {
                //MapModel geom;
                if (Regex.IsMatch(p.Name, @"^m\d\d\d\d\d\d")) {
                    pieceNames.Add(p.Name[..7]);
                    //if (!geometry.TryGetValue(p.Name[..7], out geom!)) continue;
                } else if (Regex.IsMatch(p.ModelName, @"^m\d\d\d\d\d\d\d\d")) {
                    pieceNames.Add(p.ModelName[..9]);
                } else if (Regex.IsMatch(p.Name, @"^AEG\d\d\d_\d\d\d")) {
                    pieceNames.Add(p.Name[..10]);
                    //if (!geometry.TryGetValue(p.Name[..10], out geom!)) continue;
                } else {
                    var m = Regex.Match(p.Name, @".*(AEG\d\d\d_\d\d\d).*");
                    if (m.Success) {
                        pieceNames.Add(m.Groups[1].Value);
                    } else continue;
                }
                //pieceNames.Add(p.Name);
                //placements.Add(new MapModelPlacement(geom, VecToXna(p.Position), VecToXna(p.Rotation), VecToXna(p.Scale)));
                Program.progress.CurrentIndex++;
            }
            foreach (var p in msb.Parts.Objects) {
                //MapModel geom;
                if (Regex.IsMatch(p.Name, @"^m\d\d\d\d\d\d")) {
                    pieceNames.Add(p.Name[..7]);
                    //if (!geometry.TryGetValue(p.Name[..7], out geom!)) continue;
                } else if (Regex.IsMatch(p.Name, @"^AEG\d\d\d_\d\d\d")) {
                    pieceNames.Add(p.Name[..10]);
                    //if (!geometry.TryGetValue(p.Name[..10], out geom!)) continue;
                } else if (Regex.IsMatch(p.ModelName, @"^m\d\d\d\d\d\d\d\d")) {
                    pieceNames.Add(p.ModelName[..9]);
                } else {
                    var m = Regex.Match(p.Name, @".*(AEG\d\d\d_\d\d\d).*");
                    if (m.Success) {
                        pieceNames.Add(m.Groups[1].Value);
                    } else continue;
                }
                //placements.Add(new MapModelPlacement(geom, VecToXna(p.Position), VecToXna(p.Rotation), VecToXna(p.Scale)));
                Program.progress.CurrentIndex++;
            }
            foreach (var p in msb.Parts.Collisions) {
                //MapModel geom;
                if (Regex.IsMatch(p.Name, @"^m\d\d\d\d\d\d")) {
                    pieceNames.Add(p.Name[..7]);
                    //if (!geometry.TryGetValue(p.Name[..7], out geom!)) continue;
                } else if (Regex.IsMatch(p.Name, @"^AEG\d\d\d_\d\d\d")) {
                    pieceNames.Add(p.Name[..10]);
                    //if (!geometry.TryGetValue(p.Name[..10], out geom!)) continue;
                } else if (Regex.IsMatch(p.ModelName, @"^m\d\d\d\d\d\d\d\d")) {
                    pieceNames.Add(p.ModelName[..9]);
                } else {
                    var m = Regex.Match(p.Name, @".*(AEG\d\d\d_\d\d\d).*");
                    if (m.Success) {
                        pieceNames.Add(m.Groups[1].Value);
                    } else continue;
                }
                //placements.Add(new MapModelPlacement(geom, VecToXna(p.Position), VecToXna(p.Rotation), VecToXna(p.Scale)));
                Program.progress.CurrentIndex++;
            }
            foreach (var p in msb.Parts.ConnectCollisions) {
                //MapModel geom;
                if (Regex.IsMatch(p.Name, @"^m\d\d\d\d\d\d")) {
                    pieceNames.Add(p.Name[..7]);
                    //if (!geometry.TryGetValue(p.Name[..7], out geom!)) continue;
                } else if (Regex.IsMatch(p.Name, @"^AEG\d\d\d_\d\d\d")) {
                    pieceNames.Add(p.Name[..10]);
                    //if (!geometry.TryGetValue(p.Name[..10], out geom!)) continue;
                } else if (Regex.IsMatch(p.ModelName, @"^m\d\d\d\d\d\d\d\d")) {
                    pieceNames.Add(p.ModelName[..9]);
                } else {
                    var m = Regex.Match(p.Name, @".*(AEG\d\d\d_\d\d\d).*");
                    if (m.Success) {
                        pieceNames.Add(m.Groups[1].Value);
                    } else continue;
                }
                //placements.Add(new MapModelPlacement(geom, VecToXna(p.Position), VecToXna(p.Rotation), VecToXna(p.Scale)));
                Program.progress.CurrentIndex++;
            }
            foreach (var p in msb.Parts.Unk1s) {
                //MapModel geom;
                if (Regex.IsMatch(p.Name, @"^m\d\d\d\d\d\d")) {
                    pieceNames.Add(p.Name[..7]);
                    //if (!geometry.TryGetValue(p.Name[..7], out geom!)) continue;
                } else if (Regex.IsMatch(p.Name, @"^AEG\d\d\d_\d\d\d")) {
                    pieceNames.Add(p.Name[..10]);
                    //if (!geometry.TryGetValue(p.Name[..10], out geom!)) continue;
                } else if (Regex.IsMatch(p.Name, @"^m\d\d\d\d\d\d\d\d")) {
                    pieceNames.Add(p.Name[..9]);
                } else {
                    var m = Regex.Match(p.Name, @".*(AEG\d\d\d_\d\d\d).*");
                    if (m.Success) {
                        pieceNames.Add(m.Groups[1].Value);
                    } else continue;
                }
                //placements.Add(new MapModelPlacement(geom, VecToXna(p.Position), VecToXna(p.Rotation), VecToXna(p.Scale)));
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
                //secondPart is format "xxxxxx"
                var secondPart = mp.Name[1..];
                //mxx_xx_xx_xx_xxxxxx
                var fullName = firstPart + "_" + secondPart;
                var fileName = Path.Combine(mapGeomFolder, firstPart, fullName, "Model", fullName+".flver");
                if (!File.Exists(fileName)) continue;
                var flver = FLVER2.Read(fileName);
                geometry[mp.Name] = new MapModel(flver);
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
                var fileName = Path.Combine(geomFolder, start, g.Name, "sib", g.Name+".flver");
                if (!File.Exists(fileName)) continue;
                try {
                    var flver = FLVER2.Read(fileName);
                    geometry[g.Name] = new MapModel(flver);
                } catch {
                    Debug.WriteLine($"Couldn't read flver {fileName}");
                }
                Program.progress.CurrentIndex++;
            }
            placements = new List<MapModelPlacement>();
            Program.progress.TaskName = "Loading placements";
            Program.progress.CurrentIndex = 0;
            Program.progress.MaxIndex = msb.Parts.Unk1s.Count + msb.Parts.MapPieces.Count + msb.Parts.Objects.Count;
            foreach (var p in msb.Parts.MapPieces) {
                MapModel geom;
                if (Regex.IsMatch(p.Name, @"^m\d\d\d\d\d\d")) {
                    if (!geometry.TryGetValue(p.Name[..7], out geom!)) continue;
                } else if (Regex.IsMatch(p.Name, @"^AEG\d\d\d_\d\d\d")) {
                    if (!geometry.TryGetValue(p.Name[..10], out geom!)) continue;
                } else if (Regex.IsMatch(p.ModelName, @"^m\d\d\d\d\d\d\d\d")) {
                    if (!geometry.TryGetValue(p.ModelName[..9], out geom!)) continue;
                } else {
                    var m = Regex.Match(p.Name, @".*(AEG\d\d\d_\d\d\d).*");
                    if (m.Success) {
                        if (!geometry.TryGetValue(m.Groups[1].Value, out geom!)) continue;
                    } else continue;
                };
                var placement = new MapModelPlacement(geom, VecToXna(p.Position), VecToXna(p.Rotation), VecToXna(p.Scale));
                //if (p.ModelName.StartsWith("m")) placement.transform = Matrix.CreateTranslation(new Vector3(geom.bbMin.X, p.Position.Y-geom.bbMax.Y, -geom.bbMax.Z));
                placements.Add(placement);
                Program.progress.CurrentIndex++;
            }
            foreach (var p in msb.Parts.Objects) {
                MapModel geom;
                if (Regex.IsMatch(p.Name, @"^m\d\d\d\d\d\d")) {
                    if (!geometry.TryGetValue(p.Name[..7], out geom!)) continue;
                } else if (Regex.IsMatch(p.Name, @"^AEG\d\d\d_\d\d\d")) {
                    if (!geometry.TryGetValue(p.Name[..10], out geom!)) continue;
                } else if (Regex.IsMatch(p.ModelName, @"^m\d\d\d\d\d\d\d\d")) {
                    if (!geometry.TryGetValue(p.ModelName[..9], out geom!)) continue;
                } else {
                    var m = Regex.Match(p.Name, @".*(AEG\d\d\d_\d\d\d).*");
                    if (m.Success) {
                        if (!geometry.TryGetValue(m.Groups[1].Value, out geom!)) continue;
                    } else continue;
                };
                var placement = new MapModelPlacement(geom, VecToXna(p.Position), VecToXna(p.Rotation), VecToXna(p.Scale)); 
                if (p.ModelName.StartsWith("m")) placement.transform = Matrix.CreateTranslation(new Vector3(geom.bbMin.X, p.Position.Y - geom.bbMin.Y, -geom.bbMax.Z));
                //placement.transform = Matrix.CreateTranslation(new Vector3(p.Position.X + 28, 0, p.Position.Z - 58));// * Matrix.CreateRotationX(-3*MathHelper.ToRadians(p.Rotation.Y));
                placements.Add(placement);
                Program.progress.CurrentIndex++;
            }
            foreach (var p in msb.Parts.Collisions) {
                MapModel geom;
                if (Regex.IsMatch(p.Name, @"^m\d\d\d\d\d\d")) {
                    if (!geometry.TryGetValue(p.Name[..7], out geom!)) continue;
                } else if (Regex.IsMatch(p.Name, @"^AEG\d\d\d_\d\d\d")) {
                    if (!geometry.TryGetValue(p.Name[..10], out geom!)) continue;
                } else if (Regex.IsMatch(p.ModelName, @"^m\d\d\d\d\d\d\d\d")) {
                    if (!geometry.TryGetValue(p.ModelName[..9], out geom!)) continue;
                } else {
                    var m = Regex.Match(p.Name, @".*(AEG\d\d\d_\d\d\d).*");
                    if (m.Success) {
                        if (!geometry.TryGetValue(m.Groups[1].Value, out geom!)) continue;
                    } else continue;
                };
                var placement = new MapModelPlacement(geom, VecToXna(p.Position), VecToXna(p.Rotation), VecToXna(p.Scale));
                if (p.ModelName.StartsWith("m")) placement.transform = Matrix.CreateTranslation(new Vector3(geom.bbMin.X, p.Position.Y - geom.bbMin.Y, -geom.bbMax.Z));
                //placement.transform = Matrix.CreateTranslation(new Vector3(p.Position.X + 28, 0, p.Position.Z - 58));// * Matrix.CreateRotationX(-3*MathHelper.ToRadians(p.Rotation.Y));
                placements.Add(placement);
                Program.progress.CurrentIndex++;
            }
            foreach (var p in msb.Parts.ConnectCollisions) {
                MapModel geom;
                if (Regex.IsMatch(p.Name, @"^m\d\d\d\d\d\d")) {
                    if (!geometry.TryGetValue(p.Name[..7], out geom!)) continue;
                } else if (Regex.IsMatch(p.Name, @"^AEG\d\d\d_\d\d\d")) {
                    if (!geometry.TryGetValue(p.Name[..10], out geom!)) continue;
                } else if (Regex.IsMatch(p.ModelName, @"^m\d\d\d\d\d\d\d\d")) {
                    if (!geometry.TryGetValue(p.ModelName[..9], out geom!)) continue;
                } else {
                    var m = Regex.Match(p.Name, @".*(AEG\d\d\d_\d\d\d).*");
                    if (m.Success) {
                        if (!geometry.TryGetValue(m.Groups[1].Value, out geom!)) continue;
                    } else continue;
                };
                var placement = new MapModelPlacement(geom, new Vector3(geom.bbMin.X, p.Position.Y - geom.bbMin.Y, p.Position.Z * 2 - geom.bbMin.Z), VecToXna(p.Rotation), VecToXna(p.Scale));
                if (p.ModelName.StartsWith("m")) placement.transform = Matrix.CreateTranslation(new Vector3(geom.bbMin.X, p.Position.Y - geom.bbMin.Y, -geom.bbMax.Z)) * Matrix.CreateScale(VecToXna(p.Scale));
                //placement.transform = Matrix.CreateTranslation(new Vector3(p.Position.X + 28, 0, p.Position.Z - 58));// * Matrix.CreateRotationX(-3*MathHelper.ToRadians(p.Rotation.Y));
                placements.Add(placement);
                Program.progress.CurrentIndex++;
            }
            foreach (var p in msb.Parts.Unk1s) {
                MapModel geom;
                if (Regex.IsMatch(p.Name, @"^m\d\d\d\d\d\d")) {
                    if (!geometry.TryGetValue(p.Name[..7], out geom!)) continue;
                } else if (Regex.IsMatch(p.Name, @"^AEG\d\d\d_\d\d\d")) {
                    if (!geometry.TryGetValue(p.Name[..10], out geom!)) continue;
                } else if (Regex.IsMatch(p.Name, @"^m\d\d\d\d\d\d\d\d")) {
                    if (!geometry.TryGetValue(p.Name[..9], out geom!)) continue;
                } else {
                    var m = Regex.Match(p.Name, @".*(AEG\d\d\d_\d\d\d).*");
                    if (m.Success) {
                        if (!geometry.TryGetValue(m.Groups[1].Value, out geom!)) continue;
                    } else continue;
                };
                placements.Add(new MapModelPlacement(geom, VecToXna(p.Position), VecToXna(p.Rotation), VecToXna(p.Scale)));
                Program.progress.CurrentIndex++;
            }
            Program.progress.Finish();
        }

        public static Vector3 VecToXna(System.Numerics.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }
    }
}
