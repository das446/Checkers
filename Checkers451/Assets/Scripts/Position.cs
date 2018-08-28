namespace CheckersLogic {
  public class Position {
    public int row {get;}
    public int col {get;}
    public Position(int row, int col) {
      this.row = row;
      this.col = col;
    }

    public bool equals(Position other) {
      return other.row == this.row && other.col == this.col;
    }
  }
}