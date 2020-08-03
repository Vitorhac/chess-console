using board;

namespace game
{
    class Queen : Piece
    {

        public Queen(Board board, Color colour) : base(board, colour)
        {
        }

        public override string ToString()
        {
            return "Q";
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

            // left
            pos.SetValues(position.line, position.column - 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).colour != colour)
                {
                    break;
                }
                pos.SetValues(pos.line, pos.column - 1);
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
                pos.SetValues(pos.line, pos.column + 1);
            }

            // up
            pos.SetValues(position.line - 1, position.column);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).colour != colour)
                {
                    break;
                }
                pos.SetValues(pos.line - 1, pos.column);
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
                pos.SetValues(pos.line + 1, pos.column);
            }

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