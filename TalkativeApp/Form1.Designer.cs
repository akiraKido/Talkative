namespace TalkativeApp
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent ()
        {
            this.input_TextBox = new System.Windows.Forms.TextBox();
            this.compile_Button = new System.Windows.Forms.Button();
            this.output_TextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // input_TextBox
            // 
            this.input_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input_TextBox.Location = new System.Drawing.Point(13, 13);
            this.input_TextBox.Multiline = true;
            this.input_TextBox.Name = "input_TextBox";
            this.input_TextBox.Size = new System.Drawing.Size(332, 172);
            this.input_TextBox.TabIndex = 0;
            // 
            // compile_Button
            // 
            this.compile_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.compile_Button.Location = new System.Drawing.Point(270, 191);
            this.compile_Button.Name = "compile_Button";
            this.compile_Button.Size = new System.Drawing.Size(75, 23);
            this.compile_Button.TabIndex = 1;
            this.compile_Button.Text = "Compile";
            this.compile_Button.UseVisualStyleBackColor = true;
            this.compile_Button.Click += new System.EventHandler(this.compile_Button_Click);
            // 
            // output_TextBox
            // 
            this.output_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.output_TextBox.Location = new System.Drawing.Point(13, 220);
            this.output_TextBox.Multiline = true;
            this.output_TextBox.Name = "output_TextBox";
            this.output_TextBox.Size = new System.Drawing.Size(332, 123);
            this.output_TextBox.TabIndex = 2;
            this.output_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.output_TextBox_KeyPress);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 355);
            this.Controls.Add(this.output_TextBox);
            this.Controls.Add(this.compile_Button);
            this.Controls.Add(this.input_TextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox input_TextBox;
        private System.Windows.Forms.Button compile_Button;
        private System.Windows.Forms.TextBox output_TextBox;
    }
}

