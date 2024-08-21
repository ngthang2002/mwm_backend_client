namespace Project.App.DesignPatterns.ObserverPatterns
{
    public class ObserverPatternHandling
    {
        public static readonly ObserverPatternHandling instance = new ObserverPatternHandling();
        public static ObserverPatternHandling Instance => instance;
        public ObserverPatternHandling() { }
        public void Connection()
        {
            Handling();
        }
        public void Handling()
        {
            ObserverPattern.Instance.On("EventName", (data) =>
            {

            });
        }
    }
}
