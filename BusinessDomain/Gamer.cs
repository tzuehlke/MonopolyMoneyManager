namespace MonopolyMoneyManager.BusinessObjects
{
    using System;
    
    public class Gamer {
        public string Name { get; set; }
        public int Balance { get; set; }
        public string Currency { get { return Balance.ToString("C");} }
        public string Id { get; private set; }
        public Gamer()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}