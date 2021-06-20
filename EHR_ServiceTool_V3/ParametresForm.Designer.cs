namespace EHR_ServiceTool_V3
{
    partial class ParametresForm
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
            this.NextPageButton = new System.Windows.Forms.Button();
            this.PreviousPageButton = new System.Windows.Forms.Button();
            this.SendToDeviceButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PageNo = new System.Windows.Forms.Label();
            this.LocationIndex = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Param2 = new System.Windows.Forms.Label();
            this.Param1 = new System.Windows.Forms.Label();
            this.Param3 = new System.Windows.Forms.Label();
            this.Param4 = new System.Windows.Forms.Label();
            this.Param5 = new System.Windows.Forms.Label();
            this.Param6 = new System.Windows.Forms.Label();
            this.Param7 = new System.Windows.Forms.Label();
            this.Param8 = new System.Windows.Forms.Label();
            this.Param9 = new System.Windows.Forms.Label();
            this.Param10 = new System.Windows.Forms.Label();
            this.Param11 = new System.Windows.Forms.Label();
            this.Param12 = new System.Windows.Forms.Label();
            this.Param13 = new System.Windows.Forms.Label();
            this.Param14 = new System.Windows.Forms.Label();
            this.Param15 = new System.Windows.Forms.Label();
            this.Param16 = new System.Windows.Forms.Label();
            this.Param17 = new System.Windows.Forms.Label();
            this.Param18 = new System.Windows.Forms.Label();
            this.Param19 = new System.Windows.Forms.Label();
            this.Param20 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // NextPageButton
            // 
            this.NextPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NextPageButton.Location = new System.Drawing.Point(708, 2);
            this.NextPageButton.Margin = new System.Windows.Forms.Padding(4);
            this.NextPageButton.Name = "NextPageButton";
            this.NextPageButton.Size = new System.Drawing.Size(133, 31);
            this.NextPageButton.TabIndex = 42;
            this.NextPageButton.Text = "Next Page >>";
            this.NextPageButton.UseVisualStyleBackColor = true;
            this.NextPageButton.Click += new System.EventHandler(this.NextPageButton_Click);
            // 
            // PreviousPageButton
            // 
            this.PreviousPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PreviousPageButton.Location = new System.Drawing.Point(7, 2);
            this.PreviousPageButton.Margin = new System.Windows.Forms.Padding(4);
            this.PreviousPageButton.Name = "PreviousPageButton";
            this.PreviousPageButton.Size = new System.Drawing.Size(133, 31);
            this.PreviousPageButton.TabIndex = 43;
            this.PreviousPageButton.Text = "<< Previous Page";
            this.PreviousPageButton.UseVisualStyleBackColor = true;
            this.PreviousPageButton.Click += new System.EventHandler(this.PreviousPageButton_Click);
            // 
            // SendToDeviceButton
            // 
            this.SendToDeviceButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SendToDeviceButton.Location = new System.Drawing.Point(355, 2);
            this.SendToDeviceButton.Margin = new System.Windows.Forms.Padding(4);
            this.SendToDeviceButton.Name = "SendToDeviceButton";
            this.SendToDeviceButton.Size = new System.Drawing.Size(133, 31);
            this.SendToDeviceButton.TabIndex = 44;
            this.SendToDeviceButton.Text = "Send To Device";
            this.SendToDeviceButton.UseVisualStyleBackColor = true;
            this.SendToDeviceButton.Click += new System.EventHandler(this.SendToDeviceButton_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PageNo
            // 
            this.PageNo.AutoSize = true;
            this.PageNo.Location = new System.Drawing.Point(560, 347);
            this.PageNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PageNo.Name = "PageNo";
            this.PageNo.Size = new System.Drawing.Size(16, 17);
            this.PageNo.TabIndex = 45;
            this.PageNo.Text = "0";
            this.PageNo.Visible = false;
            // 
            // LocationIndex
            // 
            this.LocationIndex.AutoSize = true;
            this.LocationIndex.Location = new System.Drawing.Point(228, 337);
            this.LocationIndex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LocationIndex.Name = "LocationIndex";
            this.LocationIndex.Size = new System.Drawing.Size(0, 17);
            this.LocationIndex.TabIndex = 91;
            this.LocationIndex.Visible = false;
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.textBox20);
            this.panel1.Controls.Add(this.textBox19);
            this.panel1.Controls.Add(this.textBox18);
            this.panel1.Controls.Add(this.textBox17);
            this.panel1.Controls.Add(this.textBox16);
            this.panel1.Controls.Add(this.textBox15);
            this.panel1.Controls.Add(this.textBox14);
            this.panel1.Controls.Add(this.textBox13);
            this.panel1.Controls.Add(this.textBox12);
            this.panel1.Controls.Add(this.textBox11);
            this.panel1.Controls.Add(this.textBox10);
            this.panel1.Controls.Add(this.textBox9);
            this.panel1.Controls.Add(this.textBox8);
            this.panel1.Controls.Add(this.textBox7);
            this.panel1.Controls.Add(this.textBox6);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.Param2);
            this.panel1.Controls.Add(this.Param1);
            this.panel1.Controls.Add(this.Param3);
            this.panel1.Controls.Add(this.Param4);
            this.panel1.Controls.Add(this.Param5);
            this.panel1.Controls.Add(this.Param6);
            this.panel1.Controls.Add(this.Param7);
            this.panel1.Controls.Add(this.Param8);
            this.panel1.Controls.Add(this.Param9);
            this.panel1.Controls.Add(this.Param10);
            this.panel1.Controls.Add(this.Param11);
            this.panel1.Controls.Add(this.Param12);
            this.panel1.Controls.Add(this.Param13);
            this.panel1.Controls.Add(this.Param14);
            this.panel1.Controls.Add(this.Param15);
            this.panel1.Controls.Add(this.Param16);
            this.panel1.Controls.Add(this.Param17);
            this.panel1.Controls.Add(this.Param18);
            this.panel1.Controls.Add(this.Param19);
            this.panel1.Controls.Add(this.Param20);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 334);
            this.panel1.TabIndex = 92;
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(749, 295);
            this.textBox20.Margin = new System.Windows.Forms.Padding(4);
            this.textBox20.Name = "textBox20";
            this.textBox20.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox20.Size = new System.Drawing.Size(79, 22);
            this.textBox20.TabIndex = 112;
            this.textBox20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox20.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox19
            // 
            this.textBox19.Location = new System.Drawing.Point(749, 263);
            this.textBox19.Margin = new System.Windows.Forms.Padding(4);
            this.textBox19.Name = "textBox19";
            this.textBox19.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox19.Size = new System.Drawing.Size(79, 22);
            this.textBox19.TabIndex = 111;
            this.textBox19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox19.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(749, 231);
            this.textBox18.Margin = new System.Windows.Forms.Padding(4);
            this.textBox18.Name = "textBox18";
            this.textBox18.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox18.Size = new System.Drawing.Size(79, 22);
            this.textBox18.TabIndex = 110;
            this.textBox18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox18.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(749, 199);
            this.textBox17.Margin = new System.Windows.Forms.Padding(4);
            this.textBox17.Name = "textBox17";
            this.textBox17.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox17.Size = new System.Drawing.Size(79, 22);
            this.textBox17.TabIndex = 109;
            this.textBox17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox17.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(749, 167);
            this.textBox16.Margin = new System.Windows.Forms.Padding(4);
            this.textBox16.Name = "textBox16";
            this.textBox16.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox16.Size = new System.Drawing.Size(79, 22);
            this.textBox16.TabIndex = 108;
            this.textBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox16.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(749, 135);
            this.textBox15.Margin = new System.Windows.Forms.Padding(4);
            this.textBox15.Name = "textBox15";
            this.textBox15.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox15.Size = new System.Drawing.Size(79, 22);
            this.textBox15.TabIndex = 107;
            this.textBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox15.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(749, 103);
            this.textBox14.Margin = new System.Windows.Forms.Padding(4);
            this.textBox14.Name = "textBox14";
            this.textBox14.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox14.Size = new System.Drawing.Size(79, 22);
            this.textBox14.TabIndex = 106;
            this.textBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox14.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(749, 71);
            this.textBox13.Margin = new System.Windows.Forms.Padding(4);
            this.textBox13.Name = "textBox13";
            this.textBox13.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox13.Size = new System.Drawing.Size(79, 22);
            this.textBox13.TabIndex = 105;
            this.textBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox13.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(749, 39);
            this.textBox12.Margin = new System.Windows.Forms.Padding(4);
            this.textBox12.Name = "textBox12";
            this.textBox12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox12.Size = new System.Drawing.Size(79, 22);
            this.textBox12.TabIndex = 104;
            this.textBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox12.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(749, 7);
            this.textBox11.Margin = new System.Windows.Forms.Padding(4);
            this.textBox11.Name = "textBox11";
            this.textBox11.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox11.Size = new System.Drawing.Size(79, 22);
            this.textBox11.TabIndex = 103;
            this.textBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox11.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(337, 295);
            this.textBox10.Margin = new System.Windows.Forms.Padding(4);
            this.textBox10.Name = "textBox10";
            this.textBox10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox10.Size = new System.Drawing.Size(79, 22);
            this.textBox10.TabIndex = 92;
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox10.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(337, 263);
            this.textBox9.Margin = new System.Windows.Forms.Padding(4);
            this.textBox9.Name = "textBox9";
            this.textBox9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox9.Size = new System.Drawing.Size(79, 22);
            this.textBox9.TabIndex = 91;
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox9.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(337, 231);
            this.textBox8.Margin = new System.Windows.Forms.Padding(4);
            this.textBox8.Name = "textBox8";
            this.textBox8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox8.Size = new System.Drawing.Size(79, 22);
            this.textBox8.TabIndex = 90;
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox8.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(337, 199);
            this.textBox7.Margin = new System.Windows.Forms.Padding(4);
            this.textBox7.Name = "textBox7";
            this.textBox7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox7.Size = new System.Drawing.Size(79, 22);
            this.textBox7.TabIndex = 89;
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox7.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(337, 167);
            this.textBox6.Margin = new System.Windows.Forms.Padding(4);
            this.textBox6.Name = "textBox6";
            this.textBox6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox6.Size = new System.Drawing.Size(79, 22);
            this.textBox6.TabIndex = 88;
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox6.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(337, 135);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4);
            this.textBox5.Name = "textBox5";
            this.textBox5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox5.Size = new System.Drawing.Size(79, 22);
            this.textBox5.TabIndex = 87;
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox5.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(337, 103);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox4.Size = new System.Drawing.Size(79, 22);
            this.textBox4.TabIndex = 86;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox4.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(337, 71);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox3.Size = new System.Drawing.Size(79, 22);
            this.textBox3.TabIndex = 85;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox3.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(337, 39);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox2.Size = new System.Drawing.Size(79, 22);
            this.textBox2.TabIndex = 84;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox2.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(337, 7);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox1.Size = new System.Drawing.Size(79, 22);
            this.textBox1.TabIndex = 83;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Leave += new System.EventHandler(this.TextBoxLeave);
            // 
            // Param2
            // 
            this.Param2.AutoSize = true;
            this.Param2.Location = new System.Drawing.Point(3, 43);
            this.Param2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param2.Name = "Param2";
            this.Param2.Size = new System.Drawing.Size(57, 17);
            this.Param2.TabIndex = 93;
            this.Param2.Text = "Param2";
            // 
            // Param1
            // 
            this.Param1.AutoSize = true;
            this.Param1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Param1.Location = new System.Drawing.Point(3, 11);
            this.Param1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param1.Name = "Param1";
            this.Param1.Size = new System.Drawing.Size(57, 17);
            this.Param1.TabIndex = 82;
            this.Param1.Text = "Param1";
            this.Param1.Click += new System.EventHandler(this.Param1_Click);
            // 
            // Param3
            // 
            this.Param3.AutoSize = true;
            this.Param3.Location = new System.Drawing.Point(3, 75);
            this.Param3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param3.Name = "Param3";
            this.Param3.Size = new System.Drawing.Size(57, 17);
            this.Param3.TabIndex = 94;
            this.Param3.Text = "Param3";
            // 
            // Param4
            // 
            this.Param4.AutoSize = true;
            this.Param4.Location = new System.Drawing.Point(3, 107);
            this.Param4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param4.Name = "Param4";
            this.Param4.Size = new System.Drawing.Size(57, 17);
            this.Param4.TabIndex = 95;
            this.Param4.Text = "Param4";
            // 
            // Param5
            // 
            this.Param5.AutoSize = true;
            this.Param5.Location = new System.Drawing.Point(3, 139);
            this.Param5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param5.Name = "Param5";
            this.Param5.Size = new System.Drawing.Size(57, 17);
            this.Param5.TabIndex = 96;
            this.Param5.Text = "Param5";
            // 
            // Param6
            // 
            this.Param6.AutoSize = true;
            this.Param6.Location = new System.Drawing.Point(3, 171);
            this.Param6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param6.Name = "Param6";
            this.Param6.Size = new System.Drawing.Size(57, 17);
            this.Param6.TabIndex = 97;
            this.Param6.Text = "Param6";
            // 
            // Param7
            // 
            this.Param7.AutoSize = true;
            this.Param7.Location = new System.Drawing.Point(3, 203);
            this.Param7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param7.Name = "Param7";
            this.Param7.Size = new System.Drawing.Size(57, 17);
            this.Param7.TabIndex = 98;
            this.Param7.Text = "Param7";
            // 
            // Param8
            // 
            this.Param8.AutoSize = true;
            this.Param8.Location = new System.Drawing.Point(3, 235);
            this.Param8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param8.Name = "Param8";
            this.Param8.Size = new System.Drawing.Size(57, 17);
            this.Param8.TabIndex = 99;
            this.Param8.Text = "Param8";
            // 
            // Param9
            // 
            this.Param9.AutoSize = true;
            this.Param9.Location = new System.Drawing.Point(3, 267);
            this.Param9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param9.Name = "Param9";
            this.Param9.Size = new System.Drawing.Size(57, 17);
            this.Param9.TabIndex = 100;
            this.Param9.Text = "Param9";
            // 
            // Param10
            // 
            this.Param10.AutoSize = true;
            this.Param10.Location = new System.Drawing.Point(3, 299);
            this.Param10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param10.Name = "Param10";
            this.Param10.Size = new System.Drawing.Size(65, 17);
            this.Param10.TabIndex = 101;
            this.Param10.Text = "Param10";
            // 
            // Param11
            // 
            this.Param11.AutoSize = true;
            this.Param11.Location = new System.Drawing.Point(425, 11);
            this.Param11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param11.Name = "Param11";
            this.Param11.Size = new System.Drawing.Size(65, 17);
            this.Param11.TabIndex = 102;
            this.Param11.Text = "Param11";
            // 
            // Param12
            // 
            this.Param12.AutoSize = true;
            this.Param12.Location = new System.Drawing.Point(425, 43);
            this.Param12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param12.Name = "Param12";
            this.Param12.Size = new System.Drawing.Size(65, 17);
            this.Param12.TabIndex = 113;
            this.Param12.Text = "Param12";
            // 
            // Param13
            // 
            this.Param13.AutoSize = true;
            this.Param13.Location = new System.Drawing.Point(425, 75);
            this.Param13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param13.Name = "Param13";
            this.Param13.Size = new System.Drawing.Size(65, 17);
            this.Param13.TabIndex = 114;
            this.Param13.Text = "Param13";
            // 
            // Param14
            // 
            this.Param14.AutoSize = true;
            this.Param14.Location = new System.Drawing.Point(425, 107);
            this.Param14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param14.Name = "Param14";
            this.Param14.Size = new System.Drawing.Size(65, 17);
            this.Param14.TabIndex = 115;
            this.Param14.Text = "Param14";
            // 
            // Param15
            // 
            this.Param15.AutoSize = true;
            this.Param15.Location = new System.Drawing.Point(425, 139);
            this.Param15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param15.Name = "Param15";
            this.Param15.Size = new System.Drawing.Size(65, 17);
            this.Param15.TabIndex = 116;
            this.Param15.Text = "Param15";
            // 
            // Param16
            // 
            this.Param16.AutoSize = true;
            this.Param16.Location = new System.Drawing.Point(425, 171);
            this.Param16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param16.Name = "Param16";
            this.Param16.Size = new System.Drawing.Size(65, 17);
            this.Param16.TabIndex = 117;
            this.Param16.Text = "Param16";
            // 
            // Param17
            // 
            this.Param17.AutoSize = true;
            this.Param17.Location = new System.Drawing.Point(425, 203);
            this.Param17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param17.Name = "Param17";
            this.Param17.Size = new System.Drawing.Size(65, 17);
            this.Param17.TabIndex = 118;
            this.Param17.Text = "Param17";
            // 
            // Param18
            // 
            this.Param18.AutoSize = true;
            this.Param18.Location = new System.Drawing.Point(425, 235);
            this.Param18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param18.Name = "Param18";
            this.Param18.Size = new System.Drawing.Size(65, 17);
            this.Param18.TabIndex = 119;
            this.Param18.Text = "Param18";
            // 
            // Param19
            // 
            this.Param19.AutoSize = true;
            this.Param19.Location = new System.Drawing.Point(425, 267);
            this.Param19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param19.Name = "Param19";
            this.Param19.Size = new System.Drawing.Size(65, 17);
            this.Param19.TabIndex = 120;
            this.Param19.Text = "Param19";
            // 
            // Param20
            // 
            this.Param20.AutoSize = true;
            this.Param20.Location = new System.Drawing.Point(425, 299);
            this.Param20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Param20.Name = "Param20";
            this.Param20.Size = new System.Drawing.Size(65, 17);
            this.Param20.TabIndex = 121;
            this.Param20.Text = "Param20";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.PreviousPageButton);
            this.panel2.Controls.Add(this.SendToDeviceButton);
            this.panel2.Controls.Add(this.NextPageButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 337);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(844, 37);
            this.panel2.TabIndex = 93;
            // 
            // ParametresForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 374);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LocationIndex);
            this.Controls.Add(this.PageNo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParametresForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ParametresForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ParametresForm_FormClosed);
            this.SizeChanged += new System.EventHandler(this.ParametresForm_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button NextPageButton;
        private System.Windows.Forms.Button PreviousPageButton;
        private System.Windows.Forms.Button SendToDeviceButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label PageNo;
        private System.Windows.Forms.Label LocationIndex;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox textBox20;
        public System.Windows.Forms.TextBox textBox19;
        public System.Windows.Forms.TextBox textBox18;
        public System.Windows.Forms.TextBox textBox17;
        public System.Windows.Forms.TextBox textBox16;
        public System.Windows.Forms.TextBox textBox15;
        public System.Windows.Forms.TextBox textBox14;
        public System.Windows.Forms.TextBox textBox13;
        public System.Windows.Forms.TextBox textBox12;
        public System.Windows.Forms.TextBox textBox11;
        public System.Windows.Forms.TextBox textBox10;
        public System.Windows.Forms.TextBox textBox9;
        public System.Windows.Forms.TextBox textBox8;
        public System.Windows.Forms.TextBox textBox7;
        public System.Windows.Forms.TextBox textBox6;
        public System.Windows.Forms.TextBox textBox5;
        public System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Label Param2;
        public System.Windows.Forms.Label Param1;
        public System.Windows.Forms.Label Param3;
        public System.Windows.Forms.Label Param4;
        public System.Windows.Forms.Label Param5;
        public System.Windows.Forms.Label Param6;
        public System.Windows.Forms.Label Param7;
        public System.Windows.Forms.Label Param8;
        public System.Windows.Forms.Label Param9;
        public System.Windows.Forms.Label Param10;
        public System.Windows.Forms.Label Param11;
        public System.Windows.Forms.Label Param12;
        public System.Windows.Forms.Label Param13;
        public System.Windows.Forms.Label Param14;
        public System.Windows.Forms.Label Param15;
        public System.Windows.Forms.Label Param16;
        public System.Windows.Forms.Label Param17;
        public System.Windows.Forms.Label Param18;
        public System.Windows.Forms.Label Param19;
        public System.Windows.Forms.Label Param20;
        private System.Windows.Forms.Panel panel2;
    }
}