using System.Collections.Generic;

namespace EnglishTest
{
    enum DanhMuc
    {
        DanhTu, DongTu, TinhTu, TrangTu, GioiTu, DanhMucKhac
    }
    abstract class Question
    {
        public DanhMuc danhMuc { get; }
        public int level { get; }
        public int id { get; }
        public float mark { get; }
        public Question() { }
        public Question(int id, int level, DanhMuc danhMuc, float mark)
        {
            this.level = level;
            this.id = id;
            this.mark = mark;
            this.danhMuc = danhMuc;
        }
    }
    class option
    {
        public string content { get; }
        public string explain { get; }
        public option(string content, string explain)
        {
            this.content = content;
            this.explain = explain;
        }
    }
    class MulChoice : Question
    {
        public string content { get; }
        public List<option> options { get; }
        public option answer { get; }
        public MulChoice(float mark) : base(-1, -1, DanhMuc.DanhMucKhac, mark)
        {
        }

        public MulChoice(int id, int level, DanhMuc danhMuc, float mark, string content, List<option> options, option answer) : base(id, level, danhMuc, mark)
        {
            this.content = content;
            this.options = options;
            this.answer = answer;
        }
    }
    class MulQuestion : Question
    {
        public string[] paragraph { get; }
        public List<MulChoice> questions { get; }
        public MulQuestion()
        {
            questions = new List<MulChoice>();
        }
        public MulQuestion(int id, int level, DanhMuc danhMuc, float mark, string[] Pharagraph, List<MulChoice> Questions) : base(id, level, danhMuc, mark)
        {
            this.paragraph = Pharagraph;
            this.questions = Questions;
        }
    }
    class conversation : MulQuestion
    {
        public conversation(int id, int level, DanhMuc danhMuc, float mark, string[] Paragraph, List<MulChoice> Questions) : base(id, level, danhMuc, mark, Paragraph, Questions)
        {
        }
    }
    class imcomplete : MulQuestion
    {
        public imcomplete(int id, int level, DanhMuc danhMuc, float mark, string[] Paragraph, List<MulChoice> Questions) : base(id, level, danhMuc, mark, Paragraph, Questions)
        {
        }
    }
}
