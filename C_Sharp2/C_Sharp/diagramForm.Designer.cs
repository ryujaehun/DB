namespace C_Sharp
{
    partial class diagramForm
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
            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(diagramForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.attribute = new System.Windows.Forms.ToolStripButton();
            this.multi_attribute = new System.Windows.Forms.ToolStripButton();
            this.entity = new System.Windows.Forms.ToolStripButton();
            this.weak_entity = new System.Windows.Forms.ToolStripButton();
            this.relation = new System.Windows.Forms.ToolStripButton();
            this.weak_relation = new System.Windows.Forms.ToolStripButton();
            this.line = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1351, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(117, 26);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(117, 26);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attribute,
            this.multi_attribute,
            this.entity,
            this.weak_entity,
            this.relation,
            this.weak_relation,
            this.line,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1351, 75);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // attribute
            // 
            this.attribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.attribute.Image = global::C_Sharp.Properties.Resources.relationship;
            this.attribute.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.attribute.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.attribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.attribute.Name = "attribute";
            this.attribute.Size = new System.Drawing.Size(64, 72);
            this.attribute.Text = "attribute";
            this.attribute.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.attribute.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.attribute.Click += new System.EventHandler(this.attribute_Click);
            // 
            // multi_attribute
            // 
            this.multi_attribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.multi_attribute.Image = global::C_Sharp.Properties.Resources.weakAttribute;
            this.multi_attribute.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.multi_attribute.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.multi_attribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.multi_attribute.Name = "multi_attribute";
            this.multi_attribute.Size = new System.Drawing.Size(99, 72);
            this.multi_attribute.Text = "multi attribute";
            this.multi_attribute.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.multi_attribute.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.multi_attribute.ToolTipText = "multi attribute";
            this.multi_attribute.Click += new System.EventHandler(this.multi_attribute_Click);
            // 
            // entity
            // 
            this.entity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.entity.Image = global::C_Sharp.Properties.Resources.Entity;
            this.entity.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.entity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.entity.Name = "entity";
            this.entity.Size = new System.Drawing.Size(52, 72);
            this.entity.Text = "entity";
            this.entity.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.entity.Click += new System.EventHandler(this.entity_Click);
            // 
            // weak_entity
            // 
            this.weak_entity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.weak_entity.Image = global::C_Sharp.Properties.Resources.weakEntity;
            this.weak_entity.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.weak_entity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.weak_entity.Name = "weak_entity";
            this.weak_entity.Size = new System.Drawing.Size(85, 72);
            this.weak_entity.Text = "weak entity";
            this.weak_entity.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.weak_entity.Click += new System.EventHandler(this.weak_entity_Click);
            // 
            // relation
            // 
            this.relation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.relation.Image = ((System.Drawing.Image)(resources.GetObject("relation.Image")));
            this.relation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.relation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.relation.Name = "relation";
            this.relation.Size = new System.Drawing.Size(60, 72);
            this.relation.Text = "relation";
            this.relation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.relation.Click += new System.EventHandler(this.relationship_Click);
            // 
            // weak_relation
            // 
            this.weak_relation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.weak_relation.Image = ((System.Drawing.Image)(resources.GetObject("weak_relation.Image")));
            this.weak_relation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.weak_relation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.weak_relation.Name = "weak_relation";
            this.weak_relation.Size = new System.Drawing.Size(99, 72);
            this.weak_relation.Text = "weak relation";
            this.weak_relation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.weak_relation.Click += new System.EventHandler(this.weak_relationship_Click);
            // 
            // line
            // 
            this.line.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.line.Image = ((System.Drawing.Image)(resources.GetObject("line.Image")));
            this.line.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.line.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(69, 72);
            this.line.Text = "line";
            this.line.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 72);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(0, 122);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1371, 878);
            this.mainPanel.TabIndex = 2;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            this.mainPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseClick);
            //this.mainPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseClick);
            //this.mainPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseMove);
            // 
            // diagramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 941);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1369, 988);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1369, 988);
            this.Name = "diagramForm";
            this.Text = "diagramForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton attribute;
        private System.Windows.Forms.ToolStripButton multi_attribute;
        private System.Windows.Forms.ToolStripButton entity;
        private System.Windows.Forms.ToolStripButton weak_entity;
        private System.Windows.Forms.ToolStripButton relation;
        private System.Windows.Forms.ToolStripButton weak_relation;
        private System.Windows.Forms.ToolStripSplitButton line;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}