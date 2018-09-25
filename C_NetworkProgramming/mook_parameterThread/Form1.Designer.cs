namespace mook_parameterThread
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
            this.btnSum = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSum
            // 
            this.btnSum.Font = new System.Drawing.Font("굴림", 13F);
            this.btnSum.Location = new System.Drawing.Point(224, 12);
            this.btnSum.Name = "btnSum";
            this.btnSum.Size = new System.Drawing.Size(100, 29);
            this.btnSum.TabIndex = 7;
            this.btnSum.Text = "SUM";
            this.btnSum.UseVisualStyleBackColor = true;
            this.btnSum.Click += new System.EventHandler(this.btnSum_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("굴림", 13F);
            this.lblResult.Location = new System.Drawing.Point(13, 89);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(74, 22);
            this.lblResult.TabIndex = 6;
            this.lblResult.Text = "결과 : ";
            // 
            // txtNum
            // 
            this.txtNum.Font = new System.Drawing.Font("굴림", 13F);
            this.txtNum.Location = new System.Drawing.Point(91, 9);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(114, 32);
            this.txtNum.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 13F);
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "SUM";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSum);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSum;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Label label1;
    }
}

