namespace C_Sharp
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.id_label = new System.Windows.Forms.Label();
            this.pw_label = new System.Windows.Forms.Label();
            this.id_text = new System.Windows.Forms.TextBox();
            this.pw_text = new System.Windows.Forms.TextBox();
            this.login_button = new System.Windows.Forms.Button();
            this.join_button = new System.Windows.Forms.Button();
            this.q1_button = new System.Windows.Forms.Button();
            this.diagram_button = new System.Windows.Forms.Button();
            this.q2_button = new System.Windows.Forms.Button();
            this.q3_button = new System.Windows.Forms.Button();
            this.about_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // id_label
            // 
            this.id_label.AutoSize = true;
            this.id_label.Font = new System.Drawing.Font("나눔바른고딕", 10F);
            this.id_label.Location = new System.Drawing.Point(29, 40);
            this.id_label.Name = "id_label";
            this.id_label.Size = new System.Drawing.Size(22, 16);
            this.id_label.TabIndex = 0;
            this.id_label.Text = "Id ";
            // 
            // pw_label
            // 
            this.pw_label.AutoSize = true;
            this.pw_label.Font = new System.Drawing.Font("나눔바른고딕", 10F);
            this.pw_label.Location = new System.Drawing.Point(29, 75);
            this.pw_label.Name = "pw_label";
            this.pw_label.Size = new System.Drawing.Size(27, 16);
            this.pw_label.TabIndex = 0;
            this.pw_label.Text = "pw";
            // 
            // id_text
            // 
            this.id_text.Location = new System.Drawing.Point(68, 40);
            this.id_text.Name = "id_text";
            this.id_text.Size = new System.Drawing.Size(138, 21);
            this.id_text.TabIndex = 1;
            // 
            // pw_text
            // 
            this.pw_text.Location = new System.Drawing.Point(68, 73);
            this.pw_text.Name = "pw_text";
            this.pw_text.PasswordChar = '*';
            this.pw_text.Size = new System.Drawing.Size(138, 21);
            this.pw_text.TabIndex = 1;
            // 
            // login_button
            // 
            this.login_button.Location = new System.Drawing.Point(32, 116);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(75, 30);
            this.login_button.TabIndex = 2;
            this.login_button.Text = "login";
            this.login_button.UseVisualStyleBackColor = true;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // join_button
            // 
            this.join_button.Location = new System.Drawing.Point(131, 116);
            this.join_button.Name = "join_button";
            this.join_button.Size = new System.Drawing.Size(75, 30);
            this.join_button.TabIndex = 2;
            this.join_button.Text = "join";
            this.join_button.UseVisualStyleBackColor = true;
            this.join_button.Click += new System.EventHandler(this.join_button_Click);
            // 
            // q1_button
            // 
            this.q1_button.Location = new System.Drawing.Point(32, 226);
            this.q1_button.Name = "q1_button";
            this.q1_button.Size = new System.Drawing.Size(55, 30);
            this.q1_button.TabIndex = 2;
            this.q1_button.Text = "Q1";
            this.q1_button.UseVisualStyleBackColor = true;
            // 
            // diagram_button
            // 
            this.diagram_button.Location = new System.Drawing.Point(32, 167);
            this.diagram_button.Name = "diagram_button";
            this.diagram_button.Size = new System.Drawing.Size(174, 30);
            this.diagram_button.TabIndex = 2;
            this.diagram_button.Text = "diagram";
            this.diagram_button.UseVisualStyleBackColor = true;
            this.diagram_button.Click += new System.EventHandler(this.diagram_button_Click);
            // 
            // q2_button
            // 
            this.q2_button.Location = new System.Drawing.Point(93, 226);
            this.q2_button.Name = "q2_button";
            this.q2_button.Size = new System.Drawing.Size(55, 30);
            this.q2_button.TabIndex = 2;
            this.q2_button.Text = "Q2";
            this.q2_button.UseVisualStyleBackColor = true;
            // 
            // q3_button
            // 
            this.q3_button.Location = new System.Drawing.Point(154, 226);
            this.q3_button.Name = "q3_button";
            this.q3_button.Size = new System.Drawing.Size(55, 30);
            this.q3_button.TabIndex = 2;
            this.q3_button.Text = "Q3";
            this.q3_button.UseVisualStyleBackColor = true;
            // 
            // about_button
            // 
            this.about_button.Location = new System.Drawing.Point(32, 278);
            this.about_button.Name = "about_button";
            this.about_button.Size = new System.Drawing.Size(174, 30);
            this.about_button.TabIndex = 2;
            this.about_button.Text = "about";
            this.about_button.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(244, 366);
            this.Controls.Add(this.about_button);
            this.Controls.Add(this.diagram_button);
            this.Controls.Add(this.q3_button);
            this.Controls.Add(this.q2_button);
            this.Controls.Add(this.q1_button);
            this.Controls.Add(this.join_button);
            this.Controls.Add(this.login_button);
            this.Controls.Add(this.pw_text);
            this.Controls.Add(this.id_text);
            this.Controls.Add(this.pw_label);
            this.Controls.Add(this.id_label);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(260, 405);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(260, 405);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Entity";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label id_label;
        private System.Windows.Forms.Label pw_label;
        private System.Windows.Forms.TextBox id_text;
        private System.Windows.Forms.TextBox pw_text;
        private System.Windows.Forms.Button login_button;
        private System.Windows.Forms.Button join_button;
        private System.Windows.Forms.Button q1_button;
        private System.Windows.Forms.Button diagram_button;
        private System.Windows.Forms.Button q2_button;
        private System.Windows.Forms.Button q3_button;
        private System.Windows.Forms.Button about_button;
    }
}

