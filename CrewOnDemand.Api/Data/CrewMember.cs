namespace CrewOnDemand.Api.Data
{
    public class CrewMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Base { get; set; }
        public string[] WorkDays { get; set; }
        public bool? IsAvailable { get; set; } 
    }
}
