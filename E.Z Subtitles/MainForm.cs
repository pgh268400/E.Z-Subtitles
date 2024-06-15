using E.Z_Subtitles.Class;
using E.Z_Subtitles.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        // 폼로드 이벤트
        private void Form1_Load(object sender, EventArgs e)
        {
            // INI 파일 읽기
            ini_read();

            // 영상, 자막 리스트뷰 초기화
            init_listview();
        }



        // INI R/W
        private void ini_write()
        {
            // :: INI 파일의 key / value 수정을 원하면 Enum 폴더 안의 InIEnum.cs 에 들어가서 Description 을 변경

            // 반드시 enum 값을 초기화 해줘야 하기 때문에 nullable한 enum을 사용해 null로 초기화.
            // 초기화를 하지 않으면 아래 GetDescription() 의 확장 메서드 컴파일이 되지 않는다.
            // 따라갈 파일 이름 : 영상 파일, 자막 파일
            FollowMethod? follow_file_name = null;

            // 매칭 방법 : 복사, 이동
            MatchStyle? match_style = null;

            // 라디오 버튼 체크에 따라 enum 값 설정
            // 무조건 if 또는 else if 에 걸려 enum이 초기화 되지 않는 경우는 없다.
            if (rvideo.Checked) follow_file_name = FollowMethod.Video;
            else if (rsmi.Checked) follow_file_name = FollowMethod.Subtitle;
            if (rcopy.Checked) match_style = MatchStyle.Copy;
            else if (rmove.Checked) match_style = MatchStyle.Move;

            IniFile ini = new IniFile();

            // enum의 descripion을 가져와서 ini 파일의 value로써 저장한다.
            string follow_desc = follow_file_name.GetDescription();
            string match_desc = match_style.GetDescription();

            string key_main = InIKey.TopSection.GetDescription();
            string key_follow = InIKey.FollowMethod.GetDescription();
            string key_match = InIKey.MatchStyle.GetDescription();

            ini[key_main][key_follow] = follow_file_name.GetDescription();
            ini[key_main][key_match] = match_style.GetDescription();
            ini.Save(INI.file_name);
        }

        private void ini_read()
        {
            // :: INI 파일의 key / value 수정을 원하면 Enum 폴더 안의 InIEnum.cs 에 들어가서 Description 을 변경

            // enum의 description을 가져오기 위해 string 변수 선언
            string follow_video = FollowMethod.Video.GetDescription();
            string follow_smi = FollowMethod.Subtitle.GetDescription();
            string copy = MatchStyle.Copy.GetDescription();
            string move = MatchStyle.Move.GetDescription();

            // InI 파일에서 사용할 Key 값들 정의
            string key_main = InIKey.TopSection.GetDescription();
            string key_follow = InIKey.FollowMethod.GetDescription();
            string key_match = InIKey.MatchStyle.GetDescription();

            IniFile ini = new IniFile();
            if (File.Exists(INI.file_name))
                ini.Load(INI.file_name);

            string f_name = ini[key_main][key_follow].ToString();
            string match_style = ini[key_main][key_match].ToString();

            if (f_name == follow_video) rvideo.Checked = true;
            else if (f_name == follow_smi) rsmi.Checked = true;
            if (match_style == copy) rcopy.Checked = true;
            else if (match_style == move) rmove.Checked = true;
        }

        // 컬럼 너비를 비율로 조정하는 메서드
        private void adjust_column_widths(ListView list_view, int[] column_widths)
        {
            if (list_view.Columns.Count != column_widths.Length)
                throw new ArgumentException("컬럼의 수와 비율 배열의 길이가 일치하지 않습니다.");

            /*
              리스트뷰 비율 전체 합이 100이 아닌 경우 Exception 발생
              Linq를 활용해 배열 내부에서 마치 SQL 쿼리를 사용하듯이 Sum(합)을 구한다.
              대충 배열 하나 하나 요소가 쿼리할 때 나오는 숫자 행 값들이라고 생각하면 될 거 같다.
              Ex) column_widths.Sum() -> SELECT SUM(column_widths) FROM column_widths
            */
            if (column_widths.Sum() != 100)
                throw new ArgumentException("컬럼 너비 비율의 합이 100이 아닙니다.");

            int total_width = list_view.ClientSize.Width;
            for (int i = 0; i < column_widths.Length; i++)
                list_view.Columns[i].Width = (int)(total_width * column_widths[i] / 100.0);
        }

        private void init_listview()
        {
            // 리스트뷰 정렬 방식을 기본으로 설정
            listview_video.Alignment = ListViewAlignment.Default;
            listview_sub.Alignment = ListViewAlignment.Default;

            // 리스트뷰에 드래그 드롭을 허용으로 설정
            listview_video.AllowDrop = true;
            listview_sub.AllowDrop = true;

            // 리스트뷰 컬럼 너비 비율 고정
            adjust_column_widths(listview_video, new int[] { 50, 12, 38 }); // 총합은 100% 가 되도록 설정 해야 한다.
            adjust_column_widths(listview_sub, new int[] { 50, 12, 38 });

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

        // 리스트뷰 아이템 변화시 라벨 업데이트 해주는 함수
        private void update_count_label()
        {
            int video_count = listview_video.Items.Count;
            int sub_count = listview_sub.Items.Count;

            label_video_count.Text = $"영상 : {video_count}개";
            label_sub_count.Text = $"자막 : {sub_count}개";

            // 영상, 자막의 개수가 채워졌는지 체크
            // 영상, 자막이 다 채워져야 유효성 검사를 시작.
            if (video_count == 0 || sub_count == 0)
            {
                label_video_count.ForeColor = System.Drawing.Color.Black;
                label_sub_count.ForeColor = System.Drawing.Color.Black;
                return;
            }

            // 유효성 검사 : 만약 영상 파일과 자막 파일의 개수가 같다면
            if (video_count == sub_count)
            {
                label_video_count.ForeColor = System.Drawing.Color.Blue;
                label_sub_count.ForeColor = System.Drawing.Color.Blue;


            }
            // 그렇지 않다면
            else
            {
                label_video_count.ForeColor = System.Drawing.Color.Red;
                label_sub_count.ForeColor = System.Drawing.Color.Red;
            }
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
            bool is_smi = listview == listview_sub; // 자막 리스트뷰인지 확인

            // 이에 맞춰 파일 추가 함수 호출
            SubTitle.file_add_to_list(file_paths, listview, is_smi);

            // 라벨 업데이트
            update_count_label();
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

            update_count_label();
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
                    // 에러 메세지 출력
                    MessageBox.Show(ex.Message, "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // 정렬 순서를 저장할 전역 변수
        private Dictionary<int, bool> column_sort_order = new Dictionary<int, bool>();

        // 컬럼 클릭시 재정렬
        private void listview_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView list_view = sender as ListView;

            // 인자로 들어온 list_view가 null이거나 리스트뷰 아이템이 1개 이하일 경우 함수 강제 종료
            if (list_view == null || list_view.Items.Count <= 0)
                return;

            // 클릭된 열의 인덱스
            int column_index = e.Column;

            // 정렬 순서를 결정합니다.
            if (!column_sort_order.ContainsKey(column_index))
            {
                // 처음 클릭 시 내림차순 정렬, 드래그 드롭으로 추가하면
                // 자동으로 내림차순 정렬되기 때문이다.
                column_sort_order[column_index] = false;
            }
            else
            {
                // 클릭할 때마다 정렬 순서를 토글합니다.
                column_sort_order[column_index] = !column_sort_order[column_index];
            }

            // ListViewItem을 포함하는 배열 생성
            ListViewItem[] items = new ListViewItem[list_view.Items.Count];
            list_view.Items.CopyTo(items, 0);

            // 내추럴 정렬
            if (column_sort_order[column_index])
            {
                // 오름차순 정렬
                Array.Sort(items, (x, y) => NatSort.CompareNatural(x.SubItems[column_index].Text, y.SubItems[column_index].Text));
            }
            else
            {
                // 내림차순 정렬
                Array.Sort(items, (x, y) => NatSort.CompareNatural(y.SubItems[column_index].Text, x.SubItems[column_index].Text));
            }

            // 정렬된 순서로 리스트뷰 아이템 재구성
            list_view.BeginUpdate();
            list_view.Items.Clear();
            list_view.Items.AddRange(items);
            list_view.EndUpdate();
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

            update_count_label();
        }
    }
}
