namespace ChatClient
{
    partial class chForm
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
            this.btn_send = new System.Windows.Forms.Button();
            this.user_text = new System.Windows.Forms.TextBox();
            this.svr_text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(399, 408);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(90, 30);
            this.btn_send.TabIndex = 0;
            this.btn_send.Text = "전송";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // user_text
            // 
            this.user_text.Font = new System.Drawing.Font("굴림", 11F);
            this.user_text.Location = new System.Drawing.Point(12, 408);
            this.user_text.Name = "user_text";
            this.user_text.Size = new System.Drawing.Size(381, 29);
            this.user_text.TabIndex = 1;
            // 
            // svr_text
            // 
            this.svr_text.Location = new System.Drawing.Point(12, 12);
            this.svr_text.Multiline = true;
            this.svr_text.Name = "svr_text";
            this.svr_text.Size = new System.Drawing.Size(477, 380);
            this.svr_text.TabIndex = 2;
            // 
            // chForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 456);
            this.Controls.Add(this.svr_text);
            this.Controls.Add(this.user_text);
            this.Controls.Add(this.btn_send);
            this.Name = "chForm";
            this.Text = "Chat Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox user_text;
        private System.Windows.Forms.TextBox svr_text;
    }
}

