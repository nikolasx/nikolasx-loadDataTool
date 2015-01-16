using System;
using System.IO;
using System.Windows.Forms;

namespace 隐患点主表导入工具
{
    public partial class Tools : Form
    {
        public Tools()
        {
            InitializeComponent();
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = @"Access Files|*.mdb;*.MDB";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath.Text = dialog.FileName;
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            filePath.Text = string.Empty;
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            tip.Text = @"正在导入…";
            string fileRoad = filePath.Text;
            if (string.IsNullOrEmpty(fileRoad))
            {
                MessageBox.Show(@"请选择文件");
                return;
            }

            try
            {
                LoadXinjiangData load = new LoadXinjiangData();
                string result = load.LoadDataByOnekey(fileRoad);
                File.AppendAllText(@"e:\loadresult.txt", result);
                tip.Text = @"完成导入,导入结果在[e:\loadresult.txt]文件中";
            }
            catch (Exception ex)
            {
                tip.Text = ex.Message;
            }


        }

        //选择五万详查数据路径
        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = @"MDB Files|*.MDB;*.mdb";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }


        //导入五万详查数据
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show(@"请选择文件");
                return;
            }
            tip.Text = @"正在导入…";
            try
            {
                LoadXinjiangData load = new LoadXinjiangData();
                string result = load.LoadDataFSByOnekey(textBox1.Text);
                File.AppendAllText(@"e:\loadresult.txt", result);
                tip.Text = @"完成导入,导入结果在[e:\loadresult.txt]文件中";
            }
            catch (Exception ex)
            {
                tip.Text = ex.Message;
            }
        }



    }
}
