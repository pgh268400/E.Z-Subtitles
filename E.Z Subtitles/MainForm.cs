using System;
using System.IO;
using System.Windows.Forms;


namespace E.Z_Subtitles
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            // 창 모니터 정 중앙에 표시되도록 설정
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();
        }

        // INI R/W
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

        private void init_listview()
        {
            // 리스트뷰 정렬 방식을 기본으로 설정
            listview_video.Alignment = ListViewAlignment.Default;
            listview_sub.Alignment = ListViewAlignment.Default;

            // 리스트뷰에 드래그 드롭을 허용으로 설정
            listview_video.AllowDrop = true;
            listview_sub.AllowDrop = true;

            /*
              이벤트 핸들러 등록의 경우 윈폼 디자이너에서 대부분 수행했기에
              InitializeComponent() 함수에 이벤트 핸들러 등록 코드가 대다수 존재한다.
              아래는 일부 수동으로 등록한 이벤트 핸들러.
              수동으로 등록한 기준은, 클릭과 같이 디자이너에서 직관적으로 등록되는 Click 이벤트 같은건
              디자이너에서 등록하고, 드래그&드롭같은 이벤트는
              디자이너에서 해당하는 이벤트 일일히 찾아야 해서 수동으로 등록함.
              (사실 내 맘대로 왔다리 갔다리 하긴 했다 ㅎ)
            */

            // Drag & Drop 이벤트 추가
            listview_video.DragDrop += listview_DragDrop;
            listview_video.DragEnter += listview_DragEnter;

            listview_sub.DragDrop += listview_DragDrop;
            listview_sub.DragEnter += listview_DragEnter;

            // 컬럼 클릭시 재정렬
            listview_video.ColumnClick += listview_ColumnClick;
            listview_sub.ColumnClick += listview_ColumnClick;

            // 더블클릭시 아이템 삭제
            listview_video.DoubleClick += listview_DoubleClick;
            listview_sub.DoubleClick += listview_DoubleClick;
        }

        // 폼로드 이벤트 추가
        private void Form1_Load(object sender, EventArgs e)
        {
            // INI 파일 읽기
            ini_read();

            // 영상, 자막 리스트뷰 초기화
            init_listview();
        }

        // 드래그&드롭 파일 추가

        // 마우스 포인터 Drag로 올려놓을때
        private void listview_DragEnter(object sender, DragEventArgs e)
        {
            // 마우스 포인터 변경
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // Copy 포인터로 변경
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // 마우스 포인터 Drag 한 후 Drop으로 내려놓을때 아이템 추가
        private void listview_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            ListView listview = sender as ListView;
            if (listview == null)
                return;

            string[] file_paths = (string[])e.Data.GetData(DataFormats.FileDrop); // 데이터를 가져오고 string[] 으로 변환
            bool is_subtitle = (bool)listview.Tag; // Tag 속성에서 추가 정보를 가져옴

            SubTitle.file_add(file_paths, listview, is_subtitle);
        }


        // 영상 / 자막 파일 리스트뷰 Clear 버튼 이벤트 핸들러
        // 버튼에 따라 다른 리스트뷰를 Clear
        private void clear_list_view_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;

            // 버튼이 영상 파일 Clear 버튼일 경우
            if (button == button_video_clear)
            {
                listview_video.Items.Clear();
            }
            // 자막 파일 Clear 버튼일 경우
            else if (button == button_sub_clear)
            {
                listview_sub.Items.Clear();
            }
            // All Clear 버튼일 경우
            else if (button == button_all_clear)
            {
                listview_video.Items.Clear();
                listview_sub.Items.Clear();
            }
        }

        //프로그램 Core - Match 버튼 구현
        private void button_match_Click(object sender, EventArgs e)
        {
            if (listview_video.Items.Count == 0)
            {
                MessageBox.Show("영상 파일을 추가해 주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listview_video.Items.Count != listview_sub.Items.Count)
            {
                MessageBox.Show("영상 파일과 자막파일의 갯수가 동일하지 않아" + Environment.NewLine + "작업을 진행할 수 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("작업을 진행하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    SubTitle.match(listview_video, listview_sub, rvideo.Checked, rcopy.Checked);
                    MessageBox.Show("작업이 완료되었습니다 :)", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // Up & Down 버튼 기능 구현

        // 리스트뷰 항목을 위 아래로 이동시키는 함수
        // move_up : true -> 위로 이동, false -> 아래로 이동
        private void move_listview_item(ListView listview, bool move_up)
        {
            foreach (ListViewItem lvi in listview.SelectedItems)
            {
                int new_index = move_up ? lvi.Index - 1 : lvi.Index + 1;

                // 새로운 인덱스가 범위를 벗어나지 않도록 체크
                if (new_index < 0 || new_index >= listview.Items.Count)
                    continue;

                // 리스트뷰에서 현재 항목 제거 후 새로운 위치에 삽입
                listview.Items.RemoveAt(lvi.Index);
                listview.Items.Insert(new_index, lvi);
            }
        }


        // Up & Down 순서 변경 버튼 클릭 공통 이벤트 핸들러
        private void button_move_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;

            // 버튼 이름을 통해 이동 방향 결정
            // 버튼 이름에 up이 포함되어 있으면 위로 이동, down이 포함되어 있으면 아래로 이동
            bool is_move_up = button.Name.Contains("up");

            // 이동 방향이 결정된 경우 리스트뷰와 방향을 설정
            ListView listview;
            if (button.Name.Contains("video"))
            {
                listview = listview_video;
            }
            else if (button.Name.Contains("sub"))
            {
                listview = listview_sub;
            }
            else
            {
                return; // 리스트뷰를 결정할 수 없으면 반환
            }

            // 리스트뷰 항목을 이동
            move_listview_item(listview, is_move_up);
        }

        // 체크박스 누를시 ini 자동 작성, 모든 라디오 버튼이 이벤트 핸들러에 연결되어 있음
        // 해당 이벤트 핸들러 등록부는 디자이너에 존재한다.
        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            ini_write();
        }

        private void button_how_to_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://pgh268400.tistory.com/189");
        }

        // 컬럼 클릭시 재정렬
        private void listview_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView listView = sender as ListView;

            // 인자로 들어온 listview가 null이거나
            // 리스트뷰 아이템이 1개 이하일 경우 함수 강제 종료
            if (listView == null || listView.Items.Count <= 0)
                return;

            string[] file_paths = new string[listView.Items.Count];

            for (int i = 0; i < listView.Items.Count; i++)
                file_paths[i] = listView.Items[i].SubItems[2].Text;

            Array.Sort(file_paths, NatSort.CompareNatural);

            listView.Items.Clear();
            foreach (string file_path in file_paths)
            {
                string file_name = Path.GetFileNameWithoutExtension(file_path);
                string extension = Path.GetExtension(file_path);

                ListViewItem item = new ListViewItem(file_name);
                item.SubItems.Add(extension);
                item.SubItems.Add(file_path);
                listView.Items.Add(item);
            }
        }

        // 더블클릭시 아이템 삭제
        private void listview_DoubleClick(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;

            if (listview == null)
                return;

            foreach (ListViewItem item in listview.SelectedItems)
            {
                listview.Items.Remove(item);
            }
        }
    }
}
