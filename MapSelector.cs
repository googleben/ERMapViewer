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
        public MapSelector()
        {
            InitializeComponent();
            Program.progress = new DelegateProgressIndicator("", s => status.Invoke(() => status.Text = s));
            LoadMSBs(@"\\WyattServer\Ben\EldenRing\Data2\map\mapstudio");
            msbDgv.AutoGenerateColumns = false;
            msbDgv.DataSource = msbs;
            msbDgv.Columns[0].DataPropertyName = "Name";
            SettingsWindow.Instance = new SettingsWindow();
            SettingsWindow.Instance.Show();
        }

        BindingList<MsbWrapper> msbs = new();

        void LoadMSBs(string folderPath)
        {
            //Program.progress.TaskName = "Searching MSBs";
            List<MsbWrapper> ans = new();
            foreach (var file in Directory.GetFiles(folderPath)) {
                if (!file.EndsWith(".msb")) continue;
                ans.Add(new MsbWrapper(file, Path.GetFileNameWithoutExtension(file)));
            }
            ans.Sort();
            msbs = new BindingList<MsbWrapper>(ans);
            //Program.progress.Finish();
        }

        private void msbDgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (loading) return;
            loading = true;
            new Thread(() => {
                var msb = msbDgv.Rows[e.RowIndex].DataBoundItem as MsbWrapper;
                Thread.CurrentThread.IsBackground = true;
                if (msb == null) {
                    Debug.WriteLine("null msbwrapper");
                    return;
                }
                var map = new Map(folder, msb.Msb);
                loading = false;
                SettingsWindow.Instance.Invoke(() => SettingsWindow.Instance.CurrentMap = msb.Name);
                if (viewer != null && monoThread != null && monoThread.IsAlive) {
                    viewer.nextMap = map;
                    return;
                }
                monoThread = Thread.CurrentThread;
                viewer = new MonoViewer(map);
                viewer.Run();
            }).Start();
        }
    }
}