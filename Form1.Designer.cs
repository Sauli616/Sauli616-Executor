
namespace Sauli616_Executor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            richTextBox1 = new RichTextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label1 = new Label();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(34, 34, 34);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 60, 60);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.WhiteSmoke;
            button1.Location = new Point(362, 181);
            button1.Name = "button1";
            button1.Size = new Size(57, 23);
            button1.TabIndex = 0;
            button1.Text = "Execute";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(34, 34, 34);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 60, 60);
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.WhiteSmoke;
            button2.Location = new Point(299, 181);
            button2.Name = "button2";
            button2.Size = new Size(57, 23);
            button2.TabIndex = 1;
            button2.Text = "Inject";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.FromArgb(34, 34, 34);
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.ForeColor = Color.LightGray;
            richTextBox1.Location = new Point(12, 31);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(407, 144);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(36, 7);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 3;
            label1.Text = "Sauli616";
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(28, 28, 28);
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
            button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 70, 70);
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = Color.WhiteSmoke;
            button3.Location = new Point(406, 4);
            button3.Name = "button3";
            button3.Size = new Size(22, 21);
            button3.TabIndex = 4;
            button3.Text = "X";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(28, 28, 28);
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
            button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 70, 70);
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = Color.WhiteSmoke;
            button4.Location = new Point(378, 4);
            button4.Name = "button4";
            button4.Size = new Size(22, 21);
            button4.TabIndex = 5;
            button4.Text = "-";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackgroundImage = Properties.Resources.Nemhoo;
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button5.Enabled = false;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Location = new Point(2, 3);
            button5.Name = "button5";
            button5.Size = new Size(28, 22);
            button5.TabIndex = 6;
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(24, 24, 24);
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 60, 60);
            button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Segoe UI Emoji", 10F);
            button6.ForeColor = Color.WhiteSmoke;
            button6.Location = new Point(12, 181);
            button6.Name = "button6";
            button6.Size = new Size(23, 23);
            button6.TabIndex = 8;
            button6.Text = "📂";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(24, 24, 24);
            button7.FlatAppearance.BorderSize = 0;
            button7.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 60, 60);
            button7.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            button7.FlatStyle = FlatStyle.Flat;
            button7.Font = new Font("Segoe UI Emoji", 10F);
            button7.ForeColor = Color.WhiteSmoke;
            button7.Location = new Point(42, 181);
            button7.Name = "button7";
            button7.Size = new Size(23, 23);
            button7.TabIndex = 9;
            button7.Text = "🗃️";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(24, 24, 24);
            button8.FlatAppearance.BorderSize = 0;
            button8.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 60, 60);
            button8.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            button8.FlatStyle = FlatStyle.Flat;
            button8.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            button8.ForeColor = Color.WhiteSmoke;
            button8.Location = new Point(71, 181);
            button8.Name = "button8";
            button8.Size = new Size(27, 23);
            button8.TabIndex = 10;
            button8.Text = "IY";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 24, 24);
            ClientSize = new Size(431, 216);
            Controls.Add(button8);
            Controls.Add(button6);
            Controls.Add(button7);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Opacity = 0.94D;
            Text = "Sauli616";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
    }
}
