namespace ListForm
{
    partial class lsForm
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
            this.clt_list = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // clt_list
            // 
            this.clt_list.Location = new System.Drawing.Point(12, 12);
            this.clt_list.Name = "clt_list";
            this.clt_list.Size = new System.Drawing.Size(488, 279);
            this.clt_list.TabIndex = 0;
            this.clt_list.UseCompatibleStateImageBehavior = false;
            this.clt_list.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.clt_list_MouseDoubleClick);
            // 
            // lsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 303);
            this.Controls.Add(this.clt_list);
            this.Name = "lsForm";
            this.Text = "List Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView clt_list;
    }
}

