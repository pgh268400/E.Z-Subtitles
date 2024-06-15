namespace E.Z_Subtitles
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listview_video = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listview_sub = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_match = new System.Windows.Forms.Button();
            this.button_video_up = new System.Windows.Forms.Button();
            this.button_video_down = new System.Windows.Forms.Button();
            this.button_sub_down = new System.Windows.Forms.Button();
            this.button_sub_up = new System.Windows.Forms.Button();
            this.button_sub_clear = new System.Windows.Forms.Button();
            this.button_video_clear = new System.Windows.Forms.Button();
            this.button_all_clear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rvideo = new System.Windows.Forms.RadioButton();
            this.rsmi = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rcopy = new System.Windows.Forms.RadioButton();
            this.rmove = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_how_to = new System.Windows.Forms.Button();
            this.label_video_count = new System.Windows.Forms.Label();
            this.label_sub_count = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listview_video
            // 
            this.listview_video.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader5});
            this.listview_video.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listview_video.HideSelection = false;
            this.listview_video.Location = new System.Drawing.Point(12, 12);
            this.listview_video.Name = "listview_video";
            this.listview_video.Size = new System.Drawing.Size(455, 264);
            this.listview_video.TabIndex = 0;
            this.listview_video.UseCompatibleStateImageBehavior = false;
            this.listview_video.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "파일명";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "확장자";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "경로";
            // 
            // listview_sub
            // 
            this.listview_sub.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader6});
            this.listview_sub.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listview_sub.HideSelection = false;
            this.listview_sub.Location = new System.Drawing.Point(494, 12);
            this.listview_sub.Name = "listview_sub";
            this.listview_sub.Size = new System.Drawing.Size(455, 264);
            this.listview_sub.TabIndex = 1;
            this.listview_sub.UseCompatibleStateImageBehavior = false;
            this.listview_sub.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "파일명";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "확장자";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "경로";
            // 
            // button_match
            // 
            this.button_match.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_match.ForeColor = System.Drawing.Color.Red;
            this.button_match.Location = new System.Drawing.Point(825, 316);
            this.button_match.Name = "button_match";
            this.button_match.Size = new System.Drawing.Size(119, 49);
            this.button_match.TabIndex = 2;
            this.button_match.Text = "Match";
            this.button_match.UseVisualStyleBackColor = true;
            this.button_match.Click += new System.EventHandler(this.button_match_Click);
            // 
            // button_video_up
            // 
            this.button_video_up.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_video_up.Location = new System.Drawing.Point(401, 282);
            this.button_video_up.Name = "button_video_up";
            this.button_video_up.Size = new System.Drawing.Size(30, 30);
            this.button_video_up.TabIndex = 3;
            this.button_video_up.Text = "▲";
            this.button_video_up.UseVisualStyleBackColor = true;
            this.button_video_up.Click += new System.EventHandler(this.button_move_Click);
            // 
            // button_video_down
            // 
            this.button_video_down.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_video_down.Location = new System.Drawing.Point(437, 282);
            this.button_video_down.Name = "button_video_down";
            this.button_video_down.Size = new System.Drawing.Size(30, 30);
            this.button_video_down.TabIndex = 4;
            this.button_video_down.Text = "▼";
            this.button_video_down.UseVisualStyleBackColor = true;
            this.button_video_down.Click += new System.EventHandler(this.button_move_Click);
            // 
            // button_sub_down
            // 
            this.button_sub_down.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_sub_down.Location = new System.Drawing.Point(914, 280);
            this.button_sub_down.Name = "button_sub_down";
            this.button_sub_down.Size = new System.Drawing.Size(30, 30);
            this.button_sub_down.TabIndex = 6;
            this.button_sub_down.Text = "▼";
            this.button_sub_down.UseVisualStyleBackColor = true;
            this.button_sub_down.Click += new System.EventHandler(this.button_move_Click);
            // 
            // button_sub_up
            // 
            this.button_sub_up.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_sub_up.Location = new System.Drawing.Point(878, 280);
            this.button_sub_up.Name = "button_sub_up";
            this.button_sub_up.Size = new System.Drawing.Size(30, 30);
            this.button_sub_up.TabIndex = 5;
            this.button_sub_up.Text = "▲";
            this.button_sub_up.UseVisualStyleBackColor = true;
            this.button_sub_up.Click += new System.EventHandler(this.button_move_Click);
            // 
            // button_sub_clear
            // 
            this.button_sub_clear.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_sub_clear.Location = new System.Drawing.Point(489, 282);
            this.button_sub_clear.Name = "button_sub_clear";
            this.button_sub_clear.Size = new System.Drawing.Size(58, 23);
            this.button_sub_clear.TabIndex = 7;
            this.button_sub_clear.Text = "C";
            this.button_sub_clear.UseVisualStyleBackColor = true;
            this.button_sub_clear.Click += new System.EventHandler(this.clear_list_view_Click);
            // 
            // button_video_clear
            // 
            this.button_video_clear.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_video_clear.Location = new System.Drawing.Point(12, 282);
            this.button_video_clear.Name = "button_video_clear";
            this.button_video_clear.Size = new System.Drawing.Size(58, 23);
            this.button_video_clear.TabIndex = 8;
            this.button_video_clear.Text = "C";
            this.button_video_clear.UseVisualStyleBackColor = true;
            this.button_video_clear.Click += new System.EventHandler(this.clear_list_view_Click);
            // 
            // button_all_clear
            // 
            this.button_all_clear.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_all_clear.Location = new System.Drawing.Point(705, 316);
            this.button_all_clear.Name = "button_all_clear";
            this.button_all_clear.Size = new System.Drawing.Size(114, 49);
            this.button_all_clear.TabIndex = 9;
            this.button_all_clear.Text = "All Clear";
            this.button_all_clear.UseVisualStyleBackColor = true;
            this.button_all_clear.Click += new System.EventHandler(this.clear_list_view_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(672, 382);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 14);
            this.label1.TabIndex = 10;
            this.label1.Text = "Copyright 2024. KRFile(pgh268400@naver.com)";
            // 
            // rvideo
            // 
            this.rvideo.AutoSize = true;
            this.rvideo.Checked = true;
            this.rvideo.Location = new System.Drawing.Point(10, 2);
            this.rvideo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rvideo.Name = "rvideo";
            this.rvideo.Size = new System.Drawing.Size(69, 18);
            this.rvideo.TabIndex = 11;
            this.rvideo.TabStop = true;
            this.rvideo.Text = "영상파일";
            this.rvideo.UseVisualStyleBackColor = true;
            this.rvideo.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // rsmi
            // 
            this.rsmi.AutoSize = true;
            this.rsmi.Location = new System.Drawing.Point(93, 2);
            this.rsmi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rsmi.Name = "rsmi";
            this.rsmi.Size = new System.Drawing.Size(69, 18);
            this.rsmi.TabIndex = 12;
            this.rsmi.Text = "자막파일";
            this.rsmi.UseVisualStyleBackColor = true;
            this.rsmi.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rvideo);
            this.panel1.Controls.Add(this.rsmi);
            this.panel1.Location = new System.Drawing.Point(120, 47);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 21);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rcopy);
            this.panel2.Controls.Add(this.rmove);
            this.panel2.Location = new System.Drawing.Point(120, 19);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(209, 22);
            this.panel2.TabIndex = 14;
            // 
            // rcopy
            // 
            this.rcopy.AutoSize = true;
            this.rcopy.Checked = true;
            this.rcopy.Location = new System.Drawing.Point(10, 2);
            this.rcopy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rcopy.Name = "rcopy";
            this.rcopy.Size = new System.Drawing.Size(47, 18);
            this.rcopy.TabIndex = 11;
            this.rcopy.TabStop = true;
            this.rcopy.Text = "복사";
            this.rcopy.UseVisualStyleBackColor = true;
            this.rcopy.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // rmove
            // 
            this.rmove.AutoSize = true;
            this.rmove.Location = new System.Drawing.Point(93, 2);
            this.rmove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rmove.Name = "rmove";
            this.rmove.Size = new System.Drawing.Size(47, 18);
            this.rmove.TabIndex = 12;
            this.rmove.Text = "이동";
            this.rmove.UseVisualStyleBackColor = true;
            this.rmove.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "자막 이동 방식";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 14);
            this.label3.TabIndex = 16;
            this.label3.Text = "따라갈 파일이름";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(12, 316);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(354, 80);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "설정";
            // 
            // button_how_to
            // 
            this.button_how_to.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_how_to.Location = new System.Drawing.Point(586, 316);
            this.button_how_to.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_how_to.Name = "button_how_to";
            this.button_how_to.Size = new System.Drawing.Size(114, 50);
            this.button_how_to.TabIndex = 18;
            this.button_how_to.Text = "How to?";
            this.button_how_to.UseVisualStyleBackColor = true;
            this.button_how_to.Click += new System.EventHandler(this.button_how_to_Click);
            // 
            // label_video_count
            // 
            this.label_video_count.AutoSize = true;
            this.label_video_count.Font = new System.Drawing.Font("나눔고딕", 10F, System.Drawing.FontStyle.Bold);
            this.label_video_count.Location = new System.Drawing.Point(179, 288);
            this.label_video_count.Name = "label_video_count";
            this.label_video_count.Size = new System.Drawing.Size(66, 16);
            this.label_video_count.TabIndex = 19;
            this.label_video_count.Text = "영상 : 0개";
            // 
            // label_sub_count
            // 
            this.label_sub_count.AutoSize = true;
            this.label_sub_count.Font = new System.Drawing.Font("나눔고딕", 10F, System.Drawing.FontStyle.Bold);
            this.label_sub_count.Location = new System.Drawing.Point(675, 287);
            this.label_sub_count.Name = "label_sub_count";
            this.label_sub_count.Size = new System.Drawing.Size(66, 16);
            this.label_sub_count.TabIndex = 20;
            this.label_sub_count.Text = "자막 : 0개";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 404);
            this.Controls.Add(this.label_sub_count);
            this.Controls.Add(this.label_video_count);
            this.Controls.Add(this.button_how_to);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_all_clear);
            this.Controls.Add(this.button_video_clear);
            this.Controls.Add(this.button_sub_clear);
            this.Controls.Add(this.button_sub_down);
            this.Controls.Add(this.button_sub_up);
            this.Controls.Add(this.button_video_down);
            this.Controls.Add(this.button_video_up);
            this.Controls.Add(this.button_match);
            this.Controls.Add(this.listview_sub);
            this.Controls.Add(this.listview_video);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "E.Z Subtitles v4";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listview_video;
        private System.Windows.Forms.ListView listview_sub;
        private System.Windows.Forms.Button button_match;
        private System.Windows.Forms.Button button_video_up;
        private System.Windows.Forms.Button button_video_down;
        private System.Windows.Forms.Button button_sub_down;
        private System.Windows.Forms.Button button_sub_up;
        private System.Windows.Forms.Button button_sub_clear;
        private System.Windows.Forms.Button button_video_clear;
        private System.Windows.Forms.Button button_all_clear;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.RadioButton rvideo;
        private System.Windows.Forms.RadioButton rsmi;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rcopy;
        private System.Windows.Forms.RadioButton rmove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_how_to;
        private System.Windows.Forms.Label label_video_count;
        private System.Windows.Forms.Label label_sub_count;
    }
}

