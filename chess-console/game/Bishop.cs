using board;

namespace game
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color colour) : base(board, colour)
        {
        }

        public override string ToString()
        {
            return "B";
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

            // NO
            pos.SetValues(position.line - 1, position.column - 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).colour != colour)
                {
                    break;
                }
                pos.SetValues(pos.line - 1, pos.column - 1);
            }

            // NE
            pos.SetValues(position.line - 1, position.column + 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).colour != colour)
                {
                    break;
                }
                pos.SetValues(pos.line - 1, pos.column + 1);
            }

            // SE
            pos.SetValues(position.line + 1, position.column + 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).colour != colour)
                {
                    break;
                }
                pos.SetValues(pos.line + 1, pos.column + 1);
            }

            // SO
            pos.SetValues(position.line + 1, position.column - 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).colour != colour)
                {
                    break;
                }
                pos.SetValues(pos.line + 1, pos.column - 1);
            }
            return mat;
        }
    }
}