using System;
using System.IO;
using System.Windows.Forms;


namespace E.Z_Subtitles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //INI R/W
        private void ini_write()
        {
            string f_name = "";
            string match_style = "";

            if (rvideo.Checked == true) f_name = "video";
            else if (rsmi.Checked == true) f_name = "smi";
            if (rcopy.Checked == true) match_style = "copy";
            else if (rmove.Checked == true) match_style = "move";

            IniFile ini = new IniFile();
            ini["Settings"]["f_name"] = f_name;
            ini["Settings"]["match_style"] = match_style;
            ini.Save("Settings.ini");
        }

        private void ini_read()
        {
            IniFile ini = new IniFile();
            if (File.Exists("Settings.ini")) ini.Load("Settings.ini");
            string f_name = ini["Settings"]["f_name"].ToString();
            string match_style = ini["Settings"]["match_style"].ToString();

            if (f_name == "video") rvideo.Checked = true;
            else if (f_name == "smi") rsmi.Checked = true;
            if (match_style == "copy") rcopy.Checked = true;
            else if (match_style == "move") rmove.Checked = true;
        }

        //폼로드 이벤트 추가
        private void Form1_Load(object sender, EventArgs e)
        {
            ini_read(); //Get InI Settings
            listView1.Alignment = ListViewAlignment.Default;
            listView2.Alignment = ListViewAlignment.Default;

            listView1.AllowDrop = true;
            listView2.AllowDrop = true;



            listView1.DragDrop += ListView1_DragDrop;
            listView1.DragEnter += ListView1_DragEnter;

            listView2.DragDrop += ListView2_DragDrop;
            listView2.DragEnter += ListView2_DragEnter;
        }



        //드래그&드롭 파일 추가
        private void ListView1_DragEnter(object sender, DragEventArgs e)
        {
            //마우스 포인트 변경
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; //Copy 포인터로 변경
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void ListView2_DragEnter(object sender, DragEventArgs e)
        {
            //마우스 포인트 변경
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; //Copy 포인터로 변경
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }



        private void ListView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop); //데이터를 가져오고 string[] 으로 변환
                SubTitle.file_add(files, listView1, false);
            }

        }



        private void ListView2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop); //데이터를 가져오고 string[] 으로 변환
                SubTitle.file_add(files, listView2, true);

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
        }

        //프로그램 Core - Match 버튼 구현
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("영상 파일을 추가해 주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (listView1.Items.Count != listView2.Items.Count)
            {

                MessageBox.Show("영상 파일과 자막파일의 갯수가 동일하지 않아" + Environment.NewLine + "작업을 진행할 수 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("작업을 진행하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {

                try
                {
                    SubTitle.match(listView1, listView2, rvideo.Checked, rcopy.Checked);
                    MessageBox.Show("작업이 완료되었습니다 :)", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }



        }

        //UP&DOWN 버튼 구현
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                if (lvi.Index > 0) //인덱스가 0보다 클경우
                {
                    //리스트뷰는 삽입 후 인덱스를 자동 관리함.
                    int index = lvi.Index - 1;
                    listView1.Items.RemoveAt(lvi.Index); //현재열 아이템 제거
                    listView1.Items.Insert(index, lvi); //그 이전열에 끼워넣기
                    //어렵게 생각할거 없이 ~번에 그냥 넣는다고 생각함.
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                if (lvi.Index < listView1.Items.Count - 1)
                {

                    int index = lvi.Index + 1;
                    listView1.Items.RemoveAt(lvi.Index);
                    listView1.Items.Insert(index, lvi);
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView2.SelectedItems)
            {
                if (lvi.Index > 0) //인덱스가 0보다 클경우
                {
                    //리스트뷰는 삽입 후 인덱스를 자동 관리함.
                    int index = lvi.Index - 1;
                    listView2.Items.RemoveAt(lvi.Index); //현재열 아이템 제거
                    listView2.Items.Insert(index, lvi); //그 이전열에 끼워넣기
                    //어렵게 생각할거 없이 ~번에 그냥 넣는다고 생각함.
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView2.SelectedItems)
            {
                if (lvi.Index < listView2.Items.Count - 1)
                {

                    int index = lvi.Index + 1;
                    listView2.Items.RemoveAt(lvi.Index);
                    listView2.Items.Insert(index, lvi);

                }
            }
        }

        //체크박스 누를시 ini 자동 작성
        private void Rvideo_CheckedChanged(object sender, EventArgs e)
        {
            ini_write();
        }

        private void Rsmi_CheckedChanged(object sender, EventArgs e)
        {
            ini_write();
        }

        private void Rcopy_CheckedChanged(object sender, EventArgs e)
        {
            ini_write();
        }

        private void Rmove_CheckedChanged(object sender, EventArgs e)
        {
            ini_write();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://pgh268400.tistory.com/189");
        }

        //컬럼 클릭시 재정렬
        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            if (listView1.Items.Count > 0)
            {
                string[] files = new string[listView1.Items.Count];
                for (int i = 0; i < listView1.Items.Count; i++)
                {

                    files[i] = listView1.Items[i].SubItems[2].Text;

                }
                Array.Sort(files, NatSort.CompareNatural);
                listView1.Items.Clear();
                foreach (string file_path in files)
                {

                    string filename = Path.GetFileNameWithoutExtension(file_path);
                    string path = Path.GetExtension(file_path);
                    ListViewItem item = new ListViewItem(filename);
                    item.SubItems.Add(path);
                    item.SubItems.Add(file_path);
                    listView1.Items.Add(item);

                }


            }


        }

        private void ListView2_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView2.Items.Count > 0)
            {
                string[] files = new string[listView2.Items.Count];
                for (int i = 0; i < listView2.Items.Count; i++)
                {

                    files[i] = listView2.Items[i].SubItems[2].Text;

                }
                Array.Sort(files, NatSort.CompareNatural);
                listView2.Items.Clear();
                foreach (string file_path in files)
                {

                    string filename = Path.GetFileNameWithoutExtension(file_path);
                    string path = Path.GetExtension(file_path);
                    ListViewItem item = new ListViewItem(filename);
                    item.SubItems.Add(path);
                    item.SubItems.Add(file_path);
                    listView2.Items.Add(item);

                }
            }
        }

        //더블클릭시 아이템 삭제
        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                    listView1.Items.Remove(item);
            }
        }

        private void ListView2_DoubleClick(object sender, EventArgs e)
        {

            if (listView2.SelectedItems != null)
            {
                foreach (ListViewItem item in listView2.SelectedItems)
                    listView2.Items.Remove(item);
            }

        }

    }
}
