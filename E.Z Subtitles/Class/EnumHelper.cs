
using System.ComponentModel;
using System.Reflection;


namespace E.Z_Subtitles.Class
{
    public static class EnumHelper
    {
        /*
        Enum 요소에 Description을 활용하기 위한 확장 메서드
          확장메서드?
          static 메서드로서 특정 클래스나 구조체 심지어 인터페이스까지 메서드를 확장가능하게 한다.
          참고 : https://forsoftwaredev.tistory.com/2
          이미 존재하는 클래스, 구조체, 인터페이스에 함수를 새로 추가 가능하는 꿀 기능
          기존 타입을 적고 this 키워드를 붙이고 전부 static으로 선언하면 끝난다.
        */

        // 아래처럼 코딩하면 모든 Enum 요소에 대해 GetDescription() 함수가 추가되며 사용할 수 있다. 신기!!
        public static string GetDescription(this System.Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
    }
}
