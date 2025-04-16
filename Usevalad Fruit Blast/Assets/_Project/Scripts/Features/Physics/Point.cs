namespace _Project.Scripts.Features.Physics
{
    public class Point
    {
        private int _x;
        private int _y;
        private Point _next;
        
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public Point Next { get => _next; set => _next = value; }

        public static bool operator ==(Point lhs, Point rhs)
        {
            return lhs != null && rhs != null && lhs.X == rhs.X && lhs.Y == rhs.Y;
        }
        
        public static bool operator !=(Point lhs, Point rhs) => !(lhs == rhs);
        
        public override bool Equals(object other) => other is Point point && Equals(point);
        
        public bool Equals(Point other) => _x == other._x && _y == other._y;

        public override int GetHashCode()
        {
            int num1 = _x;
            int hashCode = num1.GetHashCode();
            num1 = _y;
            int num2 = num1.GetHashCode() << 3;
            return hashCode ^ num2;
        }
    }
}