using System;
using System.Collections.Generic;
using System.IO;

namespace EnglishTest
{
    class ControlUser
    {
        private static List<User> list;
        private const string filePath = "User.txt";
        private ViewUser viewU;
        public ControlUser()
        {
            InitUser();
            viewU = new ViewUser();
        }
        public void InitUser()
        {
            ControlUser.list = new List<User>();
            try
            {
                if (File.Exists(filePath))
                {
                    string[] u = new string[3];
                    String[] lines = File.ReadAllLines(filePath);
                    User user;
                    for (int i = 0; i < lines.Length; i = i + 3)
                    {
                        u[0] = lines[i]; u[1] = lines[i + 1]; u[2] = lines[i + 2];
                        try
                        {
                            user = new User(u);
                            ControlUser.list.Add(user);
                        }
                        catch (FormatException e) { Console.WriteLine("err"); }
                        catch (IndexOutOfRangeException e) { }
                    }
                }
                else
                {
                    File.Create(filePath).Close();
                }
            }
            catch (IOException e) {; }
        }
        public static int createdID()   // = maxID +1
        {
            if (list.Count == 0) return 100;
            return list[list.Count - 1].id + 1;
        }
        private User accessUser(string username, string pass)
        {
            foreach (User i in ControlUser.list)
            {
                if (i.account.username == username && i.account.password == pass) return i;
            }
            return null;
        }
        private bool containUsername(string username)
        {
            foreach (User i in ControlUser.list)
            {
                if (i.account.username == username) return true;
            }
            return false;
        }
        private User search(int id)
        {
            foreach (User i in ControlUser.list)
            {
                if (i.id == id) return i;
            }
            return null;
        }
        public int proMenu()
        {
            return viewU.Menu();
        }
        public void prosaveUser()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (User i in ControlUser.list)
                    {
                        sw.Write(i.ToString());
                    }
                    sw.Close();
                }
            }
            catch (IOException e)
            {
                ;
            }
        }
        public User proSignIn()
        {
            string acc = "";
            string pass = "";
            while (true)
            {
                viewU.inputAccount(ref acc, ref pass);
                User u = this.accessUser(acc, pass);
                if (u == null)
                {
                    viewU.errorMsg("YOUR USERNAME OR PASSWORD MAY BE INCORRECT!");
                    if (viewU.continueMsg() == 0) return null;
                }
                else
                {
                    return u;
                }
            }
        }
        public User proSignUp()
        {
            viewU.viewTittle("SIGN UP", true);
            string username, pass, confirmPass;
            while (true)
            {
                username = viewU.inputString("USERNAME");
                if (this.containUsername(username) == true)
                {
                    viewU.errorMsg("This username already exists!");
                    if (viewU.continueMsg() == 0) return null;
                }
                else break;
            }
            while (true)
            {
                pass = viewU.inputString("PASSWORD");
                confirmPass = viewU.inputString("CONFIRM PASSWORD");
                if (pass != confirmPass)
                {
                    viewU.errorMsg("Does not match password field!");
                    if (viewU.continueMsg() == 0) return null;
                }
                else break;
            }
            User u = new User(new Account(username, pass));
            this.proUpdateUser(u);
            return u;
        }

        public void proAddUser(User user)
        {
            if (user != null) ControlUser.list.Add(user);
        }

        public void proShowListUser()
        {
            viewU.viewTittle("LIST USER", true);
            viewU.viewInforUser(ControlUser.list);
            viewU.Msg("BACK");
        }

        public void proShowUser(User user)
        {
            viewU.viewInforUser(user);
        }

        public void proSearchUser()
        {
            viewU.viewTittle("SEARCH USER", true);
            string name = "", native = "";
            bool gender = true;
            DateTime dob = new DateTime();
            if (viewU.inputInfor(ref name, ref native, ref gender, ref dob) == true)
            {
                List<User> listU = new List<User>();
                foreach (User i in ControlUser.list)
                {
                    if (i.name.CompareTo(name) == 0 && i.native.CompareTo(native) == 0 && i.gender == gender && i.dOB.CompareTo(dob) == 0)
                    {
                        listU.Add(i);
                    }
                }
                viewU.viewInforUser(listU);
                viewU.Msg("BACK");
            }
        }
        public void proDeleteUser()
        {
            viewU.viewTittle("DELETE USER", true);
            User u;
            while (true)
            {
                try
                {
                    int idT = int.Parse(viewU.inputString("INPUT ID"));
                    u = this.search(idT);
                    viewU.viewInforUser(u);
                    if (u == null)
                    {
                        viewU.Msg("BACK");
                        return;
                    }
                }
                catch (FormatException e)
                {
                    viewU.errorMsg("INPUT FAIL!");
                    if (viewU.continueMsg() == 0) return;
                    else continue;
                }
                if (viewU.continueMsg() == 1) ControlUser.list.Remove(u);
                return;
            }
        }
        public void proUpdateUser()
        {
            viewU.viewTittle("UPDATE INFORMATION USER", true);
            User u;
            while (true)
            {
                try
                {
                    int idT = int.Parse(viewU.inputString("INPUT ID"));
                    u = this.search(idT);
                    viewU.viewInforUser(u);
                    if (u == null)
                    {
                        viewU.Msg("BACK");
                        return;
                    }
                }
                catch (FormatException e)
                {
                    viewU.errorMsg("INPUT FAIL!");
                    if (viewU.continueMsg() == 0) return;
                    else continue;
                }
                if (viewU.continueMsg() == 1) this.proUpdateUser(u);
                return;
            }
        }
        public void proUpdateUser(User u)
        {
            viewU.viewTittle("UPDATE INFORMATION USER", true);
            if (u != null)
            {
                string name = "", native = "";
                bool gender = true;
                DateTime dob = new DateTime();
                if (viewU.inputInfor(ref name, ref native, ref gender, ref dob) == true)
                {
                    u.setInfor(name, native, gender, dob);
                }
            }
            else viewU.Msg("BACK");
        }
    }
}
