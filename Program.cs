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

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MapSelector());
        }
    }
}