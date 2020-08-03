namespace board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color colour { get; protected set; }
        public int amountMovements { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color colour)
        {
            this.position = null;
            this.board = board;
            this.colour = colour;
            this.amountMovements = 0;
        }

        public void IncrementMovements()
        {
            amountMovements++;
        }

        public void DecrementMovements()
        {
            amountMovements--;
        }

        public bool PossibleMovementsExist()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < board.lines; i++)
            {
                for (int j = 0; j < board.columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMovement(Position pos)
        {
            return PossibleMovements()[pos.line, pos.column];
        }

        public abstract bool[,] PossibleMovements();
    }
}
