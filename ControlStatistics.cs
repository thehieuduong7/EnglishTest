using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTest
{
    class ControlStatistics
    {
        public ControlStatistics(List<Mark> list)
        {
            this.list = list;
            this.inMonth = getListInMonth();
            viewSt = new viewStatistics();
        }
        private List<Mark> inMonth;
        private List<Mark> list;
        private viewStatistics viewSt;
        private float Average()
        {
            float sum = 0;
            foreach (Mark p in inMonth)
            {
                sum += p.mark;
            }
            if (inMonth.Count != 0)
            {
                float avg = sum / inMonth.Count;
                return avg;
            }
            else
            {
                return 0;
            }
        }
        private List<Mark> getListInMonth()
        {
            List<Mark> inM = new List<Mark>();
            foreach (Mark p in list)
            {
                if (p.date.Month == DateTime.Now.Month && p.date.Year == DateTime.Now.Year)
                {
                    inM.Add(p);
                }
            }
            return inM;
        }
        public void showResult()
        {
            this.inMonth = getListInMonth();
            viewSt.showResult(inMonth, this.Average());
            viewSt.Msg("BACK");
        }
    }
}
