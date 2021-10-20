using System;


namespace csharp_chess.Board
{
    class BoardException : Exception
    {
        public BoardException(string message) 
                       : base(message)
        {
        }
    }
}
