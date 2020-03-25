namespace proyeco1_ocl
{
    partial class forx
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.abrir_archivo = new System.Windows.Forms.Button();
            this.entrada = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // abrir_archivo
            // 
            this.abrir_archivo.Location = new System.Drawing.Point(13, 13);
            this.abrir_archivo.Name = "abrir_archivo";
            this.abrir_archivo.Size = new System.Drawing.Size(193, 31);
            this.abrir_archivo.TabIndex = 0;
            this.abrir_archivo.Text = "Abrir";
            this.abrir_archivo.UseVisualStyleBackColor = true;
            this.abrir_archivo.Click += new System.EventHandler(this.abrir_archivo_Click);
            // 
            // entrada
            // 
            this.entrada.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entrada.Location = new System.Drawing.Point(13, 71);
            this.entrada.Name = "entrada";
            this.entrada.Size = new System.Drawing.Size(1185, 402);
            this.entrada.TabIndex = 1;
            this.entrada.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1049, 504);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Compilar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(212, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(162, 32);
            this.button2.TabIndex = 3;
            this.button2.Text = "Guardar";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(381, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(177, 31);
            this.button3.TabIndex = 4;
            this.button3.Text = "Guardar como";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1083, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(114, 32);
            this.button4.TabIndex = 5;
            this.button4.Text = "Acerca de";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // forx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 539);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.entrada);
            this.Controls.Add(this.abrir_archivo);
            this.Name = "forx";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button abrir_archivo;
        private System.Windows.Forms.RichTextBox entrada;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

