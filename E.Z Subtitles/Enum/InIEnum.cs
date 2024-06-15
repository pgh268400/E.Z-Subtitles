namespace E.Z_Subtitles.Enum
{
    using System.ComponentModel;


    /*
      Description 을 활용해서 각 enum 요소에 설명을 남긴다.
      이후 해당 설명을 가져오기 위해 별도로 class 파일에 구현한
      GetDescription() 메서드를 활용할 것이다.
      해당 파일은 InI 저장에 관련된 enum 선언을 모아둔 파일이다.

      여기에 설정된 Description 대로 InI 파일이 기재된다.
      InI 파일의 형식을 바꾸고 싶으면 아래에서 바꾸면 된다.
    */

    // InI 파일 이름, class로 묶어주는 이유는 
    // C#의 언어 구조상 모든 멤버는 반드시 클래스, 구조체, 인터페이스 또는 열거형 내에 있어야 하기 때문 (by ChatGpt4.0o)
    public static class INI
    {
        public const string file_name = "Settings.ini";
    }

    // 따라갈 파일 이름 : 영상 파일, 자막 파일
    public enum FollowMethod
    {
        [Description("video")] Video,
        [Description("smi")] Subtitle
    }

    // 매칭 방법 : 복사, 이동
    public enum MatchStyle
    {
        [Description("copy")] Copy,
        [Description("move")] Move
    }

    // InI 파일 에서 사용할 Key 값들 정의
    public enum InIKey
    {
        [Description("Settings")] TopSection,
        [Description("f_name")] FollowMethod,
        [Description("match_style")] MatchStyle,
    }
}
