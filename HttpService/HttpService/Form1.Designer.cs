namespace HttpService
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
            this.textURL = new System.Windows.Forms.TextBox();
            this.buttonGet = new System.Windows.Forms.Button();
            this.textBoxWeb = new System.Windows.Forms.TextBox();
            this.buttonPost = new System.Windows.Forms.Button();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxPost = new System.Windows.Forms.TextBox();
            this.buttonImageDown = new System.Windows.Forms.Button();
            this.imagePB = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imagePB)).BeginInit();
            this.SuspendLayout();
            // 
            // textURL
            // 
            this.textURL.Location = new System.Drawing.Point(29, 26);
            this.textURL.Name = "textURL";
            this.textURL.Size = new System.Drawing.Size(640, 25);
            this.textURL.TabIndex = 0;
            // 
            // buttonGet
            // 
            this.buttonGet.Location = new System.Drawing.Point(686, 26);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(102, 25);
            this.buttonGet.TabIndex = 1;
            this.buttonGet.Text = "Get";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // textBoxWeb
            // 
            this.textBoxWeb.Location = new System.Drawing.Point(29, 66);
            this.textBoxWeb.Multiline = true;
            this.textBoxWeb.Name = "textBoxWeb";
            this.textBoxWeb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxWeb.Size = new System.Drawing.Size(759, 219);
            this.textBoxWeb.TabIndex = 2;
            // 
            // buttonPost
            // 
            this.buttonPost.Location = new System.Drawing.Point(29, 291);
            this.buttonPost.Name = "buttonPost";
            this.buttonPost.Size = new System.Drawing.Size(82, 33);
            this.buttonPost.TabIndex = 3;
            this.buttonPost.Text = "Post";
            this.buttonPost.UseVisualStyleBackColor = true;
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(165, 299);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(158, 25);
            this.textBoxID.TabIndex = 4;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(416, 299);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(174, 25);
            this.textBoxName.TabIndex = 5;
            // 
            // textBoxPost
            // 
            this.textBoxPost.Location = new System.Drawing.Point(29, 330);
            this.textBoxPost.Multiline = true;
            this.textBoxPost.Name = "textBoxPost";
            this.textBoxPost.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxPost.Size = new System.Drawing.Size(561, 99);
            this.textBoxPost.TabIndex = 6;
            // 
            // buttonImageDown
            // 
            this.buttonImageDown.Location = new System.Drawing.Point(596, 297);
            this.buttonImageDown.Name = "buttonImageDown";
            this.buttonImageDown.Size = new System.Drawing.Size(36, 138);
            this.buttonImageDown.TabIndex = 7;
            this.buttonImageDown.Text = "그림받기";
            this.buttonImageDown.UseVisualStyleBackColor = true;
            // 
            // imagePB
            // 
            this.imagePB.Location = new System.Drawing.Point(638, 300);
            this.imagePB.Name = "imagePB";
            this.imagePB.Size = new System.Drawing.Size(150, 138);
            this.imagePB.TabIndex = 8;
            this.imagePB.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 302);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(367, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Name";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imagePB);
            this.Controls.Add(this.buttonImageDown);
            this.Controls.Add(this.textBoxPost);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.buttonPost);
            this.Controls.Add(this.textBoxWeb);
            this.Controls.Add(this.buttonGet);
            this.Controls.Add(this.textURL);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imagePB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textURL;
        private System.Windows.Forms.Button buttonGet;
        private System.Windows.Forms.TextBox textBoxWeb;
        private System.Windows.Forms.Button buttonPost;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxPost;
        private System.Windows.Forms.Button buttonImageDown;
        private System.Windows.Forms.PictureBox imagePB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

