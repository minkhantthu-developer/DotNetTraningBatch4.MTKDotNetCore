namespace MKTDotNetCore.WinFormsAppSqlInjection
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
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            btnSingIn = new Button();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(153, 127);
            txtEmail.Multiline = true;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(379, 42);
            txtEmail.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(157, 186);
            txtPassword.Multiline = true;
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(379, 35);
            txtPassword.TabIndex = 1;
            // 
            // btnSingIn
            // 
            btnSingIn.Location = new Point(157, 240);
            btnSingIn.Name = "btnSingIn";
            btnSingIn.Size = new Size(375, 39);
            btnSingIn.TabIndex = 2;
            btnSingIn.Text = "Login";
            btnSingIn.UseVisualStyleBackColor = true;
            btnSingIn.Click += btnLogin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(57, 130);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 3;
            label1.Text = "Email";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(57, 189);
            label2.Name = "label2";
            label2.Size = new Size(71, 20);
            label2.TabIndex = 4;
            label2.Text = "Password";
            // 
            // button1
            // 
            button1.Location = new Point(157, 285);
            button1.Name = "button1";
            button1.Size = new Size(375, 39);
            button1.TabIndex = 5;
            button1.Text = "SingIn";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSingIn);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnSingIn;
        private Label label1;
        private Label label2;
        private Button button1;
    }
}
