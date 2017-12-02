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
            this.er_table = new System.Windows.Forms.ToolStripButton();
            this.line = new System.Windows.Forms.ToolStripSplitButton();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attribute,
            this.multi_attribute,
            this.entity,
            this.weak_entity,
            this.relation,
            this.weak_relation,
            this.er_table,
            this.line});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1184, 71);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // attribute
            // 
            this.attribute.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.attribute.Image = global::C_Sharp.Properties.Resources.relationship;
            this.attribute.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.attribute.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.attribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.attribute.Name = "attribute";
            this.attribute.Size = new System.Drawing.Size(59, 68);
            this.attribute.Text = "attribute";
            this.attribute.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.attribute.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.attribute.Click += new System.EventHandler(this.attribute_Click);
            // 
            // multi_attribute
            // 
            this.multi_attribute.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.multi_attribute.Image = global::C_Sharp.Properties.Resources.weakAttribute;
            this.multi_attribute.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.multi_attribute.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.multi_attribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.multi_attribute.Name = "multi_attribute";
            this.multi_attribute.Size = new System.Drawing.Size(90, 68);
            this.multi_attribute.Text = "multi attribute";
            this.multi_attribute.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.multi_attribute.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.multi_attribute.ToolTipText = "multi attribute";
            this.multi_attribute.Click += new System.EventHandler(this.multi_attribute_Click);
            // 
            // entity
            // 
            this.entity.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.entity.Image = global::C_Sharp.Properties.Resources.Entity;
            this.entity.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.entity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.entity.Name = "entity";
            this.entity.Size = new System.Drawing.Size(52, 68);
            this.entity.Text = "entity";
            this.entity.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.entity.Click += new System.EventHandler(this.entity_Click);
            // 
            // weak_entity
            // 
            this.weak_entity.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.weak_entity.Image = global::C_Sharp.Properties.Resources.weakEntity;
            this.weak_entity.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.weak_entity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.weak_entity.Name = "weak_entity";
            this.weak_entity.Size = new System.Drawing.Size(74, 68);
            this.weak_entity.Text = "weak entity";
            this.weak_entity.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.weak_entity.Click += new System.EventHandler(this.weak_entity_Click);
            // 
            // relation
            // 
            this.relation.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.relation.Image = ((System.Drawing.Image)(resources.GetObject("relation.Image")));
            this.relation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.relation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.relation.Name = "relation";
            this.relation.Size = new System.Drawing.Size(54, 68);
            this.relation.Text = "relation";
            this.relation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.relation.Click += new System.EventHandler(this.relation_Click);
            // 
            // weak_relation
            // 
            this.weak_relation.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.weak_relation.Image = ((System.Drawing.Image)(resources.GetObject("weak_relation.Image")));
            this.weak_relation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.weak_relation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.weak_relation.Name = "weak_relation";
            this.weak_relation.Size = new System.Drawing.Size(86, 68);
            this.weak_relation.Text = "weak relation";
            this.weak_relation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.weak_relation.Click += new System.EventHandler(this.weak_relation_Click);
            // 
            // er_table
            // 
            this.er_table.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.er_table.Image = ((System.Drawing.Image)(resources.GetObject("er_table.Image")));
            this.er_table.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.er_table.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.er_table.Name = "er_table";
            this.er_table.Size = new System.Drawing.Size(56, 68);
            this.er_table.Text = "ER table";
            this.er_table.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.er_table.ToolTipText = "er_table";
            this.er_table.Click += new System.EventHandler(this.er_table_Click);
            // 
            // line
            // 
            this.line.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.line.Image = ((System.Drawing.Image)(resources.GetObject("line.Image")));
            this.line.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.line.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(66, 68);
            this.line.Text = "line";
            this.line.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(0, 98);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1200, 702);
            this.mainPanel.TabIndex = 2;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            this.mainPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseClick);
            // 
            // diagramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1200, 800);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1200, 800);
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
        private System.Windows.Forms.ToolStripButton er_table;
        private System.Windows.Forms.ToolStripSplitButton line;
        private System.Windows.Forms.Panel mainPanel;
    }
}