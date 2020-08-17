using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Viewにバインドするクラス（更新通知あり）
namespace RedactedDemoManeger
{
    public class Dirs : INotifyPropertyChanged
    {
        private string red;
        public string Red
        {
            get { return red; }
            set
            {
                red = value;
                OnPropertyChanged("Red");
            }
        }
        private string res;
        public string Res
        {
            get { return res; }
            set
            {
                res = value;
                OnPropertyChanged("Res");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
