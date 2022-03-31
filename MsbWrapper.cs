using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMapViewer
{
    internal class MsbWrapper : IComparable<MsbWrapper>
    {
        public string Name { get; }
        public BHD5.FileHeader Header { get; }

        private MSBE? _msb;
        public MSBE Msb
        {
            get {
                if (_msb == null) {
                    var data = Header.ReadFile(Program.data2Bdt);
                    var decompressed = DCX.Decompress(data);
                    _msb = MSBE.Read(decompressed);
                }
                return _msb;
            }
        }
        public MsbWrapper(BHD5.FileHeader header, string name)
        {
            Name = name;
            if (MapNames.Names.TryGetValue(name, out var nameStr)) Name = $"{name} ({nameStr})";
            Header = header;
            this._msb = null;
        }
        public int CompareTo(MsbWrapper? other) => Name.CompareTo(other?.Name);
    }
}
