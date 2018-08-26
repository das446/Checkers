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

    public Move(Position from, Position to, Position over) {
      this.from = from;
      this.to = to;
      this.over = over;
      jump = true;
    }

    public bool equals(Move other) {
      return this.from.Equals(other.from) && this.to.Equals(other.to);
    }

    /// <summary>
    /// Format MoveJ|fr|fc|tr|tc|jc|jr
    /// </summary>
    /// <returns></returns>
    public string NetworkString() {
      string s = "Move|";
      if (jump) { s = "MoveJ|"; }
      s = s + from.row + "|" + from.col + "|" + to.row + "|" + to.col;
      if (jump) { s = s + "|" + over.row + "|" + over.col; }
      return s;
    }

    public static Move fromString(string data) {

      string[] aData = data.Split('|');
      

      if (aData[0] == "Move") {

        Position from = new Position(int.Parse(aData[1]), int.Parse(aData[2]));
        Position to = new Position(int.Parse(aData[3]), int.Parse(aData[4]));
        Move m = new Move(from, to);
        return m;
      } else {
        Position from = new Position(int.Parse(aData[1]), int.Parse(aData[2]));
        Position to = new Position(int.Parse(aData[3]), int.Parse(aData[4]));
        Position over = new Position(int.Parse(aData[5]), int.Parse(aData[6]));
        Move m = new Move(from, to,over);
        return m;
      }
    }

    public override string ToString() {
      return "" + (char) ('h' - from.col) + (from.row + 1) + " " + (char) ('h' - to.col) + (to.row + 1);
    }
  }
}