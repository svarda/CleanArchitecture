using System.ComponentModel;

namespace CleanArchitecture.Web.ViewModels {
    public class TeamViewModel : BaseViewModel {
        
        [DisplayName("Team")]
        public string Name { get; set; }

        [DisplayName("Founded")]
        public int FoundationYear { get; set; }

        [DisplayName("Wins")]
        public int Wins { get; set; }

        [DisplayName("Entry paid")]
        public bool EntryFeePaid { get; set; }
    }
}
