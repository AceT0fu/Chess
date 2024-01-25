using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Direction
    {
        public readonly static Direction up = new Direction(1, 0);
        public readonly static Direction down = new Direction(-1, 0);
        public readonly static Direction left = new Direction(0, -1);
        public readonly static Direction right = new Direction(0, 1);

        public readonly static Direction upLeft = up + left;
        public readonly static Direction upRight = up + right;
        public readonly static Direction downLeft = down + left;
        public readonly static Direction downright = down + right;

        public int rowDelta { get; }
        public int columnDelta { get; }

        public Direction(int rowDelta, int columnDelta)
        {
            this.rowDelta = rowDelta;
            this.columnDelta = columnDelta;
        }

        public static Direction operator +(Direction dir1, Direction dir2)
        {
            return new Direction(dir1.rowDelta + dir2.rowDelta, dir1.columnDelta + dir2.columnDelta);
        }

        public static Direction operator *(int scalar, Direction dir)
        {
            return new Direction(dir.rowDelta * scalar, dir.columnDelta * scalar);
        }
    }
}
