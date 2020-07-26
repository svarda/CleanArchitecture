using CleanArchitecture.Core.Entities.Base;

namespace CleanArchitecture.Core.Entities {
    public class Team : Entity {
        public Team() {
        }

        public Team(string name, int foundationYear, int wins, bool entryFeePaid) {
            Name = name;
            FoundationYear = foundationYear;
            Wins = wins;
            EntryFeePaid = entryFeePaid;
        }

        public string Name { get; set; }
        public int FoundationYear { get; set; }
        public int Wins { get; set; }
        public bool EntryFeePaid { get; set; }
    }
}
