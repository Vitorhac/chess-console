using System.Collections.Generic;
using board;

namespace game
{
    class ChessPlay
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }
        public Piece vulneravelEnPassant { get; private set; }

        public ChessPlay()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            check = false;
            vulneravelEnPassant = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            PlacePieces();
        }

        public Piece ExecuteMovement(Position origin, Position destination)
        {
            Piece p = board.WithdrawPiece(origin);
            p.IncrementMovements();
            Piece capturedPiece = board.WithdrawPiece(destination);
            board.PlacePiece(p, destination);

            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
            //Small Castling
            if (p is King && destination.column == origin.column + 2)
            {
                Position originT = new Position(origin.line, origin.column + 3);
                Position destinationT = new Position(origin.line, origin.column + 1);
                Piece T = board.WithdrawPiece(originT);
                T.IncrementMovements();
                board.PlacePiece(T, destinationT);
            }
            // Big Castling
            if (p is King && destination.column == origin.column - 2)
            {
                Position originT = new Position(origin.line, origin.column - 4);
                Position destinationT = new Position(origin.line, origin.column - 1);
                Piece T = board.WithdrawPiece(originT);
                T.IncrementMovements();
                board.PlacePiece(T, destinationT);
            }
            //En Passant
            if (p is Pawn)
            {
                if (origin.column != destination.column && capturedPiece == null)
                {
                    Position posP;
                    if (p.colour == Color.White)
                    {
                        posP = new Position(destination.line + 1, destination.column);
                    }
                    else
                    {
                        posP = new Position(destination.line - 1, destination.column);
                    }
                    capturedPiece = board.WithdrawPiece(posP);
                    captured.Add(capturedPiece);
                }
            }
            return capturedPiece;
        }

        public void desfazMovimento(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = board.WithdrawPiece(destination);
            p.DecrementMovements();
            if (capturedPiece != null)
            {
                board.PlacePiece(capturedPiece, destination);
                captured.Remove(capturedPiece);
            }
            board.PlacePiece(p, origin);

            //Small Castling
            if (p is King && destination.column == origin.column + 2)
            {
                Position originT = new Position(origin.line, origin.column + 3);
                Position destinationT = new Position(origin.line, origin.column + 1);
                Piece T = board.WithdrawPiece(destinationT);
                T.DecrementMovements();
                board.PlacePiece(T, originT);
            }

            //Big Castling
            if (p is King && destination.column == origin.column - 2)
            {
                Position originT = new Position(origin.line, origin.column - 4);
                Position destinationT = new Position(origin.line, origin.column - 1);
                Piece T = board.WithdrawPiece(destinationT);
                T.DecrementMovements();
                board.PlacePiece(T, originT);
            }

            //En passant
            if (p is Pawn)
            {
                if (origin.column != destination.column && capturedPiece == vulneravelEnPassant)
                {
                    Piece peao = board.WithdrawPiece(destination);
                    Position posP;
                    if (p.colour == Color.White)
                    {
                        posP = new Position(3, destination.column);
                    }
                    else
                    {
                        posP = new Position(4, destination.column);
                    }
                    board.PlacePiece(peao, posP);
                }
            }
        }

        public void MakePlay(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMovement(origin, destination);

            if (IsInCheck(currentPlayer))
            {
                desfazMovimento(origin, destination, capturedPiece);
                throw new BoardException("You can't place yourself in Check! Press Enter");
            }
            Piece p = board.piece(destination);
            //Promotion
            if (p is Pawn)
            {
                if ((p.colour == Color.White && destination.line == 0) || (p.colour == Color.Black && destination.line == 7))
                {
                    p = board.WithdrawPiece(destination);
                    pieces.Remove(p);
                    Piece dama = new Queen(board, p.colour);
                    board.PlacePiece(dama, destination);
                    pieces.Add(dama);
                }
            }
            if (IsInCheck(Opponent(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }
            if (TestCheckMate(Opponent(currentPlayer)))
            {
                finished = true;
            }
            else
            {
                turn++;
                ChangePlayer();
            }
            //En passant
            if (p is Pawn && (destination.line == origin.line - 2 || destination.line == origin.line + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (board.piece(pos) == null)
            {
                throw new BoardException("There is no Piece in the chosen position! Press Enter");
            }
            if (currentPlayer != board.piece(pos).colour)
            {
                throw new BoardException("That Piece is not Yours! Press Enter");
            }
            if (!board.piece(pos).PossibleMovementsExist())
            {
                throw new BoardException("There are no possible movements por the chosen Piece! Press Enter");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!board.piece(origin).PossibleMovement(destination))
            {
                throw new BoardException("Invalid destination position! Press Enter");
            }
        }

        private void ChangePlayer()
        {
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }

        public HashSet<Piece> CapturedPieces(Color colour)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.colour == colour)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInPlay(Color colour)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.colour == colour)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(colour));
            return aux;
        }

        private Color Opponent(Color colour)
        {
            if (colour == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color colour)
        {
            foreach (Piece x in PiecesInPlay(colour))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsInCheck(Color colour)
        {
            Piece R = King(colour);
            if (R == null)
            {
                throw new BoardException("There is no King of color " + colour + " on the board!");
            }
            foreach (Piece x in PiecesInPlay(Opponent(colour)))
            {
                bool[,] mat = x.PossibleMovements();
                if (mat[R.position.line, R.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestCheckMate(Color colour)
        {
            if (!IsInCheck(colour))
            {
                return false;
            }
            foreach (Piece x in PiecesInPlay(colour))
            {
                bool[,] mat = x.PossibleMovements();
                for (int i = 0; i < board.lines; i++)
                {
                    for (int j = 0; j < board.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = ExecuteMovement(origin, destination);
                            bool testCheck = IsInCheck(colour);
                            desfazMovimento(origin, destination, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PlaceNewPiece(char column, int line, Piece piece)
        {
            board.PlacePiece(piece, new ChessPosition(column, line).ToPosition());
            pieces.Add(piece);
        }

        private void PlacePieces()
        {
            PlaceNewPiece('a', 1, new Rook(board, Color.White));
            PlaceNewPiece('b', 1, new Horse(board, Color.White));
            PlaceNewPiece('c', 1, new Bishop(board, Color.White));
            PlaceNewPiece('d', 1, new Queen(board, Color.White));
            PlaceNewPiece('e', 1, new King(board, Color.White, this));
            PlaceNewPiece('f', 1, new Bishop(board, Color.White));
            PlaceNewPiece('g', 1, new Horse(board, Color.White));
            PlaceNewPiece('h', 1, new Rook(board, Color.White));
            PlaceNewPiece('a', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('b', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('c', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('d', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('e', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('f', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('g', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('h', 2, new Pawn(board, Color.White, this));

            PlaceNewPiece('a', 8, new Rook(board, Color.Black));
            PlaceNewPiece('b', 8, new Horse(board, Color.Black));
            PlaceNewPiece('c', 8, new Bishop(board, Color.Black));
            PlaceNewPiece('d', 8, new Queen(board, Color.Black));
            PlaceNewPiece('e', 8, new King(board, Color.Black, this));
            PlaceNewPiece('f', 8, new Bishop(board, Color.Black));
            PlaceNewPiece('g', 8, new Horse(board, Color.Black));
            PlaceNewPiece('h', 8, new Rook(board, Color.Black));
            PlaceNewPiece('a', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('b', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('c', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('d', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('e', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('f', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('g', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('h', 7, new Pawn(board, Color.Black, this));
        }
    }
}
