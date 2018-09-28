namespace PrimeNumber
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblNum = new System.Windows.Forms.Label();
            this.lblPrime = new System.Windows.Forms.Label();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.actionBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(53, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(383, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "1~10 사이의 숫자를 입력하세요.";
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Font = new System.Drawing.Font("굴림", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblNum.Location = new System.Drawing.Point(53, 143);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(241, 24);
            this.lblNum.TabIndex = 1;
            this.lblNum.Text = "Action을 눌러주세요";
            // 
            // lblPrime
            // 
            this.lblPrime.AutoSize = true;
            this.lblPrime.Font = new System.Drawing.Font("굴림", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPrime.Location = new System.Drawing.Point(53, 191);
            this.lblPrime.Name = "lblPrime";
            this.lblPrime.Size = new System.Drawing.Size(241, 24);
            this.lblPrime.TabIndex = 2;
            this.lblPrime.Text = "Action을 눌러주세요";
            // 
            // txtNum
            // 
            this.txtNum.Font = new System.Drawing.Font("굴림", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtNum.Location = new System.Drawing.Point(48, 86);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(113, 34);
            this.txtNum.TabIndex = 3;
            // 
            // actionBtn
            // 
            this.actionBtn.Font = new System.Drawing.Font("굴림", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.actionBtn.Location = new System.Drawing.Point(213, 86);
            this.actionBtn.Name = "actionBtn";
            this.actionBtn.Size = new System.Drawing.Size(108, 34);
            this.actionBtn.TabIndex = 4;
            this.actionBtn.Text = "Action";
            this.actionBtn.UseVisualStyleBackColor = true;
            this.actionBtn.Click += new System.EventHandler(this.actionBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 277);
            this.Controls.Add(this.actionBtn);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.lblPrime);
            this.Controls.Add(this.lblNum);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label lblPrime;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Button actionBtn;
    }
}

