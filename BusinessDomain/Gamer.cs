namespace MonopolyMoneyManager.BusinessObjects
{
    using System;
    using System.Text.Json.Serialization;
    
    public class Gamer {
        public string Name { get; set; }
        public int Balance { get; set; }
        [JsonIgnore]
        public string Currency { get { return Balance.ToString("C");} }
        [JsonIgnore]
        public string Id { get; private set; }
        public Gamer()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}