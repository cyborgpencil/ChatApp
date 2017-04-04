using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppServer.ViewModels
{
    public class UserManager
    {
        public DirectoryInfo Root { get; set; }
        public string DirectoryPath { get; set; }
        public string fileDir { get; set; }
        public FileStream UserFile { get; set; }
        public List<Users> LoggedInUsers { get; set; }

        public UserManager()
        {
            
            DirectoryPath = "Users";
            fileDir = DirectoryPath + "\\user.txt";

            if (!Directory.Exists(DirectoryPath))
            {
                Root = Directory.CreateDirectory(DirectoryPath);
            }

            //
            UserFile = File.Create(fileDir);
            UserFile.Close();
            LoggedInUsers = new List<Users>();
            Users usr = new Users();
            usr.UserName = "Test";
            LoggedInUsers.Add(usr);
            
        }

        private void WriteCurrentUsers()
        {
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(LoggedInUsers[0].UserName + Environment.NewLine);
                UserFile = File.OpenWrite(fileDir);
                UserFile.Write(buffer, 0, buffer.Length);
                buffer = Encoding.ASCII.GetBytes("Test2");
                UserFile.Write(buffer, 0, buffer.Length);

            }
            catch (Exception)
            {


            }
            UserFile.Close();
        }
    }
}
