using System;
using System.Collections.Generic;


namespace EnglishTest
{
    class Account
    {
        public string username { get; }
        public string password { get; set; }
        public Account(string convert)
        {
            string[] node = convert.Split(",", 2);
            username = node[0];
            password = node[1];
        }
        public Account(string username,string pass)
        {
            this.username = username;
            this.password = pass;
        }
        public override string ToString()
        {
            return this.username.ToUpper() + "," + this.password.ToUpper();
        }
    }

    class Mark
    {
        public float mark { get; set; }
        public DateTime date { get; set; }
        public int idQuestion { get; }
        public Mark(string str)
        {
            string[] node = str.Split("-", 3);
            mark = float.Parse(node[0]);
            date = DateTime.ParseExact(node[1], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            idQuestion = int.Parse(node[2]);
        }
        public Mark(float mark,int idQues)
        {
            this.mark = mark;
            this.idQuestion = idQues;
            date = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }
        public override string ToString()
        {
            return String.Format("{0}-{1}-{2}",mark,date.ToString("dd/MM/yyyy"),idQuestion);
        }
    }
    class User
    {
        public int id { get; }             
        public string name { get; set; }
        public string native { get; set; } //que quan
        public bool gender { get; set; }  //male=True
        public DateTime dOB { get; set; }     
        public DateTime startDay { get; set; }
        public Account account { get;}
        public List<Mark> marks { get; set; }
        public User(int id, string name, string native, bool gender, DateTime dOB, DateTime startDay,Account account,List<Mark> marks) {
            this.id = id;
            this.name = name;
            this.native = native;
            this.gender = gender;
            this.dOB = dOB;
            this.startDay = startDay;
            this.account = account;
            this.marks = marks;
        }
        public User(string[] convert)
        {
            string []node1 = convert[0].Split(",", 6);
            this.id = int.Parse(node1[0]);
            this.name = node1[1];
            this.native = node1[2];
            this.gender = bool.Parse(node1[3]);
            this.dOB = DateTime.ParseExact(node1[4], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            this.startDay = DateTime.ParseExact(node1[5], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            this.account = new Account(convert[1]);
            this.marks = new List<Mark>();
            if (convert[2] != "") {
                string[] node3 = convert[2].Split(",");
                foreach (string i in node3)
                {
                    marks.Add(new Mark(i));
                }
            }
            
        }
        public User(Account account)
        {
            this.id = ControlUser.createdID();
            this.account = account;
            this.marks = new List<Mark>();
            this.startDay = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }
        public void setInfor(string name,string native,bool gender,DateTime dOB)
        {
            this.name = name;
            this.native = native;
            this.gender = gender;
            this.dOB = dOB;
        }
        public void addMark(Mark mark)
        {
            this.marks.Add(mark);
        }
        public override string ToString()
        {
            string s = string.Format("{0},{1},{2},{3},{4},{5}\n",this.id,this.name,this.native,this.gender,this.dOB.ToString("dd/MM/yyyy"),this.startDay.ToString("dd/MM/yyyy"));
            s = s + this.account.ToString() + "\n";
            foreach(Mark i in marks)
            {
                s = s + i.ToString() + ",";
            }
            if(marks.Count!=0)s=s.Remove(s.Length - 1);
            s = s + "\n";
            return s;
        }
    }
}
