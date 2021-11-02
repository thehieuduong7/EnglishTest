using System;
using System.Collections.Generic;

namespace EnglishTest
{
    class ControlTraining
    {
        private User user;
        private List<MulChoice> listMulChoice;
        private List<imcomplete> listImcomplete;
        private List<conversation> listConversation;
        private viewTraining viewTr;
        public ControlTraining(User user, List<MulChoice> lMc, List<imcomplete> lImc, List<conversation> lCon)
        {
            this.user = user;
            this.listMulChoice = lMc;
            this.listImcomplete = lImc;
            this.listConversation = lCon;
            viewTr = new viewTraining();
        }
        public bool inArray(int id)
        {
            int[] listId = this.lId();

            for (int i = 0; i < listId.Length; i++)
            {
                if (listId[i] == id)
                {
                    return true;
                }
            }
            return false;
        }
        public int[] lId()
        {
            int i = 0;
            int[] Lid = new int[this.user.marks.Count + 1];
            foreach (Mark k in this.user.marks)
            {
                Lid[i] = k.idQuestion;
                i++;
            }
            return Lid;
        }
        public option getOption(List<option> ops, string key)
        {
            key = key.ToUpper();
            int k = (int)(key[0] - 'A');
            if (k >= 0 && k < ops.Count) return ops[k];
            return null;
        }

        public List<Mark> checkAnsMC(List<MulChoice> lMc, List<option> lAns)
        {
            List<Mark> MarkMc = new List<Mark>();
            for (int i = 0; i < lMc.Count; i++)
            {
                if (lAns[i].content == lMc[i].answer.content)
                {
                    MarkMc.Add(new Mark(lMc[i].mark, lMc[i].id));
                }
                else { MarkMc.Add(new Mark(0, lMc[i].id)); }
            }
            return MarkMc;
        }
        public Mark checkAnsImc(imcomplete Imc, List<option> lAns)
        {
            List<Mark> tem = checkAnsMC(Imc.questions, lAns);
            float poin = 0;
            foreach (Mark k in tem)
            {
                poin += k.mark;
            }
            return new Mark(poin, Imc.id);
        }
        public Mark checkAnsCon(conversation Con, List<option> lAns)
        {
            List<Mark> tem = checkAnsMC(Con.questions, lAns);
            float poin = 0;
            foreach (Mark k in tem)
            {
                poin += k.mark;
            }
            return new Mark(poin, Con.id);
        }

        public List<MulChoice> randomMC(int count)
        {
            int[] lId = this.lId();
            List<MulChoice> MC = new List<MulChoice>();
            MulChoice[] a = this.listMulChoice.ToArray();
            int i = 0;
            int j = 0;
            while (j < count && i < a.Length)
            {
                if (inArray(a[i].id))
                {
                    i++;
                }
                else
                {
                    MC.Add(a[i]);
                    i++;
                    j++;
                }
            }
            return MC;
        }
        public imcomplete randomImc(int level)
        {
            int[] lId = this.lId();
            int i = 0;
            int j = 0;
            foreach (imcomplete k in this.listImcomplete)
            {
                if (k.level == level && !inArray(k.id))
                {
                    return k;
                }
            }
            return null;
        }
        public conversation randomCon(int level)
        {
            int[] lId = this.lId();
            int i = 0;
            int j = 0;
            foreach (conversation k in this.listConversation)
            {
                if (k.level == level && !inArray(k.id))
                {
                    return k;
                }
            }
            return null;
        }
        public void proTrainingMC()
        {
            List<MulChoice> tMc = new List<MulChoice>();
            List<Mark> tem = new List<Mark>();
            List<option> ansOpt = new List<option>();
            int num = -1;
            viewTr.viewTittle("TRAINING",true);
            try
            {
                num = int.Parse(viewTr.inputString("SO LUONG CAU HOI"));
            }catch(FormatException e) { }
            tMc = this.randomMC(num);
            if (tMc == null)
            {
                viewTr.viewTittle("HAVE NO QUESTION", false); return;
            }
            option t;
            foreach (MulChoice k in tMc)
            {
                viewTr.viewMC(k);
                while (true)
                {
                    t = this.getOption(k.options, viewTr.inputString("YOUR ANSWER"));
                    if (t == null)
                    {
                        viewTr.errorMsg("INPUT FAIL");
                        if (viewTr.continueMsg() == 1) continue;
                        else return;
                    }
                    else
                    {
                        ansOpt.Add(t);
                        break;
                    }
                }
            }
            viewTr.viewTittle("TRAINING", true);
            tem = this.checkAnsMC(tMc, ansOpt);
            foreach (Mark k in tem)
            {
                this.user.addMark(k);
            }
            viewTr.viewMC(tMc, ansOpt);
        }
        public void proTrainingImc()
        {
            imcomplete tMc;
            Mark tem;
            List<string> Ans = new List<string>();
            List<option> ansOpt = new List<option>();
            int lev = -1;
            viewTr.viewTittle("TRAINING", true);
            try
            {
                lev = int.Parse(viewTr.inputString("NHAP LEVEL"));
            }
            catch (FormatException e) { viewTr.errorMsg("INPUT FAIL!"); return; }
            tMc = this.randomImc(lev);
            if (tMc == null)
            {
                viewTr.viewTittle("HAVE NO QUESTION",false); return;
            }
            option t;
            this.viewTr.viewParagram(tMc.paragraph);
            foreach (MulChoice k in tMc.questions)
            {
                viewTr.viewMC(k);
                while (true)
                {
                    t = this.getOption(k.options, viewTr.inputString("YOUR ANSWER"));
                    if (t == null)
                    {
                        viewTr.errorMsg("INPUT FAIL");
                        if (viewTr.continueMsg() == 1) continue;
                        else return;
                    }
                    else
                    {
                        ansOpt.Add(t);
                        break;
                    }
                }
            }
            viewTr.viewTittle("TRAINING", true);
            tem = this.checkAnsImc(tMc, ansOpt);
            this.user.addMark(tem);
            viewTr.viewMQ(tMc, ansOpt);
        }
        public void proTrainingCon()
        {
            conversation tMc;
            Mark tem;
            List<string> Ans = new List<string>();
            List<option> ansOpt = new List<option>();
            int lev = -1;
            viewTr.viewTittle("TRAINING", true);
            try
            {
                lev = int.Parse(viewTr.inputString("NHAP LEVEL"));
            }
            catch (FormatException e) { viewTr.errorMsg("INPUT FAIL!"); return; }
            tMc = this.randomCon(lev);
            if (tMc == null)
            {
                viewTr.viewTittle("HAVE NO QUESTION", false); return;
            }
            option t;
            this.viewTr.viewParagram(tMc.paragraph);
            foreach (MulChoice k in tMc.questions)
            {
                viewTr.viewMC(k);
                while (true)
                {
                    t = this.getOption(k.options, viewTr.inputString("YOUR ANSWER"));
                    if (t == null)
                    {
                        viewTr.errorMsg("INPUT FAIL");
                        if (viewTr.continueMsg() == 1) continue;
                        else return;
                    }
                    else
                    {
                        ansOpt.Add(t);
                        break;
                    }
                }
            }
            viewTr.viewTittle("TRAINING", true);
            tem = this.checkAnsCon(tMc, ansOpt);
            this.user.addMark(tem);
            viewTr.viewMQ(tMc, ansOpt);
        }
        public int proMenu()
        {
            return viewTr.Menu();
        }
    }
}
