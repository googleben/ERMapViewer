using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERMapViewer
{
    public partial class SettingsWindow : Form
    {
        public static SettingsWindow Instance;

        protected override CreateParams CreateParams
        {
            get {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~0x80000;
                return cp;
            }
        }

        public string CurrentMap
        {
            get { return currentMapControl.Text; }
            set { currentMapControl.Text = value; }
        }
        public bool ShowRegions
        {
            get { return showRegionsControl.Checked; }
            set { showRegionsControl.Checked = value; }
        }
        public bool HighPerfMode
        {
            get { return highPerfModeControl.Checked; }
            set { highPerfModeControl.Checked = value; }
        }
        public bool WireframeObjects
        {
            get { return wireframeObjectsControl.Checked; }
            set { wireframeObjectsControl.Checked = value; }
        }
        public float CameraSpeed
        {
            get { return ((float)cameraSpeedControl.Value); }
            set { cameraSpeedControl.Value = Convert.ToDecimal(value); }
        }
        public bool SelectingRegions
        {
            get { return selectingRegionsControl.Checked; }
            set { selectingRegionsControl.Checked = value; }
        }
        public bool SelectEmptyObjects
        {
            get { return selectEmptyObjectsControl.Checked; }
            set { selectEmptyObjectsControl.Checked = value; }
        }
        public bool ShowFps
        {
            get { return showFpsControl.Checked; }
            set { showFpsControl.Checked = value; }
        }
        public string SelectionInformation
        {
            get { return selectionInformationControl.Text; }
            set { selectionInformationControl.Text = value; }
        }
        public bool ShowInvasionPoints
        {
            get { return showInvasionPointsControl.Checked; }
            set { showInvasionPointsControl.Checked = value;  }
        }
        public bool ShowEnvironmentMapPoints
        {
            get { return showEnvironmentMapPointsControl.Checked; }
            set { showEnvironmentMapPointsControl.Checked = value; }
        }
        public bool ShowSounds
        {
            get { return showSoundsControl.Checked; }
            set { showSoundsControl.Checked = value; }
        }
        public bool ShowSfx
        {
            get { return showSfxControl.Checked; }
            set { showSfxControl.Checked = value; }
        }
        public bool ShowWindSfx
        {
            get { return showWindSfxControl.Checked; }
            set { showWindSfxControl.Checked = value; }
        }
        public bool ShowSpawnPoints
        {
            get { return showSpawnPointsControl.Checked; }
            set { showSpawnPointsControl.Checked = value; }
        }
        public bool ShowMessages
        {
            get { return showMessagesControl.Checked; }
            set { showMessagesControl.Checked = value; }
        }
        public bool ShowPatrolRoutes
        {
            get { return showPatrolRoutesControl.Checked; }
            set { showPatrolRoutesControl.Checked = value; }
        }
        public bool ShowWarpPoints
        {
            get { return showWarpPointsControl.Checked; }
            set { showWarpPointsControl.Checked = value; }
        }
        public bool ShowActivationAreas
        {
            get { return showActivationAreasControl.Checked; }
            set { showActivationAreasControl.Checked = value; }
        }
        public bool ShowEvents
        {
            get { return showEventsControl.Checked; }
            set { showEventsControl.Checked = value; }
        }
        public bool ShowLogic
        {
            get { return showLogicControl.Checked; }
            set { showLogicControl.Checked = value; }
        }
        public bool ShowEnvironmentMapEffects
        {
            get { return showEnvironmentMapEffectsControl.Checked; }
            set { showEnvironmentMapEffectsControl.Checked = value; }
        }
        public bool ShowWindAreas
        {
            get { return showWindAreasControl.Checked; }
            set { showWindAreasControl.Checked = value; }
        }
        public bool ShowMufflingBoxes
        {
            get { return showMufflingBoxesControl.Checked; }
            set { showMufflingBoxesControl.Checked = value; }
        }
        public bool ShowMufflingPortals
        {
            get { return showMufflingPortalsControl.Checked; }
            set { showMufflingPortalsControl.Checked = value; }
        }
        public bool ShowSoundSpaceOverrides
        {
            get { return showSoundSpaceOverridesControl.Checked; }
            set { showSoundSpaceOverridesControl.Checked = value; }
        }
        public bool ShowMufflingPlanes
        {
            get { return showMufflingPlanesControl.Checked; }
            set { showMufflingPlanesControl.Checked = value; }
        }
        public bool ShowPartsGroupAreas
        {
            get { return showPartsGroupAreasControl.Checked; }
            set { showPartsGroupAreasControl.Checked = value; }
        }
        public bool ShowAutoDrawGroupPoints
        {
            get { return showAutoDrawGroupPointsControl.Checked; }
            set { showAutoDrawGroupPointsControl.Checked = value; }
        }
        public bool ShowUnknown
        {
            get { return showUnknownControl.Checked; }
            set { showUnknownControl.Checked = value; }
        }
        public bool ShowOthers
        {
            get { return showOthersControl.Checked; }
            set { showOthersControl.Checked = value; }
        }

        public void SetAllRegionShow(bool show)
        {
            ShowActivationAreas = show;
            ShowAutoDrawGroupPoints = show;
            ShowEnvironmentMapPoints = show;
            ShowEnvironmentMapEffects = show;
            ShowEvents = show;
            ShowInvasionPoints = show;
            ShowLogic = show;
            ShowMessages = show;
            ShowMufflingBoxes = show;
            ShowMufflingPlanes = show;
            ShowMufflingPortals = show;
            ShowOthers = show;
            ShowPartsGroupAreas = show;
            ShowPatrolRoutes = show;
            ShowSfx = show;
            ShowSounds = show;
            ShowSoundSpaceOverrides = show;
            ShowSpawnPoints = show;
            ShowUnknown = show;
            ShowWarpPoints = show;
            ShowWindAreas = show;
            ShowWindSfx = show;
        }

        public SettingsWindow()
        {
            InitializeComponent();
            Program.progress = new DelegateProgressIndicator("", s => status.Invoke(() => status.Text = s));
            ShowRegions = true;
            CameraSpeed = 1;
            SelectingRegions = true;
            SetAllRegionShow(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetAllRegionShow(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetAllRegionShow(false);
        }
    }
}
