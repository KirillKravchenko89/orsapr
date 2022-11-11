
namespace ORSAPR.View
{
    partial class FormParameters
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParameters));
            this.PanelWindow = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabelInnerWidth = new System.Windows.Forms.Label();
            this.TextBoxInnerWidth = new System.Windows.Forms.TextBox();
            this.LabelParameter = new System.Windows.Forms.Label();
            this.TextBoxLength = new System.Windows.Forms.TextBox();
            this.ButtonBuild = new System.Windows.Forms.Button();
            this.TextBoxHeight = new System.Windows.Forms.TextBox();
            this.TextBoxInnerLength = new System.Windows.Forms.TextBox();
            this.TextBoxWidth = new System.Windows.Forms.TextBox();
            this.LabelBladeLength = new System.Windows.Forms.Label();
            this.TextBoxBladeLength = new System.Windows.Forms.TextBox();
            this.LabelInnerLength = new System.Windows.Forms.Label();
            this.LabelHeight = new System.Windows.Forms.Label();
            this.LabelLength = new System.Windows.Forms.Label();
            this.LabelWidth = new System.Windows.Forms.Label();
            this.toolTipInformation = new System.Windows.Forms.ToolTip(this.components);
            this.PanelWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelWindow
            // 
            this.PanelWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelWindow.Controls.Add(this.panel1);
            this.PanelWindow.Controls.Add(this.LabelInnerWidth);
            this.PanelWindow.Controls.Add(this.TextBoxInnerWidth);
            this.PanelWindow.Controls.Add(this.LabelParameter);
            this.PanelWindow.Controls.Add(this.TextBoxLength);
            this.PanelWindow.Controls.Add(this.ButtonBuild);
            this.PanelWindow.Controls.Add(this.TextBoxHeight);
            this.PanelWindow.Controls.Add(this.TextBoxInnerLength);
            this.PanelWindow.Controls.Add(this.TextBoxWidth);
            this.PanelWindow.Controls.Add(this.LabelBladeLength);
            this.PanelWindow.Controls.Add(this.TextBoxBladeLength);
            this.PanelWindow.Controls.Add(this.LabelInnerLength);
            this.PanelWindow.Controls.Add(this.LabelHeight);
            this.PanelWindow.Controls.Add(this.LabelLength);
            this.PanelWindow.Controls.Add(this.LabelWidth);
            this.PanelWindow.Location = new System.Drawing.Point(12, 12);
            this.PanelWindow.MaximumSize = new System.Drawing.Size(920, 1160);
            this.PanelWindow.MinimumSize = new System.Drawing.Size(450, 520);
            this.PanelWindow.Name = "PanelWindow";
            this.PanelWindow.Size = new System.Drawing.Size(450, 629);
            this.PanelWindow.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 204);
            this.panel1.TabIndex = 20;
            // 
            // LabelInnerWidth
            // 
            this.LabelInnerWidth.AutoSize = true;
            this.LabelInnerWidth.Location = new System.Drawing.Point(24, 495);
            this.LabelInnerWidth.Name = "LabelInnerWidth";
            this.LabelInnerWidth.Size = new System.Drawing.Size(155, 17);
            this.LabelInnerWidth.TabIndex = 19;
            this.LabelInnerWidth.Text = "w1 - Chisel cutout width";
            // 
            // TextBoxInnerWidth
            // 
            this.TextBoxInnerWidth.Location = new System.Drawing.Point(233, 492);
            this.TextBoxInnerWidth.Name = "TextBoxInnerWidth";
            this.TextBoxInnerWidth.Size = new System.Drawing.Size(203, 22);
            this.TextBoxInnerWidth.TabIndex = 5;
            this.TextBoxInnerWidth.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxInnerWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxInnerWidth_KeyPress);
            this.TextBoxInnerWidth.Leave += new System.EventHandler(this.TextBoxInnerWidth_Leave);
            // 
            // LabelParameter
            // 
            this.LabelParameter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabelParameter.AutoSize = true;
            this.LabelParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LabelParameter.Location = new System.Drawing.Point(164, 238);
            this.LabelParameter.Name = "LabelParameter";
            this.LabelParameter.Size = new System.Drawing.Size(124, 25);
            this.LabelParameter.TabIndex = 12;
            this.LabelParameter.Text = "Tool Options";
            // 
            // TextBoxLength
            // 
            this.TextBoxLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxLength.Location = new System.Drawing.Point(233, 332);
            this.TextBoxLength.Name = "TextBoxLength";
            this.TextBoxLength.Size = new System.Drawing.Size(203, 22);
            this.TextBoxLength.TabIndex = 1;
            this.TextBoxLength.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxLenght_KeyPress);
            this.TextBoxLength.Leave += new System.EventHandler(this.TextBoxLenght_Leave);
            // 
            // ButtonBuild
            // 
            this.ButtonBuild.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonBuild.Enabled = false;
            this.ButtonBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ButtonBuild.Location = new System.Drawing.Point(169, 550);
            this.ButtonBuild.Name = "ButtonBuild";
            this.ButtonBuild.Size = new System.Drawing.Size(115, 60);
            this.ButtonBuild.TabIndex = 6;
            this.ButtonBuild.Text = "build";
            this.ButtonBuild.UseVisualStyleBackColor = true;
            this.ButtonBuild.Click += new System.EventHandler(this.Buttonbuild_Click);
            // 
            // TextBoxHeight
            // 
            this.TextBoxHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxHeight.Location = new System.Drawing.Point(233, 372);
            this.TextBoxHeight.Name = "TextBoxHeight";
            this.TextBoxHeight.Size = new System.Drawing.Size(203, 22);
            this.TextBoxHeight.TabIndex = 2;
            this.TextBoxHeight.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxHeight_KeyPress);
            this.TextBoxHeight.Leave += new System.EventHandler(this.TextBoxHeight_Leave);
            // 
            // TextBoxInnerLength
            // 
            this.TextBoxInnerLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxInnerLength.Location = new System.Drawing.Point(233, 452);
            this.TextBoxInnerLength.Name = "TextBoxInnerLength";
            this.TextBoxInnerLength.Size = new System.Drawing.Size(203, 22);
            this.TextBoxInnerLength.TabIndex = 4;
            this.TextBoxInnerLength.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxInnerLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxInnerLength_KeyPress);
            this.TextBoxInnerLength.Leave += new System.EventHandler(this.TextBoxInnerLength_Leave);
            // 
            // TextBoxWidth
            // 
            this.TextBoxWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxWidth.Location = new System.Drawing.Point(233, 292);
            this.TextBoxWidth.Name = "TextBoxWidth";
            this.TextBoxWidth.Size = new System.Drawing.Size(203, 22);
            this.TextBoxWidth.TabIndex = 0;
            this.TextBoxWidth.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxWidth_KeyPress);
            this.TextBoxWidth.Leave += new System.EventHandler(this.TextBoxWidth_Leave);
            // 
            // LabelBladeLength
            // 
            this.LabelBladeLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelBladeLength.AutoSize = true;
            this.LabelBladeLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelBladeLength.Location = new System.Drawing.Point(24, 413);
            this.LabelBladeLength.Name = "LabelBladeLength";
            this.LabelBladeLength.Size = new System.Drawing.Size(154, 18);
            this.LabelBladeLength.TabIndex = 5;
            this.LabelBladeLength.Text = "l1 – Chisel blade length";
            // 
            // TextBoxBladeLength
            // 
            this.TextBoxBladeLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxBladeLength.Location = new System.Drawing.Point(233, 412);
            this.TextBoxBladeLength.Name = "TextBoxBladeLength";
            this.TextBoxBladeLength.Size = new System.Drawing.Size(203, 22);
            this.TextBoxBladeLength.TabIndex = 3;
            this.TextBoxBladeLength.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxBladeLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxBladeLength_KeyPress);
            this.TextBoxBladeLength.Leave += new System.EventHandler(this.TextBoxBladeLength_Leave);
            // 
            // LabelInnerLength
            // 
            this.LabelInnerLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelInnerLength.AutoSize = true;
            this.LabelInnerLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelInnerLength.Location = new System.Drawing.Point(24, 453);
            this.LabelInnerLength.Name = "LabelInnerLength";
            this.LabelInnerLength.Size = new System.Drawing.Size(160, 18);
            this.LabelInnerLength.TabIndex = 4;
            this.LabelInnerLength.Text = "l2 – Chisel cutout length";
            // 
            // LabelHeight
            // 
            this.LabelHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelHeight.AutoSize = true;
            this.LabelHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelHeight.Location = new System.Drawing.Point(24, 373);
            this.LabelHeight.Name = "LabelHeight";
            this.LabelHeight.Size = new System.Drawing.Size(116, 18);
            this.LabelHeight.TabIndex = 3;
            this.LabelHeight.Text = "H - Chisel height";
            // 
            // LabelLength
            // 
            this.LabelLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelLength.AutoSize = true;
            this.LabelLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelLength.Location = new System.Drawing.Point(24, 333);
            this.LabelLength.Name = "LabelLength";
            this.LabelLength.Size = new System.Drawing.Size(113, 18);
            this.LabelLength.TabIndex = 2;
            this.LabelLength.Text = "L - Chisel length";
            // 
            // LabelWidth
            // 
            this.LabelWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelWidth.AutoSize = true;
            this.LabelWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelWidth.Location = new System.Drawing.Point(24, 293);
            this.LabelWidth.Name = "LabelWidth";
            this.LabelWidth.Size = new System.Drawing.Size(115, 18);
            this.LabelWidth.TabIndex = 1;
            this.LabelWidth.Text = "W - Chisel width";
            // 
            // FormParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 653);
            this.Controls.Add(this.PanelWindow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(491, 700);
            this.MinimumSize = new System.Drawing.Size(491, 700);
            this.Name = "FormParameters";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chisel Parameters";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormParameters_FormClosing);
            this.PanelWindow.ResumeLayout(false);
            this.PanelWindow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PanelWindow;
        private System.Windows.Forms.ToolTip toolTipInformation;
        private System.Windows.Forms.Label LabelParameter;
        private System.Windows.Forms.TextBox TextBoxLength;
        private System.Windows.Forms.Button ButtonBuild;
        private System.Windows.Forms.TextBox TextBoxHeight;
        private System.Windows.Forms.TextBox TextBoxInnerLength;
        private System.Windows.Forms.TextBox TextBoxWidth;
        private System.Windows.Forms.Label LabelBladeLength;
        private System.Windows.Forms.TextBox TextBoxBladeLength;
        private System.Windows.Forms.Label LabelInnerLength;
        private System.Windows.Forms.Label LabelHeight;
        private System.Windows.Forms.Label LabelLength;
        private System.Windows.Forms.Label LabelWidth;
        private System.Windows.Forms.Label LabelInnerWidth;
        private System.Windows.Forms.TextBox TextBoxInnerWidth;
        private System.Windows.Forms.Panel panel1;
    }
}

