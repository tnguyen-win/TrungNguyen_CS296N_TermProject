namespace VideoGameTrading.Models {
    public class Item {
        public int ItemId { get; set; }

        public bool InCart { get; set; }

        public string? Title { get; set; }

        public string? Genre { get; set; }

        public int ReleaseYear { get; set; }

        public double Price { get; set; }

        public string? AgeRange { get; set; }

        public string? Condition { get; set; }

        public AppUser? From { get; set; }
    }
}
