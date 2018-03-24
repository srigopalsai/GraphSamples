namespace GraphSamples
{
    public enum CellType
    {
        SPACE,
        BLOCK,
        GUARD
    }

    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Cell(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }
    }

    public class Point
    {
        public int xPos { get; set; }

        public int yPos { get; set; }

        Point PreviousPoint { get; set; }

        public Point() { }

        public Point(int x, int y, Point prevPoint = null)
        {
            this.xPos = x;
            this.yPos = y;
            this.PreviousPoint = prevPoint;
        }

        public Point GetPreviousPoint()
        {
            return this.PreviousPoint;
        }

        public static bool IsSafePoint(int[,] matrix, int xPos, int yPos, int safeVal = 1)
        {
            if (xPos >= 0 && xPos < matrix.GetLength(0) &&
                yPos >= 0 && yPos < matrix.GetLength(1) &&
                matrix[xPos, yPos] == safeVal)
            {
                return true;
            }
            return false;
        }
    }
}