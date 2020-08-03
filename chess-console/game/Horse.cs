using board;

namespace game
{
    //For practical purposes the Knight is a Horse
    class Horse : Piece
    {
        public Horse(Board board, Color colour) : base(board, colour)
        {
        }

        public override string ToString()
        {
            return "H";
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

            pos.SetValues(position.line - 1, position.column - 2);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line - 2, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line - 2, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line - 1, position.column + 2);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line + 1, position.column + 2);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line + 2, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line + 2, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line + 1, position.column - 2);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            return mat;
        }
    }
}