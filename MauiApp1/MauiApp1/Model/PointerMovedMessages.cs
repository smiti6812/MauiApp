namespace MauiApp1.Model
{
    public class PointerMovedMessage
    {
        public double X { get; }
        public double Y { get; }

        public PointerMovedMessage(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
