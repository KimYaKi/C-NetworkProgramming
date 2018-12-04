namespace LoginForm
{
    partial class lgForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pwd_text = new System.Windows.Forms.TextBox();
            this.id_text = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rage Italic", 13F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(94, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rage Italic", 13F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(38, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "Password";
            // 
            // pwd_text
            // 
            this.pwd_text.Font = new System.Drawing.Font("굴림", 13F);
            this.pwd_text.Location = new System.Drawing.Point(155, 87);
            this.pwd_text.Name = "pwd_text";
            this.pwd_text.PasswordChar = '●';
            this.pwd_text.Size = new System.Drawing.Size(251, 32);
            this.pwd_text.TabIndex = 1;
            this.pwd_text.Text = "1234";
            // 
            // id_text
            // 
            this.id_text.Font = new System.Drawing.Font("굴림", 13F);
            this.id_text.Location = new System.Drawing.Point(155, 26);
            this.id_text.Name = "id_text";
            this.id_text.Size = new System.Drawing.Size(251, 32);
            this.id_text.TabIndex = 1;
            this.id_text.Text = "test";
            // 
            // btn_login
            // 
            this.btn_login.Font = new System.Drawing.Font("Rage Italic", 13F, System.Drawing.FontStyle.Bold);
            this.btn_login.Location = new System.Drawing.Point(43, 150);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(363, 36);
            this.btn_login.TabIndex = 2;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // lgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 214);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.id_text);
            this.Controls.Add(this.pwd_text);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "lgForm";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pwd_text;
        private System.Windows.Forms.TextBox id_text;
        private System.Windows.Forms.Button btn_login;
    }
}

