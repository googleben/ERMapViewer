using SoulsFormats;

namespace ERMapViewer
{
    internal static class Program
    {

        public static ProgressIndicator progress = new NoOpProgressIndicator("");

        private static SimpleMesh? sphere;
        public static SimpleMesh Sphere { 
            get { 
                if (sphere != null) return sphere;
                var text = File.ReadAllText(@"Content\Sphere.obj");
                var obj = WavefrontObj.Read(text);
                sphere = obj.ToMesh();
                return sphere;
            }
        }
        private static SimpleMesh? circle;
        public static SimpleMesh Circle
        {
            get {
                if (circle != null) return circle;
                var text = File.ReadAllText(@"Content\Circle.obj");
                var obj = WavefrontObj.Read(text);
                circle = obj.ToMesh();
                return circle;
            }
        }
        private static SimpleMesh? cylinder;
        public static SimpleMesh Cylinder
        {
            get {
                if (cylinder != null) return cylinder;
                var text = File.ReadAllText(@"Content\Cylinder.obj");
                var obj = WavefrontObj.Read(text);
                cylinder = obj.ToMesh();
                return cylinder;
            }
        }
        private static SimpleMesh? rectangle;
        public static SimpleMesh Rectangle
        {
            get {
                if (rectangle != null) return rectangle;
                var text = File.ReadAllText(@"Content\Rectangle.obj");
                var obj = WavefrontObj.Read(text);
                rectangle = obj.ToMesh();
                return rectangle;
            }
        }
        private static SimpleMesh? cube;
        public static SimpleMesh Cube
        {
            get {
                if (cube != null) return cube;
                var text = File.ReadAllText(@"Content\Cube.obj");
                var obj = WavefrontObj.Read(text);
                cube = obj.ToMesh();
                return cube;
            }
        }

        public static BHD5 data1Bhd;
        public static FileStream data1Bdt;
        public static BHD5 data2Bhd;
        public static FileStream data2Bdt;

        public static BND4? ReadBnd(BHD5 bhd, FileStream bdt, string fileName)
        {
            var header = bhd.fileHeaders[fileName];
            if (header == null) return null;
            var dcx = header.ReadFile(bdt);
            var data = DCX.Decompress(dcx);
            return BND4.Read(data);
        }

        public static FLVER2? ReadFlver(BND4 bnd, string matchString)
        {
            foreach (var f in bnd.Files) {
                if (f.Name.Contains(matchString)) {
                    var data = f.Bytes;
                    if (f.Name.EndsWith(".dcx")) {
                        data = DCX.Decompress(data);
                    }
                    return FLVER2.Read(data);
                }
            }
            return null;
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MapSelector(args));
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
        }
    }
}