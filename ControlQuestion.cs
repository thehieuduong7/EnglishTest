using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EnglishTest
{
    class ControlQuestion
    {
        public List<MulChoice> listMulChoice { get; }
        public List<imcomplete> listImcomplete { get; }
        public List<conversation> listConversation { get; }
        private const string fileMulChoice = "multiChoice.txt";
        private const string fileImcomplete = "imcomplete.txt";
        private const string fileConversation = "conversation.txt";
        viewQuestion viewQ;
        public ControlQuestion()
        {
            this.listMulChoice = InitMC();
            this.listImcomplete = InitImc();
            this.listConversation = InitCon();
            viewQ = new viewQuestion();
        }

        // Search question
        private List<MulChoice> searchMC(int level)
        {
            List<MulChoice> Mc = new List<MulChoice>();
            foreach (MulChoice i in this.listMulChoice)
            {
                if (i.level == level)
                {
                    Mc.Add(i);
                }
            }
            return Mc;
        }
        private List<imcomplete> searchImc(int level)
        {
            List<imcomplete> Imc = new List<imcomplete>();
            foreach (imcomplete i in this.listImcomplete)
            {
                if (i.level == level)
                {
                    Imc.Add(i);
                }
            }
            return Imc;
        }
        private List<conversation> searchCon(int level)
        {
            List<conversation> Con = new List<conversation>();
            foreach (conversation k in listConversation)
            {
                if (k.level == level)
                {
                    Con.Add(k);
                }
            }
            return Con;
        }

        private List<MulChoice> searchMC(DanhMuc danhMuc)
        {
            List<MulChoice> MC = new List<MulChoice>();
            foreach (MulChoice k in this.listMulChoice)
            {
                if (k.danhMuc == danhMuc) { MC.Add(k); }
            }
            return MC;
        }
        private List<imcomplete> searchImc(DanhMuc danhMuc)
        {
            List<imcomplete> Imc = new List<imcomplete>();
            foreach (imcomplete k in this.listImcomplete)
            {
                if (k.danhMuc == danhMuc) { Imc.Add(k); }
            }
            return Imc;
        }
        private List<conversation> searchCon(DanhMuc danhMuc)
        {
            List<conversation> Con = new List<conversation>();
            foreach (conversation k in this.listConversation)
            {
                if (k.danhMuc == danhMuc) { Con.Add(k); }
            }
            return Con;
        }

        private List<MulChoice> searchMC(string content)
        {
            List<MulChoice> MC = new List<MulChoice>();
            foreach (MulChoice k in this.listMulChoice)
            {
                if (k.content.Contains(content))
                {
                    MC.Add(k);
                }
            }
            return MC;
        }
        private List<imcomplete> searchImc(string content)
        {
            List<imcomplete> Imc = new List<imcomplete>();
            foreach (imcomplete k in this.listImcomplete)
            {
                foreach (string line in k.paragraph)
                {
                    if (line.Contains(content))
                    {
                        Imc.Add(k);
                        break;
                    }
                }
            }
            return Imc;
        }
        private List<conversation> searchCon(string content)
        {
            List<conversation> Con = new List<conversation>();
            foreach (conversation k in this.listConversation)
            {
                foreach (string line in k.paragraph)
                {
                    if (line.Contains(content))
                    {
                        Con.Add(k);
                        break;
                    }
                }
            }
            return Con;
        }
        // end of search question
        private List<MulChoice> InitMC()
        {
            List<MulChoice> MC = new List<MulChoice>();
            try
            {
                int i = 0;
                string[] lines = File.ReadAllLines(fileMulChoice);
                while (i < lines.Length)
                {
                    string[] sMc = new string[4];
                    sMc[0] = lines[i++];
                    sMc[1] = lines[i++];
                    sMc[2] = lines[i++];
                    sMc[3] = lines[i++];
                    MulChoice Mc = ConvertToMc(sMc);
                    MC.Add(Mc);
                    i++;
                }
            }catch(IOException e)
            {

            }
            return MC;
        }
        private List<imcomplete> InitImc()
        {
            List<imcomplete> Imc = new List<imcomplete>();
            try
            {
                int i = 0;
                string[] lines = File.ReadAllLines(fileImcomplete);
                while (i < lines.Length)
                {
                    string[] getData = lines[i++].Split(",");
                    int id = int.Parse(getData[0]);
                    int level = int.Parse(getData[1]);
                    float mark = float.Parse(getData[2]);
                    DanhMuc dMuc = this.getDanhMuc(getData[3]);
                    string[] paragraph = convertToParagraph(lines[i++]);
                    List<MulChoice> Mc = new List<MulChoice>();
                    while (lines[i] != "")
                    {
                        string[] sImc = new string[4];
                        sImc[0] = lines[i++];
                        sImc[1] = lines[i++];
                        sImc[2] = lines[i++];
                        sImc[3] = lines[i++];

                        Mc.Add(ConvertToMc(sImc));
                    }
                    Imc.Add(new imcomplete(id, level, dMuc, mark, paragraph, Mc));
                    if (lines[i] == "")
                    {
                        i++;
                    }
                }
            }catch(FormatException e){}
            catch(IOException e) { }
            return Imc;
        }
        private List<conversation> InitCon()
        {
            List<conversation> Con = new List<conversation>();

            int i = 0;
            string[] lines = File.ReadAllLines(fileConversation);
            while (i < lines.Length)
            {
                string[] getData = lines[i++].Split(",");
                int id = int.Parse(getData[0]);
                int level = int.Parse(getData[1]);
                float mark = float.Parse(getData[2]);
                DanhMuc dMuc = this.getDanhMuc(getData[3]);
                string[] paragraph = convertToParagraph(lines[i++]);

                List<MulChoice> Mc = new List<MulChoice>();
                while (lines[i] != "")
                {
                    string[] sImc = new string[4];
                    sImc[0] = lines[i++];
                    sImc[1] = lines[i++];
                    sImc[2] = lines[i++];
                    sImc[3] = lines[i++];

                    Mc.Add(ConvertToMc(sImc));
                }
                Con.Add(new conversation(id, level, dMuc, mark, paragraph, Mc));
                if (lines[i] == "")
                {
                    i++;
                }
            }
            return Con;
        }

        private DanhMuc getDanhMuc(string DMuc)
        {
            DanhMuc danhMuc;
            switch (DMuc.ToUpper())
            {
                case "noun":
                    danhMuc = DanhMuc.DanhTu;
                    break;
                case "verb":
                    danhMuc = DanhMuc.DongTu;
                    break;
                case "adj":
                    danhMuc = DanhMuc.TinhTu;
                    break;
                case "adv":
                    danhMuc = DanhMuc.GioiTu;
                    break;
                default:
                    danhMuc = DanhMuc.DanhMucKhac;
                    break;
            }
            return danhMuc;
        }
        private MulChoice ConvertToMc(string[] lines)
        {
            List<option> options = new List<option>();
            string[] getData = lines[0].Split(",");
            int id = (getData[0] == "") ? -1 : int.Parse(getData[0]);
            int level = (getData[1] == "") ? -1 : int.Parse(getData[1]);
            float mark = (getData[2] == "") ? -1 : float.Parse(getData[2]);
            DanhMuc danhMuc;
            danhMuc = (getData[3] == "") ? DanhMuc.DanhMucKhac : this.getDanhMuc(getData[3]);
            string content = lines[1];
            string[] option = lines[2].Split('/');
            foreach (string k in option)
            {
                string[] o = k.Split('&');
                options.Add(new option(o[0], o[1]));
            }
            option answer;
            string[] answerOption = lines[3].Split('&');
            answer = new option(answerOption[0], answerOption[1]);
            return new MulChoice(id, level, danhMuc, mark, content, options, answer);
        }
        private string[] convertToParagraph(string text)
        {
            string[] para;
            string tem = "\\";
            tem += "n";
            para = text.Split(tem);
            return para;
        }
        public void proShowList()
        {
            viewQ.viewTittle("LIST QUESTION",true);
            viewQ.viewTittle("MULTI CHOICE QUESTION", false);
            foreach (MulChoice i in this.listMulChoice)
            {
                viewQ.viewMC(i);
            }
            viewQ.viewTittle("IMCOMPLETE QUESTION",false);
            foreach (imcomplete i in this.listImcomplete)
            {
                viewQ.viewMQ(i);
            }
            viewQ.viewTittle("CONVERSATION QUESTION", false);
            foreach (conversation i in this.listConversation)
            {
                viewQ.viewMQ(i);
            }
        }
        public int proMenu()
        {
            return viewQ.Menu();
        }
        public void proSearchContent()
        {
            viewQ.viewTittle("SEARCH QUESTION BY CONTENT", true);
            string content = viewQ.inputString("INPUT CONTENT");
            List<MulChoice> a = this.searchMC(content);
            List<imcomplete> b = this.searchImc(content);
            List<conversation> c = this.searchCon(content);
            if (a.Count != 0)
            {
                viewQ.viewMC(a);
            }
            foreach(imcomplete i in b)
            {
                viewQ.viewMQ(i);
            }
            foreach (conversation i in c)
            {
                viewQ.viewMQ(i);
            }
        }
        public void proSearchType()
        {
            viewQ.viewTittle("SEARCH QUESTION BY TYPE", true);
            string danhMuc = viewQ.inputString("INPUT TYPE");
            DanhMuc type=this.getDanhMuc(danhMuc);
            List<MulChoice> a = this.searchMC(type);
            List<imcomplete> b = this.searchImc(type);
            List<conversation> c = this.searchCon(type);
            bool ok;
            if (a.Count != 0)
            {
                viewQ.viewMC(a);
            }
            foreach (imcomplete i in b)
            {
                viewQ.viewMQ(i);
            }
            foreach (conversation i in c)
            {
                viewQ.viewMQ(i);
            }
        }
        public void proSearchLevel()
        {
            viewQ.viewTittle("SEARCH QUESTION BY LEVEL", true);
            int level=0;
            try
            {
                level = int.Parse(viewQ.inputString("INPUT LEVEL"));
            }
            catch(FormatException e) { viewQ.errorMsg("INPUT FAIL!");return; }

            List<MulChoice> a = this.searchMC(level);
            List<imcomplete> b = this.searchImc(level);
            List<conversation> c = this.searchCon(level);
            if (a.Count != 0)
            {
                viewQ.viewMC(a);
            }
            foreach (imcomplete i in b)
            {
                viewQ.viewMQ(i);
            }
            foreach (conversation i in c)
            {
                viewQ.viewMQ(i);
            }
        }
    }
}
