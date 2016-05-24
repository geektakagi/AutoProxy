namespace AutoProxy
{
    partial class AutoProxySetting
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnProxyEnable = new System.Windows.Forms.Button();
            this.btnProxyDisable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(12, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(154, 24);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Enable AutoProy";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnProxyEnable
            // 
            this.btnProxyEnable.Location = new System.Drawing.Point(238, 270);
            this.btnProxyEnable.Name = "btnProxyEnable";
            this.btnProxyEnable.Size = new System.Drawing.Size(75, 23);
            this.btnProxyEnable.TabIndex = 1;
            this.btnProxyEnable.Text = "Enable";
            this.btnProxyEnable.UseVisualStyleBackColor = true;
            this.btnProxyEnable.Click += new System.EventHandler(this.ProxyEnable);
            // 
            // btnProxyDisable
            // 
            this.btnProxyDisable.Location = new System.Drawing.Point(471, 270);
            this.btnProxyDisable.Name = "btnProxyDisable";
            this.btnProxyDisable.Size = new System.Drawing.Size(75, 23);
            this.btnProxyDisable.TabIndex = 2;
            this.btnProxyDisable.Text = "Disable";
            this.btnProxyDisable.UseVisualStyleBackColor = true;
            this.btnProxyDisable.Click += new System.EventHandler(this.btnProxyDisable_Click);
            // 
            // AutoProxySetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 544);
            this.Controls.Add(this.btnProxyDisable);
            this.Controls.Add(this.btnProxyEnable);
            this.Controls.Add(this.checkBox1);
            this.MaximizeBox = false;
            this.Name = "AutoProxySetting";
            this.Text = "AutoProxySetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnProxyEnable;
        private System.Windows.Forms.Button btnProxyDisable;
    }
}

