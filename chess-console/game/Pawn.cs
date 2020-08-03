using board;

namespace game
{
    class Pawn : Piece
    {
        private ChessPlay play;
        public Pawn(Board board, Color colour, ChessPlay play) : base(board, colour)
        {
            this.play = play;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool EnemyExists(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.colour != colour;
        }

        private bool Free(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            if (colour == Color.White)
            {
                pos.SetValues(position.line - 1, position.column);
                if (board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line - 2, position.column);
                Position p2 = new Position(position.line - 1, position.column);
                if (board.ValidPosition(p2) && Free(p2) && board.ValidPosition(pos) && Free(pos) && amountMovements == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line - 1, position.column - 1);
                if (board.ValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line - 1, position.column + 1);
                if (board.ValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                // En passant
                if (position.line == 3)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.ValidPosition(left) && EnemyExists(left) && board.piece(left) == play.vulneravelEnPassant)
                    {
                        mat[left.line - 1, left.column] = true;
                    }
                    Position right = new Position(position.line, position.column + 1);
                    if (board.ValidPosition(right) && EnemyExists(right) && board.piece(right) == play.vulneravelEnPassant)
                    {
                        mat[right.line - 1, right.column] = true;
                    }
                }
            }
            else
            {
                pos.SetValues(position.line + 1, position.column);
                if (board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line + 2, position.column);
                Position p2 = new Position(position.line + 1, position.column);
                if (board.ValidPosition(p2) && Free(p2) && board.ValidPosition(pos) && Free(pos) && amountMovements == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line + 1, position.column - 1);
                if (board.ValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line + 1, position.column + 1);
                if (board.ValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                // En passant
                if (position.line == 4)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.ValidPosition(left) && EnemyExists(left) && board.piece(left) == play.vulneravelEnPassant)
                    {
                        mat[left.line + 1, left.column] = true;
                    }
                    Position right = new Position(position.line, position.column + 1);
                    if (board.ValidPosition(right) && EnemyExists(right) && board.piece(right) == play.vulneravelEnPassant)
                    {
                        mat[right.line + 1, right.column] = true;
                    }
                }
            }
            return mat;
        }
    }
}
