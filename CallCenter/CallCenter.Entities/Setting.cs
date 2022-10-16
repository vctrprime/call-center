

namespace CallCenter.Entities
{
    public class Setting
    {
        public int Id { get; set; }
        public int TimeManager { get; set; }
        public int TimeDirector { get; set; }
        public int ExecuteTimeLimitLeft { get; set; }
        public int ExecuteTimeLimitRight { get; set; }
    }
}