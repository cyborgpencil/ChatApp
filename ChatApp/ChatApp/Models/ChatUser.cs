using Prism.Mvvm;
using System;
using System.Text;

namespace ChatApp.Models
{
    public class ChatUser : BindableBase
    {
        private StringBuilder _userName;
        public StringBuilder UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
    }
}
