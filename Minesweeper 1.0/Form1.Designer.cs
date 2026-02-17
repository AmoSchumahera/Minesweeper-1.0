namespace Minesweeper_1._0
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.numMines = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.lblTimer = new System.Windows.Forms.Label();
            this.panelBoard = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numMines)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new System.Drawing.Point(237, 459);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(148, 22);
            this.txtPlayerName.TabIndex = 2;
            this.txtPlayerName.TextChanged += new System.EventHandler(this.txtPlayerName_Text);
            // 
            // numMines
            // 
            this.numMines.Location = new System.Drawing.Point(12, 552);
            this.numMines.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numMines.MaximumSize = new System.Drawing.Size(200, 0);
            this.numMines.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMines.MinimumSize = new System.Drawing.Size(1, 0);
            this.numMines.Name = "numMines";
            this.numMines.Size = new System.Drawing.Size(120, 22);
            this.numMines.TabIndex = 3;
            this.numMines.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numMines.ValueChanged += new System.EventHandler(this.numMines_Value);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(163, 519);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(222, 55);
            this.button1.TabIndex = 5;
            this.button1.Text = "-Start a new game-";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 1000;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Location = new System.Drawing.Point(474, 124);
            this.lblTimer.MaximumSize = new System.Drawing.Size(90, 90);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(14, 16);
            this.lblTimer.TabIndex = 9;
            this.lblTimer.Text = "0";
            this.lblTimer.Click += new System.EventHandler(this.lblTimer_Click);
            // 
            // panelBoard
            // 
            this.panelBoard.Location = new System.Drawing.Point(548, 84);
            this.panelBoard.Name = "panelBoard";
            this.panelBoard.Size = new System.Drawing.Size(450, 450);
            this.panelBoard.TabIndex = 10;
            this.panelBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBoard_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 684);
            this.Controls.Add(this.panelBoard);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numMines);
            this.Controls.Add(this.txtPlayerName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.NumericUpDown numMines;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Panel panelBoard;
    }
}

