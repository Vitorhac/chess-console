using board;

namespace game
{
    class Rook : Piece
    {
        public Rook(Board board, Color colour) : base(board, colour)
        {
        }

        public override string ToString()
        {
            return "R";
        }

        private bool CanMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.colour != colour;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            // up
            pos.SetValues(position.line - 1, position.column);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).colour != colour)
                {
                    break;
                }
                pos.line -= 1;
            }
            // down
            pos.SetValues(position.line + 1, position.column);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).colour != colour)
                {
                    break;
                }
                pos.line += 1;
            }
            // right
            pos.SetValues(position.line, position.column + 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).colour != colour)
                {
                    break;
                }
                pos.column += 1;
            }
            // left
            pos.SetValues(position.line, position.column - 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).colour != colour)
                {
                    break;
                }
                pos.column -= 1;
            }
            return mat;
        }
    }
}
