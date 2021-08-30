using System; //need for DateTime

namespace WestWind.Entities 
{
    public class BuildVersion {
        public int Id { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public DateTime ReleaseDate { get; set; }

        public override string ToString() {
		    return $"Id: {Id}, Major: {Major}, Minor: {Minor}, Build: {Build}, Release Date: {ReleaseDate}";
	    }
    }
}