
namespace ImageBlurring
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
            this.pictureBox_source = new System.Windows.Forms.PictureBox();
            this.pictureBox_result = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.button_open_image = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_source)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_result)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_source
            // 
            this.pictureBox_source.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_source.Name = "pictureBox_source";
            this.pictureBox_source.Size = new System.Drawing.Size(700, 700);
            this.pictureBox_source.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_source.TabIndex = 0;
            this.pictureBox_source.TabStop = false;
            // 
            // pictureBox_result
            // 
            this.pictureBox_result.Location = new System.Drawing.Point(745, 12);
            this.pictureBox_result.Name = "pictureBox_result";
            this.pictureBox_result.Size = new System.Drawing.Size(700, 700);
            this.pictureBox_result.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_result.TabIndex = 1;
            this.pictureBox_result.TabStop = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "jpg";
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.InitialDirectory = "C:\\Users\\Darishenko\\Darishenko\\UNIVERSITY\\7_Semester\\ЦОС\\ImageBlurring";
            this.openFileDialog.Title = "Choose image";
            // 
            // button_open_image
            // 
            this.button_open_image.Location = new System.Drawing.Point(12, 733);
            this.button_open_image.Name = "button_open_image";
            this.button_open_image.Size = new System.Drawing.Size(174, 34);
            this.button_open_image.TabIndex = 2;
            this.button_open_image.Text = "Open image";
            this.button_open_image.UseVisualStyleBackColor = true;
            this.button_open_image.Click += new System.EventHandler(this.button_open_image_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1553, 879);
            this.Controls.Add(this.button_open_image);
            this.Controls.Add(this.pictureBox_result);
            this.Controls.Add(this.pictureBox_source);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_source)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_result)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_source;
        private System.Windows.Forms.PictureBox pictureBox_result;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button button_open_image;
    }
}

