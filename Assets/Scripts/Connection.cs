namespace RandomDungeonWithBluePrint
{
    public class Connection
    { 
        public int From;
        public int To;

        public bool ConnectedAny(int index)
        {
            return From == index || To == index;
        }

        public bool Connected(int a, int b)
        {
            return From == a && To == b || From == b && To == a;
        }
    }
}