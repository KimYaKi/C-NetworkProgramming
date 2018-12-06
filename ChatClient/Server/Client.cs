using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Client
    {
        private string userID;
        private string userPwd;
        private string userName;

        public void setUserID(string userID)
        {
            this.userID = userID;
        }
        public string getUserID()
        {
            return userID;
        }
        public void setUserPwd(string userPwd)
        {
            this.userPwd = userPwd;
        }
        public string getUserPwd()
        {
            return userPwd;
        }
        public void setUserName(string userName)
        {
            this.userName = userName;
        }
        public string getUserName()
        {
            return userName;
        }
    }
}
