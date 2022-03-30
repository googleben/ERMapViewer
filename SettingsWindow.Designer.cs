namespace ERMapViewer
{
    partial class SettingsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.highPerfModeControl = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.selectingRegionsControl = new System.Windows.Forms.CheckBox();
            this.selectEmptyObjectsControl = new System.Windows.Forms.CheckBox();
            this.showRegionsControl = new System.Windows.Forms.CheckBox();
            this.wireframeObjectsControl = new System.Windows.Forms.CheckBox();
            this.cameraSpeedControl = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.showInvasionPointsControl = new System.Windows.Forms.CheckBox();
            this.showEnvironmentMapPointsControl = new System.Windows.Forms.CheckBox();
            this.showSoundsControl = new System.Windows.Forms.CheckBox();
            this.showSfxControl = new System.Windows.Forms.CheckBox();
            this.showWindSfxControl = new System.Windows.Forms.CheckBox();
            this.showSpawnPointsControl = new System.Windows.Forms.CheckBox();
            this.showMessagesControl = new System.Windows.Forms.CheckBox();
            this.showPatrolRoutesControl = new System.Windows.Forms.CheckBox();
            this.showWarpPointsControl = new System.Windows.Forms.CheckBox();
            this.showActivationAreasControl = new System.Windows.Forms.CheckBox();
            this.showEventsControl = new System.Windows.Forms.CheckBox();
            this.showLogicControl = new System.Windows.Forms.CheckBox();
            this.showEnvironmentMapEffectsControl = new System.Windows.Forms.CheckBox();
            this.showWindAreasControl = new System.Windows.Forms.CheckBox();
            this.showMufflingBoxesControl = new System.Windows.Forms.CheckBox();
            this.showMufflingPortalsControl = new System.Windows.Forms.CheckBox();
            this.showSoundSpaceOverridesControl = new System.Windows.Forms.CheckBox();
            this.showMufflingPlanesControl = new System.Windows.Forms.CheckBox();
            this.showPartsGroupAreasControl = new System.Windows.Forms.CheckBox();
            this.showAutoDrawGroupPointsControl = new System.Windows.Forms.CheckBox();
            this.showUnknownControl = new System.Windows.Forms.CheckBox();
            this.showOthersControl = new System.Windows.Forms.CheckBox();
            this.selectionInformationControl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.currentMapControl = new System.Windows.Forms.Label();
            this.showFpsControl = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.cameraSpeedControl)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // highPerfModeControl
            // 
            this.highPerfModeControl.AutoSize = true;
            this.highPerfModeControl.Location = new System.Drawing.Point(6, 44);
            this.highPerfModeControl.Name = "highPerfModeControl";
            this.highPerfModeControl.Size = new System.Drawing.Size(181, 19);
            this.highPerfModeControl.TabIndex = 0;
            this.highPerfModeControl.Text = "Increased Performance Mode";
            this.toolTip1.SetToolTip(this.highPerfModeControl, "Skips rendering far away objects and always renders the lowest LOD");
            this.highPerfModeControl.UseVisualStyleBackColor = true;
            // 
            // selectingRegionsControl
            // 
            this.selectingRegionsControl.AutoSize = true;
            this.selectingRegionsControl.Location = new System.Drawing.Point(6, 123);
            this.selectingRegionsControl.Name = "selectingRegionsControl";
            this.selectingRegionsControl.Size = new System.Drawing.Size(119, 19);
            this.selectingRegionsControl.TabIndex = 5;
            this.selectingRegionsControl.Text = "Selecting Regions";
            this.toolTip1.SetToolTip(this.selectingRegionsControl, "Select regions when right-clicking instead of objects");
            this.selectingRegionsControl.UseVisualStyleBackColor = true;
            // 
            // selectEmptyObjectsControl
            // 
            this.selectEmptyObjectsControl.AutoSize = true;
            this.selectEmptyObjectsControl.Location = new System.Drawing.Point(6, 148);
            this.selectEmptyObjectsControl.Name = "selectEmptyObjectsControl";
            this.selectEmptyObjectsControl.Size = new System.Drawing.Size(137, 19);
            this.selectEmptyObjectsControl.TabIndex = 6;
            this.selectEmptyObjectsControl.Text = "Select Empty Objects";
            this.toolTip1.SetToolTip(this.selectEmptyObjectsControl, "Allow the selection of objects with 0 triangles (total, over all LODs)");
            this.selectEmptyObjectsControl.UseVisualStyleBackColor = true;
            // 
            // showRegionsControl
            // 
            this.showRegionsControl.AutoSize = true;
            this.showRegionsControl.Location = new System.Drawing.Point(6, 22);
            this.showRegionsControl.Name = "showRegionsControl";
            this.showRegionsControl.Size = new System.Drawing.Size(100, 19);
            this.showRegionsControl.TabIndex = 1;
            this.showRegionsControl.Text = "Show Regions";
            this.showRegionsControl.UseVisualStyleBackColor = true;
            // 
            // wireframeObjectsControl
            // 
            this.wireframeObjectsControl.AutoSize = true;
            this.wireframeObjectsControl.Location = new System.Drawing.Point(6, 69);
            this.wireframeObjectsControl.Name = "wireframeObjectsControl";
            this.wireframeObjectsControl.Size = new System.Drawing.Size(180, 19);
            this.wireframeObjectsControl.TabIndex = 2;
            this.wireframeObjectsControl.Text = "Render Objects As Wireframe";
            this.wireframeObjectsControl.UseVisualStyleBackColor = true;
            // 
            // cameraSpeedControl
            // 
            this.cameraSpeedControl.DecimalPlaces = 2;
            this.cameraSpeedControl.Location = new System.Drawing.Point(95, 94);
            this.cameraSpeedControl.Name = "cameraSpeedControl";
            this.cameraSpeedControl.Size = new System.Drawing.Size(102, 23);
            this.cameraSpeedControl.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Camera Speed";
            // 
            // showInvasionPointsControl
            // 
            this.showInvasionPointsControl.AutoSize = true;
            this.showInvasionPointsControl.Location = new System.Drawing.Point(6, 22);
            this.showInvasionPointsControl.Name = "showInvasionPointsControl";
            this.showInvasionPointsControl.Size = new System.Drawing.Size(106, 19);
            this.showInvasionPointsControl.TabIndex = 6;
            this.showInvasionPointsControl.Text = "Invasion Points";
            this.showInvasionPointsControl.UseVisualStyleBackColor = true;
            // 
            // showEnvironmentMapPointsControl
            // 
            this.showEnvironmentMapPointsControl.AutoSize = true;
            this.showEnvironmentMapPointsControl.Location = new System.Drawing.Point(6, 47);
            this.showEnvironmentMapPointsControl.Name = "showEnvironmentMapPointsControl";
            this.showEnvironmentMapPointsControl.Size = new System.Drawing.Size(157, 19);
            this.showEnvironmentMapPointsControl.TabIndex = 8;
            this.showEnvironmentMapPointsControl.Text = "Environment Map Points";
            this.showEnvironmentMapPointsControl.UseVisualStyleBackColor = true;
            // 
            // showSoundsControl
            // 
            this.showSoundsControl.AutoSize = true;
            this.showSoundsControl.Location = new System.Drawing.Point(6, 72);
            this.showSoundsControl.Name = "showSoundsControl";
            this.showSoundsControl.Size = new System.Drawing.Size(65, 19);
            this.showSoundsControl.TabIndex = 9;
            this.showSoundsControl.Text = "Sounds";
            this.showSoundsControl.UseVisualStyleBackColor = true;
            // 
            // showSfxControl
            // 
            this.showSfxControl.AutoSize = true;
            this.showSfxControl.Location = new System.Drawing.Point(6, 97);
            this.showSfxControl.Name = "showSfxControl";
            this.showSfxControl.Size = new System.Drawing.Size(45, 19);
            this.showSfxControl.TabIndex = 10;
            this.showSfxControl.Text = "SFX";
            this.showSfxControl.UseVisualStyleBackColor = true;
            // 
            // showWindSfxControl
            // 
            this.showWindSfxControl.AutoSize = true;
            this.showWindSfxControl.Location = new System.Drawing.Point(6, 122);
            this.showWindSfxControl.Name = "showWindSfxControl";
            this.showWindSfxControl.Size = new System.Drawing.Size(73, 19);
            this.showWindSfxControl.TabIndex = 11;
            this.showWindSfxControl.Text = "WindSFX";
            this.showWindSfxControl.UseVisualStyleBackColor = true;
            // 
            // showSpawnPointsControl
            // 
            this.showSpawnPointsControl.AutoSize = true;
            this.showSpawnPointsControl.Location = new System.Drawing.Point(6, 147);
            this.showSpawnPointsControl.Name = "showSpawnPointsControl";
            this.showSpawnPointsControl.Size = new System.Drawing.Size(94, 19);
            this.showSpawnPointsControl.TabIndex = 12;
            this.showSpawnPointsControl.Text = "SpawnPoints";
            this.showSpawnPointsControl.UseVisualStyleBackColor = true;
            // 
            // showMessagesControl
            // 
            this.showMessagesControl.AutoSize = true;
            this.showMessagesControl.Location = new System.Drawing.Point(6, 172);
            this.showMessagesControl.Name = "showMessagesControl";
            this.showMessagesControl.Size = new System.Drawing.Size(77, 19);
            this.showMessagesControl.TabIndex = 13;
            this.showMessagesControl.Text = "Messages";
            this.showMessagesControl.UseVisualStyleBackColor = true;
            // 
            // showPatrolRoutesControl
            // 
            this.showPatrolRoutesControl.AutoSize = true;
            this.showPatrolRoutesControl.Location = new System.Drawing.Point(6, 197);
            this.showPatrolRoutesControl.Name = "showPatrolRoutesControl";
            this.showPatrolRoutesControl.Size = new System.Drawing.Size(93, 19);
            this.showPatrolRoutesControl.TabIndex = 14;
            this.showPatrolRoutesControl.Text = "PatrolRoutes";
            this.showPatrolRoutesControl.UseVisualStyleBackColor = true;
            // 
            // showWarpPointsControl
            // 
            this.showWarpPointsControl.AutoSize = true;
            this.showWarpPointsControl.Location = new System.Drawing.Point(6, 222);
            this.showWarpPointsControl.Name = "showWarpPointsControl";
            this.showWarpPointsControl.Size = new System.Drawing.Size(87, 19);
            this.showWarpPointsControl.TabIndex = 15;
            this.showWarpPointsControl.Text = "WarpPoints";
            this.showWarpPointsControl.UseVisualStyleBackColor = true;
            // 
            // showActivationAreasControl
            // 
            this.showActivationAreasControl.AutoSize = true;
            this.showActivationAreasControl.Location = new System.Drawing.Point(6, 247);
            this.showActivationAreasControl.Name = "showActivationAreasControl";
            this.showActivationAreasControl.Size = new System.Drawing.Size(112, 19);
            this.showActivationAreasControl.TabIndex = 16;
            this.showActivationAreasControl.Text = "Activation Areas";
            this.showActivationAreasControl.UseVisualStyleBackColor = true;
            // 
            // showEventsControl
            // 
            this.showEventsControl.AutoSize = true;
            this.showEventsControl.Location = new System.Drawing.Point(6, 272);
            this.showEventsControl.Name = "showEventsControl";
            this.showEventsControl.Size = new System.Drawing.Size(60, 19);
            this.showEventsControl.TabIndex = 17;
            this.showEventsControl.Text = "Events";
            this.showEventsControl.UseVisualStyleBackColor = true;
            // 
            // showLogicControl
            // 
            this.showLogicControl.AutoSize = true;
            this.showLogicControl.Location = new System.Drawing.Point(182, 22);
            this.showLogicControl.Name = "showLogicControl";
            this.showLogicControl.Size = new System.Drawing.Size(55, 19);
            this.showLogicControl.TabIndex = 18;
            this.showLogicControl.Text = "Logic";
            this.showLogicControl.UseVisualStyleBackColor = true;
            // 
            // showEnvironmentMapEffectsControl
            // 
            this.showEnvironmentMapEffectsControl.AutoSize = true;
            this.showEnvironmentMapEffectsControl.Location = new System.Drawing.Point(182, 47);
            this.showEnvironmentMapEffectsControl.Name = "showEnvironmentMapEffectsControl";
            this.showEnvironmentMapEffectsControl.Size = new System.Drawing.Size(159, 19);
            this.showEnvironmentMapEffectsControl.TabIndex = 19;
            this.showEnvironmentMapEffectsControl.Text = "Environment Map Effects";
            this.showEnvironmentMapEffectsControl.UseVisualStyleBackColor = true;
            // 
            // showWindAreasControl
            // 
            this.showWindAreasControl.AutoSize = true;
            this.showWindAreasControl.Location = new System.Drawing.Point(182, 72);
            this.showWindAreasControl.Name = "showWindAreasControl";
            this.showWindAreasControl.Size = new System.Drawing.Size(86, 19);
            this.showWindAreasControl.TabIndex = 20;
            this.showWindAreasControl.Text = "Wind Areas";
            this.showWindAreasControl.UseVisualStyleBackColor = true;
            // 
            // showMufflingBoxesControl
            // 
            this.showMufflingBoxesControl.AutoSize = true;
            this.showMufflingBoxesControl.Location = new System.Drawing.Point(182, 97);
            this.showMufflingBoxesControl.Name = "showMufflingBoxesControl";
            this.showMufflingBoxesControl.Size = new System.Drawing.Size(106, 19);
            this.showMufflingBoxesControl.TabIndex = 21;
            this.showMufflingBoxesControl.Text = "Muffling Boxes";
            this.showMufflingBoxesControl.UseVisualStyleBackColor = true;
            // 
            // showMufflingPortalsControl
            // 
            this.showMufflingPortalsControl.AutoSize = true;
            this.showMufflingPortalsControl.Location = new System.Drawing.Point(182, 122);
            this.showMufflingPortalsControl.Name = "showMufflingPortalsControl";
            this.showMufflingPortalsControl.Size = new System.Drawing.Size(111, 19);
            this.showMufflingPortalsControl.TabIndex = 22;
            this.showMufflingPortalsControl.Text = "Muffling Portals";
            this.showMufflingPortalsControl.UseVisualStyleBackColor = true;
            // 
            // showSoundSpaceOverridesControl
            // 
            this.showSoundSpaceOverridesControl.AutoSize = true;
            this.showSoundSpaceOverridesControl.Location = new System.Drawing.Point(182, 147);
            this.showSoundSpaceOverridesControl.Name = "showSoundSpaceOverridesControl";
            this.showSoundSpaceOverridesControl.Size = new System.Drawing.Size(147, 19);
            this.showSoundSpaceOverridesControl.TabIndex = 23;
            this.showSoundSpaceOverridesControl.Text = "Sound Space Overrides";
            this.showSoundSpaceOverridesControl.UseVisualStyleBackColor = true;
            // 
            // showMufflingPlanesControl
            // 
            this.showMufflingPlanesControl.AutoSize = true;
            this.showMufflingPlanesControl.Location = new System.Drawing.Point(182, 172);
            this.showMufflingPlanesControl.Name = "showMufflingPlanesControl";
            this.showMufflingPlanesControl.Size = new System.Drawing.Size(109, 19);
            this.showMufflingPlanesControl.TabIndex = 24;
            this.showMufflingPlanesControl.Text = "Muffling Planes";
            this.showMufflingPlanesControl.UseVisualStyleBackColor = true;
            // 
            // showPartsGroupAreasControl
            // 
            this.showPartsGroupAreasControl.AutoSize = true;
            this.showPartsGroupAreasControl.Location = new System.Drawing.Point(182, 197);
            this.showPartsGroupAreasControl.Name = "showPartsGroupAreasControl";
            this.showPartsGroupAreasControl.Size = new System.Drawing.Size(120, 19);
            this.showPartsGroupAreasControl.TabIndex = 25;
            this.showPartsGroupAreasControl.Text = "Parts Group Areas";
            this.showPartsGroupAreasControl.UseVisualStyleBackColor = true;
            // 
            // showAutoDrawGroupPointsControl
            // 
            this.showAutoDrawGroupPointsControl.AutoSize = true;
            this.showAutoDrawGroupPointsControl.Location = new System.Drawing.Point(182, 222);
            this.showAutoDrawGroupPointsControl.Name = "showAutoDrawGroupPointsControl";
            this.showAutoDrawGroupPointsControl.Size = new System.Drawing.Size(154, 19);
            this.showAutoDrawGroupPointsControl.TabIndex = 26;
            this.showAutoDrawGroupPointsControl.Text = "Auto Draw Group Points";
            this.showAutoDrawGroupPointsControl.UseVisualStyleBackColor = true;
            // 
            // showUnknownControl
            // 
            this.showUnknownControl.AutoSize = true;
            this.showUnknownControl.Location = new System.Drawing.Point(182, 247);
            this.showUnknownControl.Name = "showUnknownControl";
            this.showUnknownControl.Size = new System.Drawing.Size(77, 19);
            this.showUnknownControl.TabIndex = 27;
            this.showUnknownControl.Text = "Unknown";
            this.showUnknownControl.UseVisualStyleBackColor = true;
            // 
            // showOthersControl
            // 
            this.showOthersControl.AutoSize = true;
            this.showOthersControl.Location = new System.Drawing.Point(182, 272);
            this.showOthersControl.Name = "showOthersControl";
            this.showOthersControl.Size = new System.Drawing.Size(61, 19);
            this.showOthersControl.TabIndex = 28;
            this.showOthersControl.Text = "Others";
            this.showOthersControl.UseVisualStyleBackColor = true;
            // 
            // selectionInformationControl
            // 
            this.selectionInformationControl.AcceptsReturn = true;
            this.selectionInformationControl.AcceptsTab = true;
            this.selectionInformationControl.Location = new System.Drawing.Point(12, 360);
            this.selectionInformationControl.Multiline = true;
            this.selectionInformationControl.Name = "selectionInformationControl";
            this.selectionInformationControl.ReadOnly = true;
            this.selectionInformationControl.Size = new System.Drawing.Size(625, 178);
            this.selectionInformationControl.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 342);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 15);
            this.label3.TabIndex = 30;
            this.label3.Text = "Selection Information:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.showFpsControl);
            this.groupBox1.Controls.Add(this.selectEmptyObjectsControl);
            this.groupBox1.Controls.Add(this.selectingRegionsControl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cameraSpeedControl);
            this.groupBox1.Controls.Add(this.wireframeObjectsControl);
            this.groupBox1.Controls.Add(this.highPerfModeControl);
            this.groupBox1.Controls.Add(this.showRegionsControl);
            this.groupBox1.Location = new System.Drawing.Point(12, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 305);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.showInvasionPointsControl);
            this.groupBox2.Controls.Add(this.showEnvironmentMapPointsControl);
            this.groupBox2.Controls.Add(this.showSoundsControl);
            this.groupBox2.Controls.Add(this.showSfxControl);
            this.groupBox2.Controls.Add(this.showOthersControl);
            this.groupBox2.Controls.Add(this.showWindSfxControl);
            this.groupBox2.Controls.Add(this.showUnknownControl);
            this.groupBox2.Controls.Add(this.showSpawnPointsControl);
            this.groupBox2.Controls.Add(this.showAutoDrawGroupPointsControl);
            this.groupBox2.Controls.Add(this.showMessagesControl);
            this.groupBox2.Controls.Add(this.showPartsGroupAreasControl);
            this.groupBox2.Controls.Add(this.showPatrolRoutesControl);
            this.groupBox2.Controls.Add(this.showMufflingPlanesControl);
            this.groupBox2.Controls.Add(this.showWarpPointsControl);
            this.groupBox2.Controls.Add(this.showSoundSpaceOverridesControl);
            this.groupBox2.Controls.Add(this.showActivationAreasControl);
            this.groupBox2.Controls.Add(this.showMufflingPortalsControl);
            this.groupBox2.Controls.Add(this.showEventsControl);
            this.groupBox2.Controls.Add(this.showMufflingBoxesControl);
            this.groupBox2.Controls.Add(this.showLogicControl);
            this.groupBox2.Controls.Add(this.showWindAreasControl);
            this.groupBox2.Controls.Add(this.showEnvironmentMapEffectsControl);
            this.groupBox2.Location = new System.Drawing.Point(293, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 327);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Visible Region Types";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(87, 297);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 30;
            this.button2.Text = "Hide All";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 297);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "Show All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // currentMapControl
            // 
            this.currentMapControl.AutoSize = true;
            this.currentMapControl.Location = new System.Drawing.Point(12, 12);
            this.currentMapControl.Name = "currentMapControl";
            this.currentMapControl.Size = new System.Drawing.Size(97, 15);
            this.currentMapControl.TabIndex = 34;
            this.currentMapControl.Text = "No Map Selected";
            // 
            // showFpsControl
            // 
            this.showFpsControl.AutoSize = true;
            this.showFpsControl.Location = new System.Drawing.Point(6, 173);
            this.showFpsControl.Name = "showFpsControl";
            this.showFpsControl.Size = new System.Drawing.Size(77, 19);
            this.showFpsControl.TabIndex = 7;
            this.showFpsControl.Text = "Show FPS";
            this.showFpsControl.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 550);
            this.Controls.Add(this.currentMapControl);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.selectionInformationControl);
            this.MaximizeBox = false;
            this.Name = "SettingsWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Settings and Information";
            ((System.ComponentModel.ISupportInitialize)(this.cameraSpeedControl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox highPerfModeControl;
        private ToolTip toolTip1;
        private CheckBox showRegionsControl;
        private CheckBox wireframeObjectsControl;
        private NumericUpDown cameraSpeedControl;
        private Label label1;
        private CheckBox selectingRegionsControl;
        private CheckBox showInvasionPointsControl;
        private CheckBox showEnvironmentMapPointsControl;
        private CheckBox showSoundsControl;
        private CheckBox showSfxControl;
        private CheckBox showWindSfxControl;
        private CheckBox showSpawnPointsControl;
        private CheckBox showMessagesControl;
        private CheckBox showPatrolRoutesControl;
        private CheckBox showWarpPointsControl;
        private CheckBox showActivationAreasControl;
        private CheckBox showEventsControl;
        private CheckBox showLogicControl;
        private CheckBox showEnvironmentMapEffectsControl;
        private CheckBox showWindAreasControl;
        private CheckBox showMufflingBoxesControl;
        private CheckBox showMufflingPortalsControl;
        private CheckBox showSoundSpaceOverridesControl;
        private CheckBox showMufflingPlanesControl;
        private CheckBox showPartsGroupAreasControl;
        private CheckBox showAutoDrawGroupPointsControl;
        private CheckBox showUnknownControl;
        private CheckBox showOthersControl;
        private TextBox selectionInformationControl;
        private Label label3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label currentMapControl;
        private Button button2;
        private Button button1;
        private CheckBox selectEmptyObjectsControl;
        private CheckBox showFpsControl;
    }
}