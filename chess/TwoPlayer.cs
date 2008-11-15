using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace chess
{
    public partial class TwoPlayer : Form
    {
        private OleDbConnection conn;
        private OleDbDataAdapter oda;
        private DataSet ods=new DataSet();
        private string bckcolor;
        private PictureBox older,newer;
        private PictureBox bking, wking;
        string[] validMoves;
        private string n1, o1;
        public void calculaten1o1(PictureBox n, PictureBox o)
        {
            n1 = "Null";
            if (n!=null && n.Image != null)
            {
                n1 = n.ImageLocation.Trim("Objects/.jpg".ToCharArray());
                n1 = n1.Remove(n1.Length - 1);
            }
            o1 = "Null";
            if (o.Image != null)
            {
                o1 = o.ImageLocation.Trim("Objects/.jpg".ToCharArray());
                o1 = o1.Remove(o1.Length - 1);
            }
        }
        public int TempMove(PictureBox n, PictureBox o,int color)
        {
            int result=color;
            if (!IsSameColor(n, o))
            {
                calculaten1o1(n, o);
                int gotiColor = int.Parse(o1.Remove(0, o1.Length - 1));
                //(ods.Tables[0].Rows[int.Parse(newer.Name.Remove(0, newer.Name.Length - 1)) - 1][newer.Name.Remove(newer.Name.Length - 1, 1)]) = o1;
                //(ods.Tables[0].Rows[int.Parse(older.Name.Remove(0, older.Name.Length - 1)) - 1][older.Name.Remove(older.Name.Length - 1, 1)]) = "Null";
                bckcolor = (n.BackColor == Color.Black) ? "0" : "1";
                string ntemp = n1;
                n.Load("Objects/" + o1 + bckcolor + ".jpg");
                o.Image = null;
                if (o.ImageLocation.Contains("King0"))
                    bking = n;
                if (o.ImageLocation.Contains("King1"))
                    wking = n;
                //MessageBox.Show(gotiColor.ToString());
                result = checksTheKing();
                //if (checkResult == gotiColor)
                //{
                    //SelectBox(older, newer);
                    calculaten1o1(n, o);
                    //(ods.Tables[0].Rows[int.Parse(newer.Name.Remove(0, newer.Name.Length - 1)) - 1][newer.Name.Remove(newer.Name.Length - 1, 1)]) = ntemp;
                    //(ods.Tables[0].Rows[int.Parse(older.Name.Remove(0, older.Name.Length - 1)) - 1][older.Name.Remove(older.Name.Length - 1, 1)]) = n1;
                    if (n.ImageLocation.Contains("King0"))
                        bking = o;
                    if (n.ImageLocation.Contains("King1"))
                        wking = o;
                    //bckcolor = (older.BackColor == Color.Black) ? "0" : "1";
                    string obckcolor = (o.BackColor == Color.Black) ? "0" : "1";
                    o.Load("Objects/" + n1 + obckcolor + ".jpg");
                    if (ntemp == "Null")
                        n.Image = null;
                    else
                        n.Load("Objects/" + ntemp + bckcolor + ".jpg");
                //}
            }
            return result;
        }
       
        public void checkTheCheckMate(int colorOfKing)
        {
            PictureBox king = colorOfKing == 1 ? wking : bking;
            bool gameOver=true;
            foreach (PictureBox p in this.Controls)
            {
                if (IsSameColor(p, king))
                {
                    FindAllValidMoves(p);
                    foreach (string box in validMoves)
                    {
                        //MessageBox.Show(box);
                        if (box!=null && TempMove(((PictureBox)this.Controls[box]),p,colorOfKing) != colorOfKing)
                            gameOver = false;
                    }
                }
            }
            if (gameOver)
            {
                MessageBox.Show("Check Mate!!!");
                
                //Application.Exit();
            }
        }
        public int checksTheKing()
        {
            foreach (PictureBox p in this.Controls)
            {
                if(p.Image!=null)
                {
                    FindAllValidMoves(p);
                    foreach (string box in validMoves)
                    {
                        if (!IsSameColor(wking, p) && wking.Name == box)
                        {
                            //MessageBox.Show(p.Name + " checks " + wking.Name);
                            return 1;
                        }
                        if (!IsSameColor(bking, p) && bking.Name == box)
                        {
                            //MessageBox.Show(p.Name + " checks " + bking.Name);
                            return 0;
                        }
                    }
                }
            }
            return 10;
        }
        public void SelectBox(PictureBox n, PictureBox o)
        {
            n.Left += 2;
            n.Top += 2;
            n.Width -= 4;
            n.Height -= 4;
            o.Left -= 2;
            o.Top -= 2;
            o.Height += 4;
            o.Width += 4;
        }
        public string FindOppColor(PictureBox o)
        {
            calculaten1o1(null,o);
            foreach (DataRow r in ods.Tables[0].Rows)
            {
                foreach (DataColumn c in ods.Tables[0].Columns)
                {
                    if (c.Caption != "ID" && r[c].ToString() != "Null" && r[c].ToString().Remove(0, r[c].ToString().Length - 1) != o1.Remove(0, o1.Length - 1))
                        return (c.Caption+r["ID"].ToString());
                }
            }
            return "Null";
        }
        public void FindAllValidMoves(PictureBox o)
        {
            calculaten1o1(null,o);
            //bool result = true;
            bool tocontinue = false;
            char col = Convert.ToChar(o.Name.Remove(1, 1)), less, gr;
            int ro = int.Parse(o.Name.Remove(0, 1));
            int gotiColor = int.Parse(o1.Remove(0, o1.Length - 1)), startro, toAdd = 1,i=0;
            switch (o1.Remove(o1.Length - 1, 1))
            {
                case "Ghoda":
                    validMoves = new string[8];
                    foreach (PictureBox n in this.Controls)
                    {
                        if (Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) > 2 || (Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1)))) > 2)
                            continue;
                        if (Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) + (Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1)))) != 3)
                            continue;
                        validMoves[i] = n.Name;
                        i++;
                    }
                    break;
                case "Haathi":
                    validMoves = new string[14];
                    foreach (PictureBox n in this.Controls)
                    {
                        tocontinue = false;
                        if (!(Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) == 0) && !(Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))) == 0))
                            continue;
                        if (Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) == 0)
                        {
                            less = col < Convert.ToChar(n.Name.Remove(1, 1)) ? col : Convert.ToChar(n.Name.Remove(1, 1));
                            gr = col < Convert.ToChar(n.Name.Remove(1, 1)) ? Convert.ToChar(n.Name.Remove(1, 1)) : col;
                            less++;
                            while (less < gr)
                            {
                                if (((PictureBox)this.Controls[less.ToString() + ro.ToString()]).Image != null)
                                    tocontinue=true;
                                less++;
                            }
                            if (tocontinue)
                                continue;
                        }
                        if (Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))) == 0)
                        {
                            int lessi = ro < int.Parse(n.Name.Remove(0, 1)) ? ro : int.Parse(n.Name.Remove(0, 1));
                            int gri = ro < int.Parse(n.Name.Remove(0, 1)) ? int.Parse(n.Name.Remove(0, 1)) : ro;
                            lessi++;
                            while (lessi < gri)
                            {
                                if (((PictureBox)this.Controls[col.ToString() + lessi.ToString()]).Image != null)
                                    tocontinue=true;
                                lessi++;
                            }
                            if (tocontinue)
                                continue;
                        }
                        validMoves[i] = n.Name;
                        i++;
                    }
                    break;
                case "Pyada":
                    validMoves = new string[4];
                    foreach (PictureBox n in this.Controls)
                    {
                        //peeche nahi jaa sakta hai...
                        if ((gotiColor == 0 && ro > int.Parse(n.Name.Remove(0, 1))) || (gotiColor == 1 && ro < int.Parse(n.Name.Remove(0, 1))))
                            continue;
                        //ek baar mein ek hi square aage jaa sakta hai...
                        if (Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) != 1 || Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))) > 1)
                        {
                            //Pehli baar mein 2 kadam chal sakta hai....
                            if ((Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))) == 0))
                            {
                                if (n.Image==null&&(Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) == 2) && ((gotiColor == 0 && ro == 2) || (gotiColor == 1 && ro == 7)))
                                {
                                    validMoves[i] = n.Name;
                                    i++;
                                }
                            }
                            continue;
                        }
                        //Teda sirf kha sakta hai chal nahi sakta hai....
                        if (Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))) == 1 && n.Image == null)
                            continue;
                        if (Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))) == 0 && n.Image != null)
                            continue;
                        validMoves[i] = n.Name;
                        i++;
                    }
                    break;
                case "King":
                    validMoves = new string[9];
                    foreach (PictureBox n in this.Controls)
                    {
                        if (Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) > 1 || Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))) > 1)
                            continue;
                        validMoves[i] = n.Name;
                        i++;
                    }

                    break;
                case "Vazeer":
                    validMoves=new string[28];
                    foreach (PictureBox n in this.Controls)
                    {
                        tocontinue = false;
                        //agar teda nahi chal raha hai to copy from haathi....
                        if (Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) != Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))))
                        {
                            if (!(Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) == 0) && !(Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))) == 0))
                                continue;
                            if (Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) == 0)
                            {
                                less = col < Convert.ToChar(n.Name.Remove(1, 1)) ? col : Convert.ToChar(n.Name.Remove(1, 1));
                                gr = col < Convert.ToChar(n.Name.Remove(1, 1)) ? Convert.ToChar(n.Name.Remove(1, 1)) : col;
                                less++;
                                while (less < gr)
                                {
                                    if (((PictureBox)this.Controls[less.ToString() + ro.ToString()]).Image != null)
                                        tocontinue=true;
                                    less++;
                                }
                                if (tocontinue)
                                    continue;
                            }
                            if (Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))) == 0)
                            {
                                int lessi = ro < int.Parse(n.Name.Remove(0, 1)) ? ro : int.Parse(n.Name.Remove(0, 1));
                                int gri = ro < int.Parse(n.Name.Remove(0, 1)) ? int.Parse(n.Name.Remove(0, 1)) : ro;
                                lessi++;
                                while (lessi < gri)
                                {
                                    if (((PictureBox)this.Controls[col.ToString() + lessi.ToString()]).Image != null)
                                        tocontinue = true ;
                                    lessi++;
                                }
                                if (tocontinue)
                                    continue;
                            }
                        }
                        //agar seedha nahi chal raha hai to copy from Camel......
                        if (!(Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) == 0) && !(Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))) == 0))
                        {
                            if (Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) != Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))))
                                continue;
                            toAdd = 1;
                            if (col < Convert.ToChar(n.Name.Remove(1, 1)))
                            {
                                less = col;
                                gr = Convert.ToChar(n.Name.Remove(1, 1));
                                startro = ro;
                                if (ro > int.Parse(n.Name.Remove(0, 1)))
                                    toAdd = -1;
                            }
                            else
                            {
                                gr = col;
                                less = Convert.ToChar(n.Name.Remove(1, 1));
                                startro = int.Parse(n.Name.Remove(0, 1));
                                if (ro < int.Parse(n.Name.Remove(0, 1)))
                                    toAdd = -1;
                            }
                            less++;
                            startro += toAdd;
                            while (less < gr)
                            {
                                if (((PictureBox)this.Controls[less.ToString() + startro.ToString()]).Image != null)
                                    tocontinue=true;
                                less++;
                                startro += toAdd;
                            }
                            if (tocontinue)
                                continue;
                        }
                        validMoves[i] = n.Name;
                        i++;
                    }
                    break;
                case "Camel":
                    validMoves = new string[14];
                    foreach (PictureBox n in this.Controls)
                    {
                        tocontinue = false;
                        if (Math.Abs((int.Parse(n.Name.Remove(0, 1)) - ro)) != Math.Abs(col - Convert.ToChar(n.Name.Remove(1, 1))))
                            continue;
                        toAdd = 1;
                        if (col < Convert.ToChar(n.Name.Remove(1, 1)))
                        {
                            less = col;
                            gr = Convert.ToChar(n.Name.Remove(1, 1));
                            startro = ro;
                            if (ro > int.Parse(n.Name.Remove(0, 1)))
                                toAdd = -1;
                        }
                        else
                        {
                            gr = col;
                            less = Convert.ToChar(n.Name.Remove(1, 1));
                            startro = int.Parse(n.Name.Remove(0, 1));
                            if (ro < int.Parse(n.Name.Remove(0, 1)))
                                toAdd = -1;
                        }
                        less++;
                        startro += toAdd;
                        while (less < gr)
                        {
                            if (((PictureBox)this.Controls[less.ToString() + startro.ToString()]).Image != null)
                                tocontinue=true;
                            less++;
                            startro += toAdd;
                        }
                        if (tocontinue)
                            continue;
                        validMoves[i] = n.Name;
                        i++;
                    }
                    break;
                default: break;
            }
        }
        public bool IsValidMove(PictureBox n, PictureBox o)
        {
            //calculaten1o1(n,o);
            bool result=false;
            FindAllValidMoves(o);
            foreach (string box in validMoves)
            {
                if (n.Name == box)
                    return true;
            }
            
            //MessageBox.Show("false return");
            return result;
        }
        public bool IsSameColor(PictureBox n, PictureBox o)
        {
            calculaten1o1(n,o);
            if(n1.Remove(0,n1.Length-1)==o1.Remove(0,o1.Length-1)&&n1!="Null") 
                return true;
            else
                return false;
        }

        public TwoPlayer()
        {
            this.FormClosed += new FormClosedEventHandler(TwoPlayer_FormClosed);
            InitializeComponent();
            conn=new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+Application.StartupPath+"/chess.mdb");
            conn.Open();
            oda = new OleDbDataAdapter("Select * from Table1", conn);
            oda.Fill(ods, "Table1");
            this.Click += new EventHandler(Form1_Click);
            bking = E1; wking = E8;
            older = E7;
            E7.Left += 2;
            E7.Top += 2;
            E7.Width -= 4;
            E7.Height-=4;
        }

        void TwoPlayer_FormClosed(object sender, FormClosedEventArgs e)
        {
            //throw new NotImplementedException();
            Application.Exit();
        }

        void Form1_Click(object sender, EventArgs e)
        {
            char s = 'A';
            int x=((((MouseEventArgs)e).X)/90)+1;
            for (int i = 0; i < x-1; i++)
                s++;
            int y = ((((MouseEventArgs)e).Y) / 90) + 1;
            newer=((PictureBox)this.Controls[s.ToString() + y.ToString()]);
            if (IsValidMove(newer, older) && !IsSameColor(newer, older))
            {
                calculaten1o1(newer, older);
                int gotiColor = int.Parse(o1.Remove(0, o1.Length - 1));
                (ods.Tables[0].Rows[int.Parse(newer.Name.Remove(0, newer.Name.Length - 1)) - 1][newer.Name.Remove(newer.Name.Length - 1, 1)]) = o1;
                (ods.Tables[0].Rows[int.Parse(older.Name.Remove(0, older.Name.Length - 1)) - 1][older.Name.Remove(older.Name.Length - 1, 1)]) = "Null";
                bckcolor = (newer.BackColor == Color.Black) ? "0" : "1";
                string ntemp = n1;
                newer.Load("Objects/" + o1 + bckcolor+".jpg");
                older.Image = null;
                if (older.ImageLocation.Contains("King0"))
                    bking = newer;
                if (older.ImageLocation.Contains("King1"))
                    wking = newer;
                //MessageBox.Show(gotiColor.ToString());
                int checkResult=checksTheKing();
                if (checkResult == gotiColor)
                {
                    //SelectBox(older, newer);
                    calculaten1o1(newer,older);
                    (ods.Tables[0].Rows[int.Parse(newer.Name.Remove(0, newer.Name.Length - 1)) - 1][newer.Name.Remove(newer.Name.Length - 1, 1)]) = ntemp;
                    (ods.Tables[0].Rows[int.Parse(older.Name.Remove(0, older.Name.Length - 1)) - 1][older.Name.Remove(older.Name.Length - 1, 1)]) = n1;
                    if (newer.ImageLocation.Contains("King0"))
                        bking = older;
                    if (newer.ImageLocation.Contains("King1"))
                        wking = older;
                    //bckcolor = (older.BackColor == Color.Black) ? "0" : "1";
                    string obckcolor = (older.BackColor == Color.Black) ? "0" : "1";
                    older.Load("Objects/" + n1 + obckcolor + ".jpg");
                    if (ntemp == "Null")
                        newer.Image = null;
                    else
                        newer.Load("Objects/" + ntemp + bckcolor + ".jpg");
                }
                else
                {
                    SelectBox(newer, older);
                    older = newer;                    
                    newer = ((PictureBox)this.Controls[FindOppColor(older)]);
                    SelectBox(newer, older);
                    older = newer;
                    if (checkResult != 10)
                    {
                        checkTheCheckMate(checkResult);
                    }
                }
            }
            if (IsSameColor(newer, older))
            {
                SelectBox(newer, older);
                older = newer;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gc = e.Graphics;
            Pen redpen = new Pen(Color.Red, 5);
            gc.DrawRectangle(redpen, older.Left, older.Top, older.Width, older.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i = 1;
            char s = 'A';
            while (s != 'I')
            {
                for (i = 1; i < 9; i++)
                {
                    if ((ods.Tables[0].Rows[i - 1][s.ToString()]).ToString() != "Null")
                    {
                        bckcolor= (this.Controls[s.ToString() + i.ToString()].BackColor == Color.Black)? "0": "1";
                        ((PictureBox)this.Controls[s.ToString() + i.ToString()]).Load("Objects/" + ods.Tables[0].Rows[i - 1][s.ToString()]+bckcolor+".jpg");
                    }
                }
                s++;
            }
        }
    }
}