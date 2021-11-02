using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTest
{
    class controlProgram
    {
        private User user;
        private ControlUser ctrUser;
        private ViewMenu vMenu;
        private ViewStart vStart;
        private ControlStatistics ctrStatistics;
        private ControlQuestion ctrQuestion;
        private ControlTraining ctrTraining;
        public controlProgram()
        {
            ctrUser = new ControlUser();
            vStart = new ViewStart();
            vMenu = new ViewMenu();
            ctrQuestion = new ControlQuestion();
        }
        public void proUser()
        {
            User u; int id;
            while (true)
            {
                switch (ctrUser.proMenu())
                {
                    case 1:
                        ctrUser.proShowListUser();
                        break;
                    case 2:
                        ctrUser.proSearchUser();
                        break;
                    case 3:
                        u = ctrUser.proSignUp();
                        if (u != null)
                        {
                            ctrUser.proAddUser(u);
                        }
                        break;
                    case 4:
                        ctrUser.proDeleteUser();
                        break;
                    case 5:
                        ctrUser.proUpdateUser();
                        break;
                    case 0:
                        return;
                }
            }
        }
        public void proQuestion()
        {
            while (true)
            {
                switch (ctrQuestion.proMenu())
                {
                    case 1:
                        ctrQuestion.proShowList();
                        vMenu.Msg("BACK");
                        break;
                    case 2:
                        ctrQuestion.proSearchContent();
                        vMenu.Msg("BACK");
                        break;
                    case 3:
                        ctrQuestion.proSearchType();
                        vMenu.Msg("BACK");
                        break;
                    case 4:
                        ctrQuestion.proSearchLevel();
                        vMenu.Msg("BACK");
                        break;
                    case 0:
                        return;
                }
            }
        }
        public void proTraining()
        {
            while (true)
            {
                switch (ctrTraining.proMenu())
                {
                    case 1:
                        ctrTraining.proTrainingMC();
                        vMenu.Msg("BACK");
                        break;
                    case 2:
                        ctrTraining.proTrainingImc();
                        vMenu.Msg("BACK");
                        break;
                    case 3:
                        ctrTraining.proTrainingCon();
                        vMenu.Msg("BACK");
                        break;
                    case 0:
                        return;
                }
            }
        }
        public void proMenu()
        {
            ctrStatistics = new ControlStatistics(this.user.marks);
            ctrTraining = new ControlTraining(this.user, ctrQuestion.listMulChoice, ctrQuestion.listImcomplete, ctrQuestion.listConversation);
            User u;
            while (true)
            {
                switch (vMenu.Menu())
                {
                    case 1:
                        this.proUser();
                        break;
                    case 2:
                        this.proQuestion();
                        break;
                    case 3:
                        this.proTraining();
                        break;
                    case 4:
                        ctrStatistics.showResult();
                        break;
                    case 0:
                        return;
                }
            }
        }
        public void proStart()
        {
            User u;
            while (true)
            {
                switch (vStart.Menu())
                {
                    case 1:
                        u=ctrUser.proSignIn();
                        if (u != null) {
                            this.user = u;
                            this.proMenu();
                        }
                        break;
                    case 2:
                        u = ctrUser.proSignUp();
                        if (u != null)
                        {
                            this.user = u;
                            ctrUser.proAddUser(user);
                            this.proMenu(); ;
                        }
                        break;
                    case 0:
                        vStart.viewExit();
                        ctrUser.prosaveUser();
                        return;
                }
            }
        }
        static void Main(string[] args)
        {
            controlProgram con = new controlProgram();
            con.proStart();
            //ControlQuestion q = new ControlQuestion();
            
        }
    }
}
