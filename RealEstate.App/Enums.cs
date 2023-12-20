namespace RealEstate.App
{
    public static class Enums
    {
        public enum Status
        {
            ForSale = 2,
            ForRent = 1,
            SoldOrRented = 0,
            Unknown = -1

        }
        public enum Role
        {
            User,
            Admin
        }
    }
}
