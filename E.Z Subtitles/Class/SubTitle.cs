using System;
using System.IO;
using System.Windows.Forms;

namespace E.Z_Subtitles
{
    class SubTitle
    {
        // 편의를 위해서 static으로 메서드(함수) 를 생성
        // 프로그램 규모가 작으므로 사용하나, 큰 프로젝트에선 메모리 관리를 위해 사용을 비권장

        //자막 확장자(유효성) 검사
        private static bool subtitle_check_ext(string ext)
        {
            string[] sub_ext = { ".psb", ".srt", ".ssa", ".ass", ".sub", ".sami", ".smi", ".smil", ".usf", ".vtt" };
            foreach (string c in sub_ext)
            {
                if (ext.ToLower() == c) return true; //자막 True
            }
            return false; //자막 False
        }

        // ListViewItem을 추가하는 메서드
        private static void add_listview_item(ListView lv, string filename, string path, string file_path)
        {
            ListViewItem item = new ListViewItem(filename);
            item.SubItems.Add(path);
            item.SubItems.Add(file_path);
            lv.Items.Add(item);
        }

        //드래그 드롭 파일 추가 Core
        public static void file_add_to_list(string[] files, ListView lv, bool is_smi)
        {
            if (files.Length < 1) return; // 파일 목록이 없는 경우 함수 강제 종료

            Array.Sort(files, NatSort.CompareNatural); // Natural Sort로 문자열 정렬

            foreach (string file_path in files)
            {
                bool directory_check = File.GetAttributes(file_path).HasFlag(FileAttributes.Directory);

                // 드래그 드롭된 것이 파일인 경우
                if (directory_check == false)
                {
                    // 파일 확장자를 가져온다.
                    string ext = Path.GetExtension(file_path);

                    // 먼저 자막 파일이라면 자막 리스트뷰에 추가한다는 말이 된다.
                    if (is_smi)
                    {
                        // 자막 리스트뷰에 자막이 아닌것을 추가하지 않는다.
                        if (subtitle_check_ext(ext) == false) continue; // 자막 확장자가 아닌 경우 다음 파일로 이동
                    }
                    // 자막 파일이 아니라면 비디오 리스트뷰에 추가한다는 말이 된다.
                    else
                    {
                        // 비디오 리스트뷰에 자막을 추가하지 않는다.
                        if (subtitle_check_ext(ext) == true) continue; // 자막 확장자인 경우 다음 파일로 이동
                    }

                    // 위 검사를 통과했다면 리스트뷰에 추가
                    string filename = Path.GetFileNameWithoutExtension(file_path);
                    string path = Path.GetExtension(file_path);
                    add_listview_item(lv, filename, path, file_path);
                }
                // 드래그 드롭된 것이 폴더(디렉토리)인 경우
                else
                {
                    // 디렉토리 내 모든 파일을 재귀적으로 추가
                    string[] directory_files = Directory.GetFiles(file_path, "*.*", SearchOption.AllDirectories); // 모든 파일 로드
                    Array.Sort(directory_files, NatSort.CompareNatural); // Natural Sort
                    file_add_to_list(directory_files, lv, is_smi); // 재귀적으로 호출하여 파일 추가
                }
            }

            // if (lv.Items.Count > 0)
            //    lv.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent); // Column Auto Width
        }

        // 파일 이름 매칭 (Main 기능)
        public static void match(ListView left_video_lv, ListView right_video_lv, bool is_follow_video_name, bool is_copy)
        {
            try
            {
                for (int i = 0; i < left_video_lv.Items.Count; i++)
                {
                    if (is_follow_video_name) // 따라갈 파일 이름 : 영상
                    {
                        string video_path = left_video_lv.Items[i].SubItems[2].Text;
                        string smi_path = right_video_lv.Items[i].SubItems[2].Text;
                        //비디오 파일 경로\비디오파일이름\.자막확장자
                        string copy_path = Path.GetDirectoryName(video_path) + "\\" + Path.GetFileNameWithoutExtension(video_path) + Path.GetExtension(smi_path);
                        // MessageBox.Show(String.Format("{0} {1}", smi_path, copy_path));

                        if (is_copy)
                        {
                            if (smi_path == copy_path) File.Move(smi_path, copy_path); //동일경로면 Move로 옮김
                            else File.Copy(smi_path, copy_path); //자막 복사 모드
                        }
                        else File.Move(smi_path, copy_path); //자막 이동 모드
                    }
                    else // 따라갈 파일 이름 : 자막
                    {
                        string video_path = left_video_lv.Items[i].SubItems[2].Text;
                        string smi_path = right_video_lv.Items[i].SubItems[2].Text;
                        //비디오 파일 경로\자막파일이름\.자막확장자
                        string copy_path = Path.GetDirectoryName(video_path) + "\\" + Path.GetFileNameWithoutExtension(smi_path) + Path.GetExtension(smi_path);
                        // MessageBox.Show(String.Format("{0} {1}", smi_path, copy_path));

                        if (is_copy)
                        {
                            if (smi_path == copy_path) File.Move(smi_path, copy_path); //동일경로면 Move로 옮김
                            else File.Copy(smi_path, copy_path); //자막 복사 모드

                        }
                        else File.Move(smi_path, copy_path); //자막 이동 모드

                        //비디오 파일경로\자막파일이름\.비디오확장자
                        string change_video_path = Path.GetDirectoryName(video_path) + "\\" + Path.GetFileNameWithoutExtension(smi_path) + Path.GetExtension(video_path);
                        File.Move(video_path, change_video_path); //영상파일 이름 -> 자막파일 이름으로 변경
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
