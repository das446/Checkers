namespace CheckersLogic {
  public class Move {
    public Position from { get; }
    public Position to { get; }
    public Position over { get; }
    public bool jump;
    public Move(Position from, Position to) {
      this.from = from;
      this.to = to;
      jump = false;
    }

    public Move(Position from, Position to,Position over) {
      this.from = from;
      this.to = to;
      this.over = over;
      jump = true;
    }

    public bool equals(Move other) {
      return this.from.Equals(other.from) && this.to.Equals(other.to);
    }

    public static Move fromString(string move) {
      move = move.Trim();
      if (move.Length != 5) {
        return null; //Throw here
      }
      return new Move(new Position(move[1] - '1', 'h' - move[0]), new Position(move[4] - '1', 'h' - move[3]));
    }

    public override string ToString() {
      return "" + (char) ('h' - from.col) + (from.row + 1) + " " + (char) ('h' - to.col) + (to.row + 1);
    }
  }
}