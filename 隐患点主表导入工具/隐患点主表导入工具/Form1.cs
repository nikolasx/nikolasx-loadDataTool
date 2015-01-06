using System;
using System.IO;
using System.Windows.Forms;
using 隐患点主表导入工具.Investigation;
using 隐患点主表导入工具.MassPres;
using 隐患点主表导入工具.MineRecovery;
using 隐患点主表导入工具.PotentialThreats;
using 隐患点主表导入工具.Relocation;

namespace 隐患点主表导入工具
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //导入基础数据
        //十万调查，移民搬迁，五万详查
        //选择基础数据导入的文件路径
        private void baseSelect_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = @"ExcelFiles&AccessFiles|*.xls;*.xlsx;*.MDB;*.mdb";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                baseFile.Text = dialog.FileName;
            }
        }
        //选择基础数据导入的综合表文件路径
        private void baseComSelect_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = @"ExcelFiles&AccessFiles|*.xls;*.xlsx;*.MDB;*.mdb";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                baseComFile.Text = dialog.FileName;
            }
        }
        //导入基础数据
        private void loadBaseBtn_Click(object sender, EventArgs e)
        {
            baseTip.Text = string.Empty;
            string baseFileRoad = baseFile.Text,
                baseComFileRoad = baseComFile.Text,
                loadResult = string.Empty;
            if (string.IsNullOrEmpty(baseDataComboBox.Text))
            {
                MessageBox.Show(@"请选择数据类型");
                return;
            }
            if (string.IsNullOrEmpty(baseFileRoad) || string.IsNullOrEmpty(baseComFileRoad))
            {
                MessageBox.Show(@"请选择数据文件路径");
                return;
            }
            try
            {
                switch (baseDataComboBox.Text)
                {
                    case "十万调查":
                        LoadComprehensive loadComprehensive = new LoadComprehensive();
                        loadResult = loadComprehensive.InsertBaseDisasterInfo(baseFileRoad, baseComFileRoad);
                        break;
                    case "移民搬迁":
                        LoadRelocationComprehensive loadRelocationComprehensive = new LoadRelocationComprehensive();
                        loadResult = loadRelocationComprehensive.InsertBaseRelocationInfo(baseFileRoad, baseComFileRoad);
                        break;
                }
                File.AppendAllText(@"e:\loadresult.txt", loadResult);
                baseTip.Text = @"完成导入";
            }
            catch (Exception ex)
            {
                baseTip.Text = ex.Message;
            }

        }

        //选择属性表路径
        private void componentFileSelect_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = @"ExcelFiles&AccessFiles|*.xls;*.xlsx;*.MDB;*.mdb";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                componentFile.Text = dialog.FileName;
            }
        }
        //导入属性表数据
        private void componentFileBtn_Click(object sender, EventArgs e)
        {
            componentTip.Text = string.Empty;
            string fileRoad = componentFile.Text, loadResult = string.Empty;
            if (string.IsNullOrEmpty(componentComboBox.Text))
            {
                MessageBox.Show(@"请选择数据类型");
                return;
            }
            if (string.IsNullOrEmpty(fileRoad))
            {
                MessageBox.Show(@"请选择文件路径");
                return;
            }
            try
            {
                switch (componentComboBox.Text)
                {
                    case "隐患点主表":
                        LoadThreat loadThreat = new LoadThreat();
                        loadResult = loadThreat.InsertData(fileRoad);
                        break;
                    case "月报速报":
                        LoadMonthlyReport loadMonthlyReport = new LoadMonthlyReport();
                        loadResult = loadMonthlyReport.InsertData(fileRoad);
                        break;
                    case "应急调查":
                        LoadEmergencySurvey loadEmergencySurvey = new LoadEmergencySurvey();
                        loadResult = loadEmergencySurvey.InsertData(fileRoad);
                        break;
                    case "防灾预案表":
                        LoadPrePlan loadPrePlan=new LoadPrePlan();
                        loadResult = loadPrePlan.InsertData(fileRoad);
                        break;
                    case "工作明白卡":
                        LoadWorkingGuideCards loadCards=new LoadWorkingGuideCards();
                        loadResult = loadCards.InsertData(fileRoad);
                        break;
                    case "避险明白卡":
                        LoadAvoidRiskCards loadAvoid=new LoadAvoidRiskCards();
                        loadResult = loadAvoid.InsertData(fileRoad);
                        break;
                }
                File.AppendAllText(@"e:\loadresult.txt", loadResult);
                componentTip.Text = @"完成导入";
            }
            catch (Exception ex)
            {
                componentTip.Text = ex.Message;
            }

        }

        //选择其他表的数据路径
        private void otherFileSelect_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = @"ExcelFiles&AccessFiles|*.xls;*.xlsx;*.MDB;*.mdb";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                otherFile.Text = dialog.FileName;
            }
        }
        //导入其他表的数据
        private void otherFileBtn_Click(object sender, EventArgs e)
        {
            otherTip.Text = string.Empty;
            string fileRoad = otherFile.Text, loadResult = string.Empty;

            if (string.IsNullOrEmpty(otherComboBox.Text))
            {
                MessageBox.Show(@"请选择文件类型");
                return;
            }
            if (string.IsNullOrEmpty(fileRoad))
            {
                MessageBox.Show(@"请选择文件路径");
                return;
            }
            try
            {
                switch (otherComboBox.Text)
                {
                    case "矿山复绿基础档案表":
                        LoadMineArchive loadMineArchive = new LoadMineArchive();
                        loadResult = loadMineArchive.InsertData(fileRoad);
                        break;
                    case "矿山复绿环境调查表":
                        LoadMineEnvironmentSurvey loadEnvironmentSurvey = new LoadMineEnvironmentSurvey();
                        loadResult = loadEnvironmentSurvey.InsertData(fileRoad);
                        break;
                    case "矿山复绿遥感解译卡":
                        LoadMineRemoteSensingCard loadCard = new LoadMineRemoteSensingCard();
                        loadResult = loadCard.InsertData(fileRoad);
                        break;
                    case "移民搬迁泥石流核查表":
                        LoadRelocationDebrisFlowCheck loadDebrisFlowCheck = new LoadRelocationDebrisFlowCheck();
                        loadResult = loadDebrisFlowCheck.InsertData(fileRoad);
                        break;
                    case "移民搬迁崩塌核查表":
                        LoadRelocationLandSlipCheck loadLandSlipCheck = new LoadRelocationLandSlipCheck();
                        loadResult = loadLandSlipCheck.InsertData(fileRoad);
                        break;
                    case "移民搬迁滑坡核查表":
                        LoadRelocationLandSlideCheck loadLandSlideCheck = new LoadRelocationLandSlideCheck();
                        loadResult = loadLandSlideCheck.InsertData(fileRoad);
                        break;
                    case "移民搬迁斜坡核查表":
                        LoadRelocationSlopeCheck loadSlopeCheck = new LoadRelocationSlopeCheck();
                        loadResult = loadSlopeCheck.InsertData(fileRoad);
                        break;
                    case "移民搬迁地面塌陷核查表":
                        LoadRelocationLandCollapseCheck loadLandCollapseCheck =
                            new LoadRelocationLandCollapseCheck();
                        loadResult = loadLandCollapseCheck.InsertData(fileRoad);
                        break;
                    case "移民搬迁安置地评价表":
                        LoadRelocationPlaceEvaluation loadPlaceEvaluation = new LoadRelocationPlaceEvaluation();
                        loadResult = loadPlaceEvaluation.InsertData(fileRoad);
                        break;
                    case "防治规划":
                        LoadPreventionPlanning loadPreventionPlan = new LoadPreventionPlanning();
                        loadResult = loadPreventionPlan.InsertData(fileRoad);
                        break;
                }
                File.AppendAllText(@"e:\loadresult.txt", loadResult);
                otherTip.Text = @"完成导入";
            }
            catch (Exception ex)
            {
                otherTip.Text = ex.Message;
            }

        }










    }
}
