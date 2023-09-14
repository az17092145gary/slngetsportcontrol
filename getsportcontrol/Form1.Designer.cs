namespace getsportcontrol
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btn_login = new Button();
            btn_sports = new Button();
            btn_Active = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            button2 = new Button();
            timer2 = new System.Windows.Forms.Timer(components);
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // btn_login
            // 
            btn_login.Location = new Point(937, 578);
            btn_login.Margin = new Padding(2);
            btn_login.Name = "btn_login";
            btn_login.Size = new Size(130, 44);
            btn_login.TabIndex = 0;
            btn_login.Text = "Login";
            btn_login.UseVisualStyleBackColor = true;
            btn_login.Click += btn_login_Click;
            // 
            // btn_sports
            // 
            btn_sports.Location = new Point(278, 424);
            btn_sports.Margin = new Padding(2);
            btn_sports.Name = "btn_sports";
            btn_sports.Size = new Size(182, 61);
            btn_sports.TabIndex = 1;
            btn_sports.Text = "get/save_Sports";
            btn_sports.UseVisualStyleBackColor = true;
            btn_sports.Click += btn_sports_Click;
            // 
            // btn_Active
            // 
            btn_Active.Location = new Point(465, 424);
            btn_Active.Margin = new Padding(2);
            btn_Active.Name = "btn_Active";
            btn_Active.Size = new Size(182, 61);
            btn_Active.TabIndex = 2;
            btn_Active.Text = "get/save_Active";
            btn_Active.UseVisualStyleBackColor = true;
            btn_Active.Click += btn_Active_Click;
            // 
            // button1
            // 
            button1.Location = new Point(652, 424);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(182, 61);
            button1.TabIndex = 3;
            button1.Text = "get/save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(277, 18);
            textBox1.Margin = new Padding(2);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(557, 384);
            textBox1.TabIndex = 4;
            // 
            // button2
            // 
            button2.Location = new Point(277, 489);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(182, 61);
            button2.TabIndex = 6;
            button2.Text = "get/10sec save";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // timer2
            // 
            timer2.Enabled = true;
            timer2.Interval = 10000;
            timer2.Tick += timer2_Tick;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1076, 631);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(btn_Active);
            Controls.Add(btn_sports);
            Controls.Add(btn_login);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_login;
        private Button btn_sports;
        private Button btn_Active;
        private Button button1;
        private TextBox textBox1;
        private Button btn_Start;
        private Button button2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer1;
    }
}