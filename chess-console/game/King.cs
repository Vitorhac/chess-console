using board;

namespace game
{
    class King : Piece
    {
        private ChessPlay play;

        public King(Board board, Color colour, ChessPlay play) : base(board, colour)
        {
            this.play = play;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.colour != colour;
        }

        private bool TestCastling(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Rook && p.colour == colour && p.amountMovements == 0;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            // up
            pos.SetValues(position.line - 1, position.column);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // ne
            pos.SetValues(position.line - 1, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // right
            pos.SetValues(position.line, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // se
            pos.SetValues(position.line + 1, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // down
            pos.SetValues(position.line + 1, position.column);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // so
            pos.SetValues(position.line + 1, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // left
            pos.SetValues(position.line, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // no
            pos.SetValues(position.line - 1, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // Castling
            if (amountMovements == 0 && !play.check)
            {
                //Small Castling
                Position posT1 = new Position(position.line, position.column + 3);
                if (TestCastling(posT1))
                {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);
                    if (board.piece(p1) == null && board.piece(p2) == null)
                    {
                        mat[position.line, position.column + 2] = true;
                    }
                }
                // #Big Castling
                Position posT2 = new Position(position.line, position.column - 4);
                if (TestCastling(posT2))
                {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                    {
                        mat[position.line, position.column - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}