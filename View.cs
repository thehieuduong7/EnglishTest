using System;
using System.Collections.Generic;
using System.Threading;


namespace EnglishTest
{
    class ViewTool
    {
        public virtual int viewOption(String tittle, String[] option, bool clear)
        {
            viewTittle(tittle, clear);
            foreach (string i in option)
            {
                Console.WriteLine(String.Format("{0,3}{1,-10}", "", i));
            }
            if (clear)
            {
                for (int i = option.Length; i <= 7; i++)
                {
                    Console.WriteLine();
                }
            }
            while (true)
            {
                try
                {
                    int choice = int.Parse(inputString("YOUR SELECT"));
                    if (choice < option.Length && choice >= 0) return choice;
                    else throw new FormatException();
                }
                catch (FormatException e) { errorMsg("INPUT FAIL!"); }
                finally { Console.WriteLine(); }
            }
        }
        public virtual void viewTittle(string tittle, bool clear)
        {
            if (clear) Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            if (clear) Console.WriteLine(String.Format("{0,15}__________________________{1,5}{2}{3,5}__________________________\n", "", " ", tittle, " "));
            else Console.WriteLine(String.Format("{0,3}{1,-10}", "", tittle));
            Console.ForegroundColor = ConsoleColor.White;
        }
        public virtual string inputString(string tittle)
        {
            Console.Write(String.Format("{0,3}{1:15}: ", "", tittle));
            string input = Console.ReadLine();
            return input;
        }
        public virtual void errorMsg(string error)
        {
            Console.Beep();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0,5}{1,10}", "", error);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public virtual int continueMsg()
        {
            string tittle = "CONTINUE?";
            string[] option = { "1-Yes", "0-No" };
            return viewOption(tittle, option, false);
        }
        public virtual void Msg(string tittle)
        {
            string[] option = { "0-OK" };
            viewOption(tittle, option, false);
        }
    }
    class ViewMenu : ViewTool
    {
        protected string tittle;
        protected string[] option;
        public ViewMenu()
        {
            this.tittle = "MENU";
            string[] t = { "1-MANAGE USER", "2-MANAGE QUESTION", "3-TRAINING", "4-MARK STATISTICS", "0-BACK" };
            this.option = t;
        }
        public virtual int Menu()
        {
            return this.viewOption(this.tittle, this.option, true);
        }
    }
    class ViewStart : ViewMenu
    {
        public ViewStart()
        {
            this.tittle = "WELCOME TO ENGLISH";
            string[] t = { "1-SIGN IN", "2-SIGN UP", "0-EXIT" };
            option = t;
        }
        public virtual void viewExit()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(String.Format("\n\n\n\n                                    " +
                "  .............{0,5}{1}{2,5}.............", " ", "GOOD BYE", " "));
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
        }
    }

    class ViewUser : ViewMenu
    {
        public ViewUser()
        {
            this.tittle = "MANAGE USER";
            string[] t = { "1-VIEW LIST USER", "2-SEARCH USER", "3-ADD NEW USER", "4-DELETE USER", "5-UPDATE USER", "0-BACK" };
            this.option = t;
        }
        public void viewInforUser(User user)
        {
            if (user == null)
            {
                this.errorMsg("Can not found user!");
            }
            else
            {
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(String.Format("{0,30}{1,15}: {2}", "", "ID", user.id));
                Console.WriteLine(String.Format("{0,30}{1,15}: {2}", "", "FULL NAME", user.name));
                Console.WriteLine(String.Format("{0,30}{1,15}: {2}", "", "NATIVE", user.native));
                string temp = (user.gender == true) ? "MALE" : "FEMALE";
                Console.WriteLine(String.Format("{0,30}{1,15}: {2}", "", "GENDER", temp));
                Console.WriteLine(String.Format("{0,30}{1,15}: {2}", "", "DATE OF BIRTH", user.dOB.ToString("dd/MM/yyyy")));
                Console.WriteLine(String.Format("{0,30}{1,15}: {2}", "", "START DAY", user.startDay.ToString("dd/MM/yyyy")));
                Console.WriteLine(String.Format("{0,30}{1,15}: {2}", "", "USERNAME", user.account.username));
                Console.WriteLine(String.Format("{0,30}{1,15}: {2}", "", "PASSWORD", user.account.password));
            }
            Console.WriteLine();
        }
        public void viewInforUser(List<User> listU)
        {
            if (listU == null || listU.Count == 0) this.Msg("List empty!");
            else
            {
                foreach (User i in listU)
                {
                    this.viewInforUser(i);
                }
            }
        }
        public virtual bool inputInfor(ref string name, ref string native, ref bool gender, ref DateTime dob)
        {
            this.viewTittle("INPUT INFORMATION", false);
            while (true)
            {
                try
                {
                    name = this.inputString("FULL NAME").ToUpper();
                    native = this.inputString("NATIVE").ToUpper();
                    string gen = this.inputString("GENDER(M/F)");
                    if (gen == "M" || gen == "m") gender = true;
                    else if (gen == "F" || gen == "f") gender = false;
                    else throw new FormatException();
                    string i = this.inputString("DOB(yyyy/mm/dd)");
                    dob = DateTime.ParseExact(i, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                    return true;
                }
                catch (FormatException e)
                {
                    this.errorMsg("INPUT FAIL!");
                    if (this.continueMsg() == 0) return false;
                }
                finally { Console.WriteLine(); }
            }
        }
        public virtual void inputAccount(ref string username, ref string password)
        {
            this.viewTittle("SIGN IN", true);
            username = this.inputString("Username").ToUpper();
            password = this.inputString("Password").ToUpper();
        }
    }
    class viewStatistics : ViewMenu
    {
        public void showResult(List<Mark> list, float arg)
        {
            this.viewTittle("YOUR RESULT", true);
            if (list.Count != 0)
            {
                Console.WriteLine(string.Format("{0,30}{1,20}||{2,20}", "MARK", "", "DATE"));
                foreach (Mark p in list)
                {
                    Console.WriteLine(string.Format("{0,30}{1,20}||{2,14}{3,5}", p.mark, "", "", p.date.ToString("dd/MM/yyyy")));
                }
                Console.WriteLine("\n\n");
                Console.WriteLine(string.Format("{0,-27}: {1,-8}", "So lan thi trong thang", list.Count));
                Console.WriteLine(string.Format("{0,-27}: {1,-8}", "Diem trung binh trong thang", arg));
            }
            else
            {
                Console.WriteLine(string.Format("{0,15}:", "Khong co bai thi nao trong thang"));
            }
        }
    }
    class viewQuestion : ViewMenu
    {
        public viewQuestion()
        {
            tittle = "MANAGE QUESTION";
            string[] t = { "1-VIEW LIST QUESTION", "2-SEARCH QUESTION BY CONTENT", "3-SEARCH QUESTION BY TYPE", "4-SEARCH QUESTION BY LEVEL", "0-BACK" };
            this.option = t;
        }
        public void viewMC(MulChoice mc)
        {
            if (mc == null)
            {
                Console.WriteLine("QUESTION CAN NOT FOUND!");
            }
            Console.WriteLine(string.Format("\n  {0,5} ({1}d)", mc.content.ToUpper(), mc.mark));
            int dem = 0;
            foreach (option i in mc.options)
            {
                Console.WriteLine(string.Format("{0}. {1,-10}", (char)(dem++ + 'A'), i.content));
            }
            Console.WriteLine("\n");
        }
        public void viewMC(List<MulChoice> list)
        {
            if (list == null || list.Count == 0)
            {
                Console.WriteLine("LIST QUESTION CAN NOT FOUND!");
            }
            else
            {
                foreach (MulChoice i in list)
                {
                    viewMC(i);
                }
            }
        }
        public void viewParagram(string[] para)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (string i in para)
            {
                Console.WriteLine(i);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void viewMQ(MulQuestion mq)
        {
            if (mq == null)
            {
                Console.WriteLine("QUESTION CAN NOT FOUND!");
                return;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Format("({1}d)", "", mq.mark));
            viewParagram(mq.paragraph);
            viewMC(mq.questions);
        }
    }
    class viewTraining:viewQuestion
    {
        public viewTraining()
        {
            this.tittle="TRAINING";
            string[] t = { "1-MULTI CHOICE QUESTION", "2-IMCOMPLETE QUESTION", "3-CONVERSATION QUESTION", "0-BACK" };
            this.option = t;
        }
        public void viewMC(MulChoice mc, option ans)
        {
            base.viewMC(mc);
            Console.Write(string.Format("{0,15}: {1}", "YOUR ANSWER", ans.content));
            if(mc.answer.content==ans.content)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(string.Format(" {0}!", "CORRECT"));
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format(" {0}!", "INCORRECT"));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(string.Format("{0,15}: {1}", "CORRECT ANSWER", mc.answer.content));
            }
            Console.WriteLine(string.Format("{0,15}: {1}", "EXPLAIN", mc.answer.explain));
        }
        public void viewMC(List<MulChoice> mc, List<option> ans)
        {
            for(int i = 0; i < mc.Count; i++)
            {
                viewMC(mc[i], ans[i]);
            }
        }
        public void viewMQ(MulQuestion mq, List<option> ans)
        {
            if (mq == null)
            {
                Console.WriteLine("QUESTION CAN NOT FOUND!");
                return;
            }
            viewParagram(mq.paragraph);
            viewMC(mq.questions, ans);
        }
    }
}
