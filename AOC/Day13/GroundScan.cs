namespace AOC_2023.Day13
{
    public class GroundScan
    {
        public List<List<char>> Rows { get; set; }

        public GroundScan(List<List<char>> rows)
        {
            Rows = rows;
        }

        public GroundScan Clone()
        {
            var newRows = new List<List<char>>();
            foreach (var row in Rows)
            {
                var newRow = new List<char>();
                foreach (var tile in row)
                {
                    newRow.Add(tile);
                }
                newRows.Add(newRow);
            }
            return new GroundScan(newRows);
        }

        public GroundScan CloneAndRotate()
        {
            var clone = Clone();
            foreach (var row in clone.Rows)
            {
                row.Reverse();
            }
            var newRows = new List<List<char>>();
            for (var i = 0; i < clone.Rows[0].Count; i++)
            {
                var newRow = new List<char>();
                for (var j = 0; j < clone.Rows.Count; j++)
                {
                    newRow.Add(clone.Rows[j][i]);
                }
                newRows.Add(newRow);
            }
            return new GroundScan(newRows);
        }
    }
}
