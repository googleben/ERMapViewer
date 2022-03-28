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
        public string Path { get; }

        private MSBE? _msb;
        public MSBE Msb
        {
            get {
                if (_msb == null) {
                    _msb = MSBE.Read(Path);
                }
                return _msb;
            }
        }
        public MsbWrapper(string path, string name)
        {
            Name = name;
            Path = path;
            this._msb = null;
        }
        public int CompareTo(MsbWrapper? other) => Name.CompareTo(other?.Name);
    }
}
