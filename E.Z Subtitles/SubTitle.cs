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

        //드래그 드롭 파일 추가 Core
        public static void file_add(string[] files, ListView lv, bool isSMI)
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

                        if (isSMI == true & subtitle_check_ext(path) == true)
                        {
                            ListViewItem item = new ListViewItem(filename);
                            item.SubItems.Add(path);
                            item.SubItems.Add(file_path);
                            lv.Items.Add(item);
                        }
                        else if (isSMI == false & subtitle_check_ext(path) == false)
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
                            if (isSMI == true & subtitle_check_ext(path) == true)
                            {
                                ListViewItem item = new ListViewItem(filename);
                                item.SubItems.Add(path);
                                item.SubItems.Add(d_file_path);
                                lv.Items.Add(item);
                            }
                            else if (isSMI == false & subtitle_check_ext(path) == false)
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

        // 파일 이름 매칭 (Main 기능)
        public static void match(ListView left_video_lv, ListView right_video_lv, bool isFollowVideoName, bool isCopy)
        {
            try
            {
                for (int i = 0; i < left_video_lv.Items.Count; i++)
                {
                    if (isFollowVideoName) //따라갈 파일 이름 : 영상
                    {
                        string video_path = left_video_lv.Items[i].SubItems[2].Text;
                        string smi_path = right_video_lv.Items[i].SubItems[2].Text;
                        //비디오 파일 경로\비디오파일이름\.자막확장자
                        string copy_path = Path.GetDirectoryName(video_path) + "\\" + Path.GetFileNameWithoutExtension(video_path) + Path.GetExtension(smi_path);
                        //MessageBox.Show(String.Format("{0} {1}", smi_path, copy_path));

                        if (isCopy)
                        {
                            if (smi_path == copy_path) File.Move(smi_path, copy_path); //동일경로면 Move로 옮김
                            else File.Copy(smi_path, copy_path); //자막 복사 모드

                        }
                        else File.Move(smi_path, copy_path); //자막 이동 모드
                    }
                    else //따라갈 파일 이름 : 자막
                    {
                        string video_path = left_video_lv.Items[i].SubItems[2].Text;
                        string smi_path = right_video_lv.Items[i].SubItems[2].Text;
                        //비디오 파일 경로\자막파일이름\.자막확장자
                        string copy_path = Path.GetDirectoryName(video_path) + "\\" + Path.GetFileNameWithoutExtension(smi_path) + Path.GetExtension(smi_path);
                        //MessageBox.Show(String.Format("{0} {1}", smi_path, copy_path));

                        if (isCopy)
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
