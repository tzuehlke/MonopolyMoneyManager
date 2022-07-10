namespace MonopolyMoneyManager.BusinessObjects
{
    using System;
    using System.Collections.Generic;
    using MonopolyMoneyManager.BusinessObjects;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    
    public class CurrentGamers {
        private IList<Gamer> gamerlist = new List<Gamer>();
        public IList<Gamer> GamerList {
            get { return gamerlist;}
             set { gamerlist = value; }
        }

        public string GetStateAsJSON(){
            string jsonString = JsonSerializer.Serialize<IList<Gamer>>(this.GamerList);
            System.Console.WriteLine(jsonString);
            return jsonString;
        }

        public void SetStateFromJSON(string state){
            this.GamerList.Clear();
            this.GamerList = JsonSerializer.Deserialize<IList<Gamer>>(state);
        }

    }
}