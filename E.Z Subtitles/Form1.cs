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
        private void iniWrite()
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

        private void iniRead()
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
            iniRead(); //Get InI Settings
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


        private Boolean Subtitle_Check_Ext(string ext) //자막 확장자 검사
        {
            string[] sub_ext = { ".psb", ".srt", ".ssa", ".ass", ".sub", ".sami", ".smi", ".smil", ".usf", ".vtt" };
            foreach(string c in sub_ext)
            {
                if (ext.ToLower() == c) return true; //자막 True
            }
            return false; //자막 False
        }

        //드래그 드롭 파일 추가 Core
        private void File_Add(string[] files, ListView lv, Boolean isSMI)
        {

            Array.Sort(files, NatSort.CompareNatural); //Natural Sort
            if (files.Length >= 1) //1개 이상이면 
            {
                foreach (string file_path in files)
                {
                    bool directory_check = File.GetAttributes(file_path).HasFlag(FileAttributes.Directory);

                    if (directory_check == false) //파일만 추가함.
                    {
                        string filename = Path.GetFileNameWithoutExtension(file_path);
                        string path = Path.GetExtension(file_path);

                        if (isSMI == true & Subtitle_Check_Ext(path) == true)
                        {
                            ListViewItem item = new ListViewItem(filename);
                            item.SubItems.Add(path);
                            item.SubItems.Add(file_path);
                            lv.Items.Add(item);
                        }else if (isSMI == false & Subtitle_Check_Ext(path) == false)
                        {
                            ListViewItem item = new ListViewItem(filename);
                            item.SubItems.Add(path);
                            item.SubItems.Add(file_path);
                            lv.Items.Add(item);
                        }
                    }
                        
                    else
                    {
                        string[] directory_files = Directory.GetFiles(file_path); //디렉토리 파일 로드
                        Array.Sort(directory_files, NatSort.CompareNatural); //Natural Sort
                        foreach (string d_file_path in directory_files)
                        {

                            string filename = Path.GetFileNameWithoutExtension(d_file_path);
                            string path = Path.GetExtension(d_file_path);
                            if (isSMI == true & Subtitle_Check_Ext(path) == true)
                            {
                                ListViewItem item = new ListViewItem(filename);
                                item.SubItems.Add(path);
                                item.SubItems.Add(d_file_path);
                                lv.Items.Add(item);
                            }
                            else if (isSMI == false & Subtitle_Check_Ext(path) == false)
                            {
                                ListViewItem item = new ListViewItem(filename);
                                item.SubItems.Add(path);
                                item.SubItems.Add(file_path);
                                lv.Items.Add(item);
                            }
                        }

                    }
                }
                if (lv.Items.Count > 0)
                {
                    lv.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent); //Column Auto Width
                }
            }
        }

        private void ListView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop); //데이터를 가져오고 string[] 으로 변환
                File_Add(files, listView1, false);
            }

        }



        private void ListView2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop); //데이터를 가져오고 string[] 으로 변환
                File_Add(files, listView2, true);

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
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        if (rvideo.Checked == true) //따라갈 파일 이름 : 영상
                        {
                            string video_path = listView1.Items[i].SubItems[2].Text;
                            string smi_path = listView2.Items[i].SubItems[2].Text;
                            //비디오 파일 경로\비디오파일이름\.자막확장자
                            string copy_path = Path.GetDirectoryName(video_path) + "\\" + Path.GetFileNameWithoutExtension(video_path) + Path.GetExtension(smi_path);
                            //MessageBox.Show(String.Format("{0} {1}", smi_path, copy_path));

                            if (rcopy.Checked == true)
                            {
                                if (smi_path == copy_path) File.Move(smi_path, copy_path); //동일경로면 Move로 옮김
                                else File.Copy(smi_path, copy_path); //자막 복사 모드

                            }
                            else if (rmove.Checked == true) File.Move(smi_path, copy_path); //자막 이동 모드
                        }
                        else if (rsmi.Checked == true) //따라갈 파일 이름 : 자막
                        {
                            string video_path = listView1.Items[i].SubItems[2].Text;
                            string smi_path = listView2.Items[i].SubItems[2].Text;
                            //비디오 파일 경로\자막파일이름\.자막확장자
                            string copy_path = Path.GetDirectoryName(video_path) + "\\" + Path.GetFileNameWithoutExtension(smi_path) + Path.GetExtension(smi_path);
                            //MessageBox.Show(String.Format("{0} {1}", smi_path, copy_path));

                            if (rcopy.Checked == true)
                            {
                                if (smi_path == copy_path) File.Move(smi_path, copy_path); //동일경로면 Move로 옮김
                                else File.Copy(smi_path, copy_path); //자막 복사 모드

                            }
                            else if (rmove.Checked == true) File.Move(smi_path, copy_path); //자막 이동 모드

                            //비디오 파일경로\자막파일이름\.비디오확장자
                            string change_video_path = Path.GetDirectoryName(video_path) + "\\" + Path.GetFileNameWithoutExtension(smi_path) + Path.GetExtension(video_path);
                            File.Move(video_path, change_video_path); //영상파일 이름 -> 자막파일 이름으로 변경
                        }



                    }
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
            iniWrite();
        }

        private void Rsmi_CheckedChanged(object sender, EventArgs e)
        {
            iniWrite();
        }

        private void Rcopy_CheckedChanged(object sender, EventArgs e)
        {
            iniWrite();
        }

        private void Rmove_CheckedChanged(object sender, EventArgs e)
        {
            iniWrite();
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
