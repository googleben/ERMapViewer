using SoulsFormats;
using System.ComponentModel;
using System.Diagnostics;

namespace ERMapViewer
{
    public partial class MapSelector : Form
    {
        MonoViewer? viewer = null;
        string folder = @"\\WyattServer\Ben\EldenRing";
        static Thread? monoThread;
        static bool loading = false;
        public MapSelector(string[] args)
        {
            InitializeComponent();
            SettingsWindow.Instance = new SettingsWindow();
            SettingsWindow.Instance.Show();
            string gamePath;
            if (args.Length == 0 || !File.Exists(Path.Combine(args[0], "eldenring.exe"))) {
                OpenFileDialog ofd = new();
                ofd.Filter = "eldenring.exe|eldenring.exe";
                ofd.Title = "Select your Elden Ring installation";
                var result = ofd.ShowDialog();
                if (result != DialogResult.OK) return;
                gamePath = Path.GetDirectoryName(ofd.FileName)!;
            } else {
                gamePath = args[0];
            }
            new Thread(() => {
                Program.progress.MaxIndex = 0;
                Program.progress.TaskName = "Reading .bhd files...";
                Program.data1Bhd = BHD5.Read(Path.Combine(gamePath, "data1.bhd"), BHD5.Game.EldenRing);
                Program.data1Bdt = File.OpenRead(Path.Combine(gamePath, "data1.bdt"));
                Program.data2Bhd = BHD5.Read(Path.Combine(gamePath, "data2.bhd"), BHD5.Game.EldenRing);
                Program.data2Bdt = File.OpenRead(Path.Combine(gamePath, "data2.bdt"));
                Program.progress.Finish();
                LoadMSBs();
            }).Start();
        }

        BindingList<MsbWrapper> msbs = new();

        void LoadMSBs()
        {
            Program.progress.TaskName = "Searching MSBs";
            List<MsbWrapper> ans = new();
            foreach (var (fname, h) in Program.data2Bhd.fileHeaders) {
                if (fname.EndsWith(".msb.dcx")) ans.Add(new MsbWrapper(h, Path.GetFileNameWithoutExtension(fname.Replace(".msb", ""))));
            }
            ans.Sort();
            msbs = new BindingList<MsbWrapper>(ans);
            msbDgv.Invoke(() => {
                msbDgv.AutoGenerateColumns = false;
                msbDgv.DataSource = msbs;
                msbDgv.Columns[0].DataPropertyName = "Name";
            });
            Program.progress.Finish();
        }

        private void msbDgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (loading) return;
            loading = true;
            new Thread(() => {
                Thread.CurrentThread.IsBackground = true;
                if (msbDgv.Rows[e.RowIndex].DataBoundItem is not MsbWrapper msb) {
                    Debug.WriteLine("null msbwrapper");
                    return;
                }
                var map = new Map(folder, msb.Msb);
                loading = false;
                SettingsWindow.Instance.Invoke(() => SettingsWindow.Instance.CurrentMap = msb.Name);
                if (viewer != null && monoThread != null && monoThread.IsAlive) {
                    var tmp = viewer.map;
                    viewer.nextMap = map;
                    Thread.Sleep(1000);
                    tmp.Dispose();
                    return;
                }
                monoThread = Thread.CurrentThread;
                viewer = new MonoViewer(map);
                //delete our ref to the map so it can be GC'd
                map = null;
                viewer.Run();
            }).Start();
        }
    }
}