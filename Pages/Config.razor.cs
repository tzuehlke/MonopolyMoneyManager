namespace MonopolyMoneyManager.Pages
{
using System.Collections.Generic;
using System.Threading.Tasks;
//using System.Text;
using System;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using TestBlazorWebAss.BusinessController;
using MonopolyMoneyManager.BusinessController;
using MonopolyMoneyManager.BusinessObjects;
using Microsoft.JSInterop;

    public partial class Config
    {
        public string playerName {get;set;}
        public int playerBalance {get;set;}
        public int currentCount {get;set;}
        private const string localStateIdentifier = "MonopolyMoneyManager";
        public string link {get;set;}
        protected override async Task OnInitializedAsync()
        {
            init();
        }
        public void init(){
            if(currentGamers.GamerList.Count == 0){
                currentGamers.GamerList.Add(new Gamer(){Name = "Pott", Balance=0});
                currentGamers.GamerList.Add(new Gamer(){Name = "A", Balance=100});
                currentGamers.GamerList.Add(new Gamer(){Name = "J", Balance=100});
                currentGamers.GamerList.Add(new Gamer(){Name = "K", Balance=100});
                currentGamers.GamerList.Add(new Gamer(){Name = "T", Balance=100});
            }
        }
        public void AddPlayer(){
            Gamer newGamer = new Gamer(){ Name = playerName, Balance = playerBalance};
            currentGamers.GamerList.Add(newGamer);
        }

        public async Task SaveStateLocal()
        {
            string state = currentGamers.GetStateAsJSON();
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", localStateIdentifier, state);
        }
        public async Task LoadStateLocal()
        {
            string state = await JSRuntime.InvokeAsync<string>("localStorage.getItem", localStateIdentifier);
            currentGamers.SetStateFromJSON(state);
        }
        public async Task SaveStateCookie(){
            string state = currentGamers.GetStateAsJSON();
            await JSRuntime.InvokeAsync<string>("setCookie", "state", state, 365);
        }
        public async Task LoadStateCookie()
        {
            string state = await JSRuntime.InvokeAsync<string>("getCookie", "state");
            currentGamers.SetStateFromJSON(state);
        }
        public void GenLink(){
            var cu = new UriBuilder(NavigationManager.Uri);
            string newuri = cu.Scheme+"://"+cu.Host+":"+cu.Port+"/monopoly/"+currentGamers.GetStateAsJSON();
            link = newuri;
        }

            string AddQueryParm(string parmName, string parmValue)
    {
        var uriBuilder = new UriBuilder(NavigationManager.Uri);
        var q = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
        q[parmName] = parmValue;
        uriBuilder.Query = q.ToString();
        var newUrl = uriBuilder.ToString();
        return newUrl;
    }

    // Blazor: get query parm from the URL
    string GetQueryParm(string parmName)
    {
        var uriBuilder = new UriBuilder(NavigationManager.Uri);
        var q = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
        return q[parmName] ?? "";
    }
    }
}