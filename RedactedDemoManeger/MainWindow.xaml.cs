using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.IO;
using MSAPI = Microsoft.WindowsAPICodePack;
using System.Reflection.Emit;
using f = System.Windows.Forms;

namespace RedactedDemoManeger
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        
        //RedactedRootの設定取得
        private string getRedDir
        {
            get { return Properties.Settings.Default.RedRoot; }
            set { Properties.Settings.Default.RedRoot = value; }
        }

        //DemoReserveの設定取得
        private string getResDir
        {
            get { return Properties.Settings.Default.ResRoot; }
            set { Properties.Settings.Default.ResRoot = value; }
        }
        //Main
        public MainWindow()
        {
            InitializeComponent();
            
            Dirs dirs = new Dirs();
            dirs.Red = getRedDir;
            dirs.Res = getResDir;
            this.DataContext = dirs;
        }

        //RedactedRootの設定
        private void RedSet_Click(object sender, RoutedEventArgs e)
        {
            //フォルダ選択ダイアログ
            var dlg = new MSAPI::Dialogs.CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            dlg.Title = "Redacted.exeのルートを選択";

            //初期ディレクトリ（よくわからん）
            Assembly myAssembly = Assembly.GetEntryAssembly();
            dlg.InitialDirectory = myAssembly.Location;

            //選択後の処理
            if (dlg.ShowDialog() == MSAPI::Dialogs.CommonFileDialogResult.Ok)
            {
                MessageBox.Show($"{dlg.FileName}をRedactedのRootに設定");

                //設定に反映
                getRedDir = dlg.FileName;
                Properties.Settings.Default.Save();

                //Viewの更新
                Dirs dirs = new Dirs();
                dirs.Red = getRedDir;
                dirs.Res = getResDir;
                this.DataContext = dirs;
            }
        }

        //Reserveの設定
        private void ResSet_Click(object sender, RoutedEventArgs e)
        {
            //フォルダ選択ダイアログ
            var dlg = new MSAPI::Dialogs.CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            dlg.Title = "リザーブディレクトリを選択";

            //初期ディレクトリ
            Assembly myAssembly = Assembly.GetEntryAssembly();
            dlg.InitialDirectory = myAssembly.Location;

            //選択後の処理
            if (dlg.ShowDialog() == MSAPI::Dialogs.CommonFileDialogResult.Ok)
            {
                MessageBox.Show($"{dlg.FileName}をDemoのリザーブディレクトリに設定");

                //設定に反映
                getResDir = dlg.FileName;
                Properties.Settings.Default.Save();

                //Viewの更新
                Dirs dirs = new Dirs();
                dirs.Red = getRedDir;
                dirs.Res = getResDir;
                this.DataContext = dirs;
            }
        }

        private void sw_Weapon(object sender, RoutedEventArgs e)
        {
            string mod_dir = getRedDir + @"\zone\redacted\";
            Boolean mod_en = File.Exists(mod_dir + "patch_redacted.ff");
            if (mod_en)
            {
                string msg = "カスタム武器MODを無効にしますか？";
                string caption = "Switching MOD";
                f.MessageBoxButtons buttons = f.MessageBoxButtons.YesNo;
                f.DialogResult result = f.MessageBox.Show(msg, caption, buttons);
                if(result == f.DialogResult.Yes)
                {
                    File.Move(mod_dir + "patch_redacted.ff", mod_dir + "patch_redacted_old.ff");
                }
            }
            else
            {
                if (File.Exists(mod_dir + "patch_redacted_old.ff"))
                {
                    string msg = "カスタム武器MODを有効にしますか？";
                    string caption = "Switching MOD";
                    f.MessageBoxButtons buttons = f.MessageBoxButtons.YesNo;
                    f.DialogResult result = f.MessageBox.Show(msg, caption, buttons);
                    if(result == f.DialogResult.Yes)
                    {
                        File.Move(mod_dir + "patch_redacted_old.ff", mod_dir + "patch_redacted.ff");
                    }
                }
                else
                {
                    MessageBox.Show("MODファイルが見つかりません。");
                }
            }
        }
    }
}
