
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
            this.LabelWidth = new System.Windows.Forms.Label();
            this.LabelLenght = new System.Windows.Forms.Label();
            this.LabelHeight = new System.Windows.Forms.Label();
            this.LabelInnerLenght = new System.Windows.Forms.Label();
            this.LabelBladeLenght = new System.Windows.Forms.Label();
            this.TextBoxHeight = new System.Windows.Forms.TextBox();
            this.TextBoxLenght = new System.Windows.Forms.TextBox();
            this.TextBoxWidth = new System.Windows.Forms.TextBox();
            this.TextBoxBladeLenght = new System.Windows.Forms.TextBox();
            this.TextBoxInnerLenght = new System.Windows.Forms.TextBox();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.PanelWindow = new System.Windows.Forms.Panel();
            this.LabelParameter = new System.Windows.Forms.Label();
            this.toolTipInformation = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.PanelWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelWidth
            // 
            this.LabelWidth.AutoSize = true;
            this.LabelWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelWidth.Location = new System.Drawing.Point(25, 201);
            this.LabelWidth.Name = "LabelWidth";
            this.LabelWidth.Size = new System.Drawing.Size(142, 18);
            this.LabelWidth.TabIndex = 1;
            this.LabelWidth.Text = "W - Ширина зубила";
            // 
            // LabelLenght
            // 
            this.LabelLenght.AutoSize = true;
            this.LabelLenght.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelLenght.Location = new System.Drawing.Point(25, 151);
            this.LabelLenght.Name = "LabelLenght";
            this.LabelLenght.Size = new System.Drawing.Size(127, 18);
            this.LabelLenght.TabIndex = 2;
            this.LabelLenght.Text = "L - Длина зубила";
            // 
            // LabelHeight
            // 
            this.LabelHeight.AutoSize = true;
            this.LabelHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelHeight.Location = new System.Drawing.Point(27, 101);
            this.LabelHeight.Name = "LabelHeight";
            this.LabelHeight.Size = new System.Drawing.Size(138, 18);
            this.LabelHeight.TabIndex = 3;
            this.LabelHeight.Text = "H - Высота зубила";
            // 
            // LabelInnerLenght
            // 
            this.LabelInnerLenght.AutoSize = true;
            this.LabelInnerLenght.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelInnerLenght.Location = new System.Drawing.Point(25, 251);
            this.LabelInnerLenght.Name = "LabelInnerLenght";
            this.LabelInnerLenght.Size = new System.Drawing.Size(184, 18);
            this.LabelInnerLenght.TabIndex = 4;
            this.LabelInnerLenght.Text = "l1 – Длина выреза зубила";
            // 
            // LabelBladeLenght
            // 
            this.LabelBladeLenght.AutoSize = true;
            this.LabelBladeLenght.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelBladeLenght.Location = new System.Drawing.Point(25, 301);
            this.LabelBladeLenght.Name = "LabelBladeLenght";
            this.LabelBladeLenght.Size = new System.Drawing.Size(182, 18);
            this.LabelBladeLenght.TabIndex = 5;
            this.LabelBladeLenght.Text = "l2 – Длина лезвия зубила";
            // 
            // TextBoxHeight
            // 
            this.TextBoxHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxHeight.Location = new System.Drawing.Point(236, 100);
            this.TextBoxHeight.Name = "TextBoxHeight";
            this.TextBoxHeight.Size = new System.Drawing.Size(213, 22);
            this.TextBoxHeight.TabIndex = 6;
            // 
            // TextBoxLenght
            // 
            this.TextBoxLenght.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxLenght.Location = new System.Drawing.Point(236, 150);
            this.TextBoxLenght.Name = "TextBoxLenght";
            this.TextBoxLenght.Size = new System.Drawing.Size(213, 22);
            this.TextBoxLenght.TabIndex = 7;
            // 
            // TextBoxWidth
            // 
            this.TextBoxWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxWidth.Location = new System.Drawing.Point(236, 200);
            this.TextBoxWidth.Name = "TextBoxWidth";
            this.TextBoxWidth.Size = new System.Drawing.Size(213, 22);
            this.TextBoxWidth.TabIndex = 8;
            this.TextBoxWidth.TextChanged += new System.EventHandler(this.TextBoxWidth_TextChanged);
            // 
            // TextBoxBladeLenght
            // 
            this.TextBoxBladeLenght.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxBladeLenght.Location = new System.Drawing.Point(236, 300);
            this.TextBoxBladeLenght.Name = "TextBoxBladeLenght";
            this.TextBoxBladeLenght.Size = new System.Drawing.Size(213, 22);
            this.TextBoxBladeLenght.TabIndex = 9;
            // 
            // TextBoxInnerLenght
            // 
            this.TextBoxInnerLenght.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxInnerLenght.Location = new System.Drawing.Point(236, 250);
            this.TextBoxInnerLenght.Name = "TextBoxInnerLenght";
            this.TextBoxInnerLenght.Size = new System.Drawing.Size(213, 22);
            this.TextBoxInnerLenght.TabIndex = 10;
            // 
            // ButtonSave
            // 
            this.ButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ButtonSave.Location = new System.Drawing.Point(180, 390);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(100, 50);
            this.ButtonSave.TabIndex = 11;
            this.ButtonSave.Text = "Сохранить";
            this.ButtonSave.UseVisualStyleBackColor = true;
            // 
            // PanelWindow
            // 
            this.PanelWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelWindow.Controls.Add(this.toolStrip1);
            this.PanelWindow.Controls.Add(this.LabelParameter);
            this.PanelWindow.Controls.Add(this.TextBoxLenght);
            this.PanelWindow.Controls.Add(this.ButtonSave);
            this.PanelWindow.Controls.Add(this.TextBoxHeight);
            this.PanelWindow.Controls.Add(this.TextBoxInnerLenght);
            this.PanelWindow.Controls.Add(this.TextBoxWidth);
            this.PanelWindow.Controls.Add(this.LabelBladeLenght);
            this.PanelWindow.Controls.Add(this.TextBoxBladeLenght);
            this.PanelWindow.Controls.Add(this.LabelInnerLenght);
            this.PanelWindow.Controls.Add(this.LabelHeight);
            this.PanelWindow.Controls.Add(this.LabelLenght);
            this.PanelWindow.Controls.Add(this.LabelWidth);
            this.PanelWindow.Location = new System.Drawing.Point(12, 12);
            this.PanelWindow.MaximumSize = new System.Drawing.Size(920, 1160);
            this.PanelWindow.MinimumSize = new System.Drawing.Size(450, 520);
            this.PanelWindow.Name = "PanelWindow";
            this.PanelWindow.Size = new System.Drawing.Size(460, 580);
            this.PanelWindow.TabIndex = 12;
            // 
            // LabelParameter
            // 
            this.LabelParameter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LabelParameter.AutoSize = true;
            this.LabelParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LabelParameter.Location = new System.Drawing.Point(104, 36);
            this.LabelParameter.Name = "LabelParameter";
            this.LabelParameter.Size = new System.Drawing.Size(252, 25);
            this.LabelParameter.TabIndex = 12;
            this.LabelParameter.Text = "Параметры инструмента";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(460, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // FormParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 604);
            this.Controls.Add(this.PanelWindow);
            this.MaximumSize = new System.Drawing.Size(1002, 1182);
            this.MinimumSize = new System.Drawing.Size(491, 591);
            this.Name = "FormParameters";
            this.Text = "Настройки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormParameters_FormClosing);
            this.Load += new System.EventHandler(this.FormParameters_Load);
            this.PanelWindow.ResumeLayout(false);
            this.PanelWindow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LabelWidth;
        private System.Windows.Forms.Label LabelLenght;
        private System.Windows.Forms.Label LabelHeight;
        private System.Windows.Forms.Label LabelInnerLenght;
        private System.Windows.Forms.Label LabelBladeLenght;
        private System.Windows.Forms.TextBox TextBoxHeight;
        private System.Windows.Forms.TextBox TextBoxLenght;
        private System.Windows.Forms.TextBox TextBoxWidth;
        private System.Windows.Forms.TextBox TextBoxBladeLenght;
        private System.Windows.Forms.TextBox TextBoxInnerLenght;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Panel PanelWindow;
        private System.Windows.Forms.Label LabelParameter;
        private System.Windows.Forms.ToolTip toolTipInformation;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}

