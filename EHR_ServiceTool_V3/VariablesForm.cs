using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EHR_ServiceTool_V3
{
    public partial class VariablesForm : Form
    {
        private static readonly char[] NumArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static bool GetParam;

        public VariablesForm()
        {
            InitializeComponent();
            NextPageButton.Text = SplashScreen.LSNextPage + " >>";
            PreviousPageButton.Text = "<< " + SplashScreen.LSPreviousPage;
            GetParam = true;
            timer1.Enabled = true;
            timer2.Enabled = true;
            this.Width = (MainForm.CurrentWidth / 2) - 45;
            this.Height = (MainForm.CurrentHeight / 2) - 45;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (GetParam)
            {
                int PageNo = Int32.Parse(this.PageNo.Text);

                if ((PageNo > 1) && (PageNo < 50))
                {
                    PreviousPageButton.Enabled = true;
                    NextPageButton.Enabled = true;
                }
                else
                {
                    if (PageNo == 1)
                    {
                        PreviousPageButton.Enabled = false;
                    }
                    else
                    {
                        NextPageButton.Enabled = false;
                    }
                }

                foreach (TextBox TB in this.panel1.Controls.OfType<TextBox>())
                {
                    string Name = TB.Name;

                    Name = Name.Substring(7, (Name.Length - 7));
                    int ValueNo = Int32.Parse(Name);
                    TB.Text = SetTextBoxText(PageNo, ValueNo);

                }

                foreach (Label label in this.panel1.Controls.OfType<Label>())
                {
                    string Name = label.Name;
                    if ((Name != "PageNo") && (Name != "LocationIndex"))
                    {
                        Name = Name.Substring(3, (Name.Length - 3));
                        int ValueNo = Int32.Parse(Name);
                        label.Text = SetLabelText(PageNo, ValueNo);
                    }

                }
                GetParam = false;
            }

        }
        private string SetTextBoxText(int PageNo, int ValueNo)
        {
            string Value = MainForm.VariableValues[(PageNo - 1) * 20 + (ValueNo - 1)].ToString();
            return Value;
        }
        private static string SetLabelText(int PageNo, int ValueNo)
        {

            string Text = ((PageNo - 1) * 20 + ValueNo).ToString() + ". " + MainForm.VariableNames[((PageNo - 1) * 20) + (ValueNo - 1)] + "..........................................................................................";

            if (Text.Length > 80)
            {
                Text = Text.Substring(0, 80);
            }


            return Text;
        }

        private void NextPageButton_Click(object sender, EventArgs e)
        {
            int PageNo = Int32.Parse(this.PageNo.Text);
            if (PageNo < 50)
            {
                PageNo++;
                this.PageNo.Text = PageNo.ToString();
                if (MainForm.VarPageNames[PageNo] != null)
                {
                    this.Text = MainForm.VarPageNames[PageNo];
                }
                else
                {
                    this.Text = SplashScreen.LSVariables + " " + SplashScreen.LSPage + " " + this.PageNo.Text;
                }
                GetParam = true;
                timer1.Enabled = true;

            }


        }

        private void PreviousPageButton_Click(object sender, EventArgs e)
        {
            int PageNo = Int32.Parse(this.PageNo.Text);
            if (PageNo > 1)
            {
                PageNo--;
                this.PageNo.Text = PageNo.ToString();
                if (MainForm.VarPageNames[PageNo] != null)
                {
                    this.Text = MainForm.VarPageNames[PageNo];
                }
                else
                {
                    this.Text = SplashScreen.LSVariables + " " + SplashScreen.LSPage + " " + this.PageNo.Text;
                }
                GetParam = true;
                timer1.Enabled = true;
            }


        }

        private void VariablesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            int i = Int32.Parse(this.LocationIndex.Text);
            MainForm.IsLocationFilled[i] = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            if (MainForm.VarUpdate)
            {
                if (MainForm.VarUpdateCounter < 30)
                {
                    MainForm.VarUpdateCounter++;
                }
                else
                {
                    MainForm.VarUpdateCounter = 0;
                    MainForm.VarUpdate = false;


                }
                int PageNo = Int32.Parse(this.PageNo.Text);
                foreach (TextBox TB in this.panel1.Controls.OfType<TextBox>())
                {
                    string Name = TB.Name;

                    Name = Name.Substring(7, (Name.Length - 7));
                    int ValueNo = Int32.Parse(Name);
                    TB.Text = SetTextBoxText(PageNo, ValueNo);

                }

            }
            if (MainForm.IsSizeChanged)
            {
                if (MainForm.ChildFormSizeChangedCounter < 4)
                {
                    MainForm.ChildFormSizeChangedCounter++;
                }
                else
                {
                    MainForm.ChildFormSizeChangedCounter = 0;
                    MainForm.IsSizeChanged = false;
                }


                Size size = new Size();
                size.Width = (MainForm.CurrentWidth / 2) - 45;
                size.Height = (MainForm.CurrentHeight / 2) - 45;
                this.Size = size;
                this.Location = SetLocation(Int32.Parse(this.LocationIndex.Text), this.Width, this.Height);


            }
            timer2.Enabled = true;
        }

        private void VariablesForm_SizeChanged(object sender, EventArgs e)
        {
            int Width = this.Width;
            int Height = this.Height;
            panel1.Height = this.Height - 80;
            panel1.Width = Width - 16;
            if ((Width > 700) && (Height > 350))
            {

                panel1.Dock = DockStyle.None;
                panel1.Anchor = AnchorStyles.None;
            }
            else
            {
                panel1.Dock = DockStyle.Top;
                panel1.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            }
        }
        private Point SetLocation(int LocationIndex, int Formwidth, int Formheight)
        {
            Point PointResult = new Point(0, 0);
            if (LocationIndex == 0)
            {
                Point pointxy = new Point(0, 0);
                PointResult = pointxy;
            }
            else if (LocationIndex == 1)
            {
                int width = MainForm.CurrentWidth;
                int pointx = width - Formwidth - 20;
                Point pointxy = new Point(pointx, 0);
                PointResult = pointxy;

            }
            else if (LocationIndex == 2)
            {
                int height = MainForm.CurrentHeight;
                int pointy = height - Formheight - 90;
                Point pointxy = new Point(0, pointy);
                PointResult = pointxy;
            }
            else if (LocationIndex == 3)
            {
                int width = MainForm.CurrentWidth;
                int pointx = width - Formwidth - 20;
                int height = MainForm.CurrentHeight;
                int pointy = height - Formheight - 90;
                Point pointxy = new Point(pointx, pointy);
                PointResult = pointxy;
            }
            return PointResult;
        }

        private void Var1_Click(object sender, EventArgs e)
        {

        }

        private void VariablesForm_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
