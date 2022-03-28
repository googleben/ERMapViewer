using System.ComponentModel;
using System.Diagnostics;

namespace ERMapViewer
{
    public partial class Form1 : Form
    {
        MonoViewer? viewer = null;
        string folder = @"\\WyattServer\Ben\EldenRing";
        public Form1()
        {
            InitializeComponent();
            Program.progress = new DelegateProgressIndicator("", s => status.Invoke(() => status.Text = s));
            LoadMSBs(@"\\WyattServer\Ben\EldenRing\Data2\map\mapstudio");
            msbDgv.AutoGenerateColumns = false;
            msbDgv.DataSource = msbs;
            msbDgv.Columns[0].DataPropertyName = "Name";
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
            new Thread(() => {
                var msb = msbDgv.Rows[e.RowIndex].DataBoundItem as MsbWrapper;
                Thread.CurrentThread.IsBackground = true;
                if (msb == null) {
                    Debug.WriteLine("null msbwrapper");
                    return;
                }
                var map = new Map(folder, msb.Msb);
                if (viewer != null) {
                    viewer.nextMap = map;
                    return;
                }
                viewer = new MonoViewer(map);
                viewer.Run();
            }).Start();
        }
    }
}