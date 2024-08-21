namespace Project.App.Definitions
{
    public static class RedisTemplate
    {
        public static string Device = "VMS-MTC-Device-";
        

        public static string GetDeviceKey(this string deviceCode)
        {
            return Device+deviceCode;
        }
        public static string GetDeviceKeyPlay(this string deviceCode)
        {
            return "Play-Device-" + deviceCode;
        }
        public static string GetDashboardKey(this string areaId)
        {
            return "Dashboard-" + areaId;
        }
        public static string GetAllchildArea(this string areaId)
        {
            return "GetAllchildArea-" + areaId;
        }
        public static string GetAllchildAreaHTTTCode(this string areaId)
        {
            return "GetAllchildAreaHTTTCode-" + areaId;
        }
    }
}